using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using System.Linq;
using Control;

public class GameplayManager : SingletonMonoBehaviour<GameplayManager>
{
    public enum GameplayStage
    {
        OpenLobby,
        LockLobby,
        DoNothing
    };
    public GameplayStage currentStage = GameplayStage.DoNothing;

    public enum Round { None };
    public Round currentRound = Round.None;
    public int roundsPlayed = 0;

    [Button]
    public void ProgressGameplay()
    {
        switch (currentStage)
        {
            case GameplayStage.OpenLobby:
                MatchManager.Get.OnOpenLobby();
                if(Operator.Get.autoRun)
                {
                    StartCoroutine(MatchManager.Get.AutoLockLobby(Operator.Get.autoStartCountdown));
                    currentStage = GameplayStage.DoNothing;
                }
                else
                    currentStage = GameplayStage.LockLobby;
                break;

            case GameplayStage.LockLobby:
                if (PlayerManager.Get.players.Count < 2)
                    return;
                MatchManager.Get.OnLockLobby();
                currentStage++;
                break;

            case GameplayStage.DoNothing:
                break;
        }
    }
}
