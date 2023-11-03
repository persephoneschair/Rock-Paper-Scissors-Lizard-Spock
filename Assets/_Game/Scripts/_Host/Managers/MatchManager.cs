using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class MatchManager : SingletonMonoBehaviour<MatchManager>
{
    public TextMeshProUGUI joinAndNextMatchMesh;
    public TextMeshProUGUI roundInfoMesh;

    public List<Bout> boutsThisRound = new List<Bout>();
    public int currentBout = 0;

    public PlayerDisplayObject[] playerDisplays;
    public bool finalBout;
    public bool finalRound;

    private int timeRemaining = 0;
    public TextMeshProUGUI timeRemainingMesh;

    public TextMeshProUGUI resultMesh;
    public Animator resultAnim;

    public GameObject logo;
    public Animator canvasAnim;

    public void OnOpenLobby()
    {
        canvasAnim.SetTrigger("toggle");
        string spacedRoomCode = "";
        foreach (char c in HostManager.Get.host.RoomCode)
            spacedRoomCode += c + " ";

        joinAndNextMatchMesh.text = $"To join the game, please visit:\n<size=150%><color=orange>https://persephoneschair.itch.io/gamenight</color></size>\n<size=175%>Use the room code <color=red>{spacedRoomCode}</color></size>";
    }

    public IEnumerator AutoLockLobby(int timer)
    {
        while(timer > 0)
        {
            roundInfoMesh.text = "Match starts in: " + timer.ToString();
            yield return new WaitForSeconds(1f);
            timer--;
        }
        if (PlayerManager.Get.players.Count < 2)
            StartCoroutine(AutoLockLobby(30));
        else
            OnLockLobby();
    }

    public void OnLockLobby()
    {
        logo.SetActive(false);
        foreach (MoveObject mo in RPSLSManager.Get.objects)
            mo.gameObject.SetActive(true);
        Operator.Get.lateEntry = true;
        UpdateRoundInfo();
        SetUpBouts();
    }

    public void SetUpBouts()
    {
        boutsThisRound.Clear();
        currentBout = 0;

        foreach (PlayerObject po in PlayerManager.Get.players)
        {
            po.boutsWonThisRound = 0;
            po.boutsPlayedThisRound = 0;
            po.playedMove = RPSLSManager.RPSLS.None;
            po.UpdatePlayerScore();
            if (!po.eliminated)
                po.podium.scoreMesh.text = "";
        }

        List<PlayerObject> shuffledPlayers = PlayerManager.Get.players.Where(x => !x.eliminated).Shuffle().ToList();

        for (int r = 0; r < Operator.Get.activeBoutTotal; r++)
        {
            //Don't shuffle when in the final
            if(!finalRound)
                shuffledPlayers.Shuffle();

            for (int i = 0; i < shuffledPlayers.Count; i += 2)
                boutsThisRound.Add(new Bout(new[] { shuffledPlayers[i], shuffledPlayers[(i + 1) % shuffledPlayers.Count] }));

            //if there's only two, we shouldn't touch the lists
            if(!finalRound)
            {
                //If we have an odd number of players, player 0 already has more matches than everybody else - remove them for the next pass
                if (shuffledPlayers.Count % 2 != 0)
                    shuffledPlayers.Remove(shuffledPlayers[0]);

                //Otherwise, if we're playing an extended number of bouts, we may have removed a player that we need to put back, so recompile the whole list
                else
                    shuffledPlayers = PlayerManager.Get.players.Where(x => !x.eliminated).Shuffle().ToList();
            }
        }
        foreach (Bout b in boutsThisRound)
            DebugLog.Print($"{b.players[0].playerName} vs. {b.players[1].playerName}", DebugLog.StyleOption.Italic, DebugLog.ColorOption.Orange);

        InitiateNextBout(boutsThisRound.FirstOrDefault());
    }

    void InitiateNextBout(Bout bout)
    {
        finalBout = IsFinalBoutOfRound();
        if (finalBout)
            joinAndNextMatchMesh.text = "Final bout this round...";
        else
            joinAndNextMatchMesh.text = $"Next Bout:\n<size=150%>{boutsThisRound[currentBout + 1].players[0].playerName} vs. {boutsThisRound[currentBout + 1].players[1].playerName}";

        for(int i = 0; i < playerDisplays.Length; i++)
        {
            playerDisplays[i].SetDisplay(bout.players[i]);
            playerDisplays[i].ToggleDisplay();

            bout.players[i].playedMove = RPSLSManager.RPSLS.None;
            HostManager.Get.SendPayloadToClient(bout.players[i], EventLibrary.HostEventType.MultipleChoiceQuestion, $"You're facing {bout.players[(i + 1) % 2].playerName}...|{Operator.Get.turnTime - 1}|ROCK|PAPER|SCISSORS|LIZARD|SPOCK|RANDOM");
        }
        StartCoroutine(Countdown());
    }

    void InitiateNextBout()
    {
        if (finalBout)
        {
            EndOfRound();
            return;
        }
        currentBout++;
        InitiateNextBout(boutsThisRound[currentBout]);
    }

    IEnumerator Countdown()
    {
        timeRemaining = Operator.Get.turnTime;
        timeRemainingMesh.text = timeRemaining.ToString();
        while(timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            if (timeRemaining == 0)
            {
                timeRemainingMesh.text = "";
                break;
            }
            timeRemaining--;
            timeRemainingMesh.text = timeRemaining.ToString();
        }
        timeRemainingMesh.text = "";
        TimeUp();
    }

    void TimeUp()
    {
        for (int i = 0; i < playerDisplays.Length; i++)
            HostManager.Get.SendPayloadToClient(boutsThisRound[currentBout].players[i], EventLibrary.HostEventType.Information, $"Time up!");

        Invoke("TimeUpCooldown", 1.5f);
    }

    void TimeUpCooldown()
    {
        List<RPSLSManager.RPSLS> moves = new List<RPSLSManager.RPSLS>();
        for (int i = 0; i < playerDisplays.Length; i++)
        {
            playerDisplays[i].SetMove(boutsThisRound[currentBout].players[i].playedMove);
            playerDisplays[i].ToggleDisplay();
            if (boutsThisRound[currentBout].players[i].playedMove != RPSLSManager.RPSLS.None && !moves.Contains(boutsThisRound[currentBout].players[i].playedMove))
                moves.Add(boutsThisRound[currentBout].players[i].playedMove);
        }
        foreach (RPSLSManager.RPSLS move in moves)
            RPSLSManager.Get.ToggleActivation(move);
        RPSLSManager.Get.DelayedFire();
        Invoke("DetermineWinner", 3f);
    }

    public void DetermineWinner()
    {
        StartCoroutine(WinAnimation());
    }

    IEnumerator WinAnimation()
    {
        resultAnim.SetTrigger("toggle");
        int winIndex = RPSLSManager.Get.CalculateWinner(boutsThisRound[currentBout].players[0].playedMove, boutsThisRound[currentBout].players[1].playedMove);
        if (winIndex >= 0)
        {
            playerDisplays[(winIndex + 1) % 2].ToggleDisplay();
            boutsThisRound[currentBout].players[winIndex].boutsWonThisRound++;
            boutsThisRound[currentBout].players[winIndex].totalboutsWon++;
            yield return new WaitForSeconds(5f);
            RPSLSManager.Get.ToggleActivation(boutsThisRound[currentBout].players[winIndex].playedMove);
            playerDisplays[winIndex].ToggleDisplay();
        }
        else
        {
            resultMesh.text = "NO WINNER";
            yield return new WaitForSeconds(3f);
            if(winIndex == -1)
                RPSLSManager.Get.ToggleActivation(boutsThisRound[currentBout].players[0].playedMove);

            playerDisplays[0].ToggleDisplay();
            playerDisplays[1].ToggleDisplay();
        }
        foreach (PlayerObject po in boutsThisRound[currentBout].players)
        {
            po.boutsPlayedThisRound++;
            po.podium.scoreMesh.text = "<color=" + (po.boutsWonThisRound >= Operator.Get.activeBoutTarget ? "green>" : "red>") + po.boutsWonThisRound;

            po.UpdatePlayerScore();
        }
            
        yield return new WaitForSeconds(3f);
        InitiateNextBout();
    }

    public void EndOfRound()
    {
        List<string> eliminatedNames = PlayerManager.Get.players.Where(x => !x.eliminated && x.boutsWonThisRound < Operator.Get.activeBoutTarget).Select(x => x.playerName).ToList();
        foreach(PlayerObject po in PlayerManager.Get.players.Where(x => !x.eliminated))
        {
            if(po.boutsWonThisRound < Operator.Get.activeBoutTarget)
            {
                po.eliminated = true;
                Destroy(po.podium.gameObject);
                po.boutsPlayedThisRound = 0;
                po.boutsWonThisRound = 0;
                po.UpdatePlayerScore();
                HostManager.Get.SendPayloadToClient(po, EventLibrary.HostEventType.Information, $"You failed to hit the target and are out of the game!\nThanks for playing!");
            }
        }

        resultAnim.SetTrigger("toggle");
        if (eliminatedNames.Count == 0)
            resultMesh.text = "Nobody was eliminated";
        else if(eliminatedNames.Count == 1)
            resultMesh.text = $"{eliminatedNames.FirstOrDefault()} was eliminated";
        else
            resultMesh.text = string.Join(", ", eliminatedNames.Take(eliminatedNames.Count() - 1)) + " and " + eliminatedNames.Last() + " were eliminated";

        if (PlayerManager.Get.players.Count(x => !x.eliminated) > 1)
        {
            foreach (PlayerObject po in PlayerManager.Get.players.Where(x => !x.eliminated))
            {
                HostManager.Get.SendPayloadToClient(po, EventLibrary.HostEventType.Information, $"Get ready for the next round...");
                po.boutsPlayedThisRound = 0;
                po.boutsWonThisRound = 0;
                po.UpdatePlayerScore();
            }                

            GameplayManager.Get.roundsPlayed++;
            if (PlayerManager.Get.players.Count(x => !x.eliminated) == 2)
            {
                finalRound = true;
                Operator.Get.activeBoutTotal = Operator.Get.finalBoutTotal;
                Operator.Get.activeBoutTarget = Operator.Get.finalBoutTarget;
            }
            else if (PlayerManager.Get.players.Count(x => !x.eliminated) < 5)
            {
                Operator.Get.activeBoutTotal = Operator.Get.fewerThanFiveBoutTotal;
                Operator.Get.activeBoutTarget = Operator.Get.fewerThanFiveBoutTarget;
            }
            else if(PlayerManager.Get.players.Count(x => !x.eliminated) < 10)
            {
                Operator.Get.activeBoutTotal = Operator.Get.fewerThanTenBoutTotal;
                Operator.Get.activeBoutTarget = Operator.Get.fewerThanTenBoutTarget;
            }
            else if(PlayerManager.Get.players.Count(x => !x.eliminated) % 2 == 0)
            {
                Operator.Get.activeBoutTotal = Operator.Get.evenBoutTotal;
                Operator.Get.activeBoutTarget = Operator.Get.evenBoutTarget;
            }
            else
            {
                Operator.Get.activeBoutTotal = Operator.Get.startBoutTotal;
                Operator.Get.activeBoutTarget = Operator.Get.startBoutTarget;
            }

            joinAndNextMatchMesh.text = $"End of round {GameplayManager.Get.roundsPlayed}";
            UpdateRoundInfo();
            Invoke("SetUpBouts", 5f);
        }
        else
            StartCoroutine(EndOfGame());
    }

    public IEnumerator EndOfGame()
    {
        roundInfoMesh.text = "Game over";
        joinAndNextMatchMesh.text = "Game over";
        yield return new WaitForSeconds(5f);
        resultAnim.SetTrigger("toggle");

        if(PlayerManager.Get.players.Count(x => !x.eliminated) == 1)
        {
            PlayerObject winner = PlayerManager.Get.players.FirstOrDefault(x => !x.eliminated);
            resultMesh.text = $"{winner.playerName} is the winner";
            winner.boutsWonThisRound = 0;
            winner.boutsPlayedThisRound = 0;
            winner.podium.scoreMesh.text = "";
            winner.totalboutsWon *= 2;
            HostManager.Get.SendPayloadToClient(winner, EventLibrary.HostEventType.Information, "Congratulations!\nYou won!");
            winner.UpdatePlayerScore();
        }
            
        else
            resultMesh.text = "No winners this time";

        yield return new WaitForSeconds(5f);

        foreach (MoveObject mo in RPSLSManager.Get.objects)
            mo.gameObject.SetActive(false);
        logo.SetActive(true);
        GameplayPennys.Get.UpdatePennysAndMedals();
        canvasAnim.SetTrigger("toggle");

        foreach (PlayerObject po in PlayerManager.Get.players)
            HostManager.Get.SendPayloadToClient(po, EventLibrary.HostEventType.Information, $"Thanks for playing Rock, Paper, Scissors Lizard, Spock\n\nYou have earned {po.totalboutsWon * GameplayPennys.Get.multiplyFactor} Pennys");
    }

    public void CheckForBothResponded()
    {
        //Both have moved
        if (boutsThisRound[currentBout].players.Count(x => x.playedMove == RPSLSManager.RPSLS.None) == 0)
            timeRemaining = 0;
    }

    public void UpdateRoundInfo()
    {
        roundInfoMesh.text = $"Round {GameplayManager.Get.roundsPlayed + 1}\n<size=75%>Win {Operator.Get.activeBoutTarget} of {Operator.Get.activeBoutTotal} to {(finalRound ? "win" : "progress")}</size>\n<size=50%>{PlayerManager.Get.players.Count(x => !x.eliminated)} players active";
    }

    private bool IsFinalBoutOfRound()
    {
        return currentBout == boutsThisRound.Count - 1 ? true : false;
    }
}
