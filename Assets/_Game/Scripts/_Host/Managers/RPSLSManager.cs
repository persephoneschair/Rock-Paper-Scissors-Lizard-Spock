using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RPSLSManager : SingletonMonoBehaviour<RPSLSManager>
{
    public MoveObject[] objects;

    private RPSLS FocusMove;
    public enum RPSLS
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spock,
        None
    }

    #region Naughty Buttons

    [Button]
    private void ToggleRock()
    {
        FocusMove = RPSLS.Rock;
        ToggleActivation();
    }

    [Button]
    private void TogglePaper()
    {
        FocusMove = RPSLS.Paper;
        ToggleActivation();
    }

    [Button]
    private void ToggleScissors()
    {
        FocusMove = RPSLS.Scissors;
        ToggleActivation();
    }

    [Button]
    private void ToggleLizard()
    {
        FocusMove = RPSLS.Lizard;
        ToggleActivation();
    }

    [Button]
    private void ToggleSpock()
    {
        FocusMove = RPSLS.Spock;
        ToggleActivation();
    }

    #endregion

    private void ToggleActivation()
    {
        objects[(int)FocusMove].ToggleActivation();
    }

    public void ToggleActivation(RPSLS move)
    {
        objects[(int)move].ToggleActivation();
    }

    [Button]
    public void FireActiveMoves()
    {
        bool winnerDisplayed = false;
        foreach(MoveObject mo in objects.Where(x => x.colorSetting == MoveObject.ColorSetting.Active))
        {
            for(int i = 0; i < 2; i++)
            {
                if (mo.hitTargets[i].colorSetting == MoveObject.ColorSetting.Active)
                {
                    mo.FireWinLine(i);
                    winnerDisplayed = true;
                }                    
            }
        }
        //This means no lines have fired, which means somebody has won via abstention OR both have abstained...
        if(!winnerDisplayed && MatchManager.Get.boutsThisRound[MatchManager.Get.currentBout].players.Count(x => x.playedMove == RPSLS.None) != 2)
            MatchManager.Get.resultMesh.text = $"{objects.FirstOrDefault(x => x.colorSetting == MoveObject.ColorSetting.Active).name} WINS BY DEFAULT";
    }

    public void DelayedFire()
    {
        Invoke("FireActiveMoves", 3f);
    }

    public int CalculateWinner(RPSLS p1Move, RPSLS p2Move)
    {
        //Both abstained
        if (p1Move == RPSLS.None && p2Move == RPSLS.None)
            return -2;

        //Both the same
        else if (p1Move == p2Move)
            return -1;

        else if (p1Move == RPSLS.None)
            return 1;
        else if (p2Move == RPSLS.None)
            return 0;

        else if (objects[(int)p1Move].hitTargets.Contains(objects[(int)p2Move]))
            return 0;
        else
            return 1;
    }
}
