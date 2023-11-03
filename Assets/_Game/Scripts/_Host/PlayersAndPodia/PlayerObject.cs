using Control;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerObject
{
    public string playerClientID;
    public Player playerClientRef;
    public Podium podium;
    public string otp;
    public string playerName;

    public string twitchName;
    public Texture profileImage;

    public bool eliminated;

    public RPSLSManager.RPSLS playedMove;
    public int boutsWonThisRound = 0;
    public int boutsPlayedThisRound = 0;
    public int totalboutsWon = 0;

    public PlayerObject(Player pl, string name)
    {
        playerClientRef = pl;
        otp = OTPGenerator.GenerateOTP();
        playerName = name;
    }

    public void ApplyProfilePicture(string name, Texture tx, bool bypassSwitchAccount = false)
    {
        //Player refreshs and rejoins the same game
        if (PlayerManager.Get.players.Count(x => (!string.IsNullOrEmpty(x.twitchName)) && x.twitchName.ToLowerInvariant() == name.ToLowerInvariant()) > 0 && !bypassSwitchAccount)
        {
            PlayerObject oldPlayer = PlayerManager.Get.players.FirstOrDefault(x => x.twitchName.ToLowerInvariant() == name.ToLowerInvariant());
            if (oldPlayer == null)
                return;

            HostManager.Get.SendPayloadToClient(oldPlayer, EventLibrary.HostEventType.SecondInstance, "");

            oldPlayer.playerClientID = playerClientID;
            oldPlayer.playerClientRef = playerClientRef;
            oldPlayer.playerName = playerName;

            otp = "";
            //podium.containedPlayer = null;
            podium = null;
            playerClientRef = null;
            playerName = "";

            if (PlayerManager.Get.pendingPlayers.Contains(this))
                PlayerManager.Get.pendingPlayers.Remove(this);

            HostManager.Get.SendPayloadToClient(oldPlayer, EventLibrary.HostEventType.Validated, $"{oldPlayer.playerName}|{oldPlayer.boutsWonThisRound.ToString()}/{oldPlayer.boutsPlayedThisRound.ToString()}|{oldPlayer.twitchName}");
            //HostManager.Get.UpdateLeaderboards();
            return;
        }
        otp = "";
        twitchName = name.ToLowerInvariant();
        profileImage = tx;
        if(!Operator.Get.lateEntry)
        {
            HostManager.Get.SendPayloadToClient(this, EventLibrary.HostEventType.Validated, $"{playerName}|{boutsWonThisRound.ToString()}/{boutsPlayedThisRound.ToString()}|{twitchName}");
            PlayerManager.Get.players.Add(this);
            PlayerManager.Get.pendingPlayers.Remove(this);
            SaveManager.BackUpData();
            PlayerManager.Get.InstantiateNewPodium(this);
        }        
        else
        {
            HostManager.Get.SendPayloadToClient(this, EventLibrary.HostEventType.Information, "The game has already started.");
            PlayerManager.Get.pendingPlayers.Remove(this);
            return;
        }
    }

    public void HandlePlayerScoring(string[] submittedAnswers)
    {
        string ans = submittedAnswers.FirstOrDefault();
        switch(ans)
        {
            case "ROCK":
                playedMove = RPSLSManager.RPSLS.Rock;
                break;

            case "PAPER":
                playedMove = RPSLSManager.RPSLS.Paper;
                break;

            case "SCISSORS":
                playedMove = RPSLSManager.RPSLS.Scissors;
                break;

            case "LIZARD":
                playedMove = RPSLSManager.RPSLS.Lizard;
                break;

            case "SPOCK":
                playedMove = RPSLSManager.RPSLS.Spock;
                break;

            case "RANDOM":
                playedMove = (RPSLSManager.RPSLS)UnityEngine.Random.Range(0, 5);
                break;
        }
        MatchManager.Get.CheckForBothResponded();
    }

    public void UpdatePlayerScore()
    {
        HostManager.Get.SendPayloadToClient(this, EventLibrary.HostEventType.UpdateScore, $"<color={(boutsWonThisRound >= Operator.Get.activeBoutTarget ? "green" : "red")}>{boutsWonThisRound}/{boutsPlayedThisRound}");
    }
}
