using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using NaughtyAttributes;
using System.Linq;

public class Operator : SingletonMonoBehaviour<Operator>
{
    [Header("System Settings")]

    [Tooltip("Once true, no more players can join - flagged when lobby locks")]
    [ShowOnly] public bool lateEntry;
    [Tooltip("Supresses Twitch chat messages and will store Pennys and medals in a separate test file")]
    public bool testMode;
    [Tooltip("Players must join the room with valid Twitch username as their name; this will skip the process of validation")]
    public bool fastValidation;
    [Tooltip("Start the game in recovery mode to restore any saved data from a previous game crash")]
    public bool recoveryMode;
    [Tooltip("Limits the number of accounts that may connect to the room (set to 0 for infinite)")]
    [Range(0, 100)] public int playerLimit;

    [Header("Game Settings")]
    [Tooltip("Bouts required to win a round")]
    [ShowOnly] public int activeBoutTarget = 1;
    [Tooltip("Bouts per player per round")]
    [ShowOnly] public int activeBoutTotal = 2;

    [Header("Timing Settings")]
    [Tooltip("Will run the game automatically without any operator input")]
    public bool autoRun;
    [Tooltip("Time (seconds) before the game will start")]
    [Range(1, 300)] public int autoStartCountdown = 120;
    [Tooltip("Time (seconds) per turn before default is locked in")]
    [Range(2, 30)] public int turnTime = 10;

    [Header("Start Settings")]
    [Tooltip("The required bouts at the start of the match")]
    [Range(0, 10)] public int startBoutTarget = 1;
    [Tooltip("The total bouts per round at the start of the match - THIS MUST BE EVEN (game will add one on if it is not)")]
    [Range(0, 10)] public int startBoutTotal = 2;

    [Header("Large Even Number Settings")]
    [Tooltip("The required bouts at the start of the match")]
    [Range(0, 10)] public int evenBoutTarget = 1;
    [Tooltip("The total bouts per round at the start of the match - THIS MUST BE EVEN")]
    [Range(0, 10)] public int evenBoutTotal = 1;

    [Header("Fewer Than Ten Settings")]
    [Tooltip("The required bouts when fewer than ten players remain")]
    [Range(0, 10)] public int fewerThanTenBoutTarget = 2;
    [Tooltip("The total bouts when fewer than ten players remain - THIS MUST BE EVEN")]
    [Range(0, 10)] public int fewerThanTenBoutTotal = 2;

    [Header("Fewer Than Five Settings")]
    [Tooltip("The required bouts when fewer than five players remain")]
    [Range(0, 10)] public int fewerThanFiveBoutTarget = 2;
    [Tooltip("The total bouts when fewer than five players remain - THIS MUST BE EVEN")]
    [Range(0, 10)] public int fewerThanFiveBoutTotal = 4;

    [Header("Final Settings")]
    [Tooltip("The required bouts at the start of the match")]
    [Range(0, 10)] public int finalBoutTarget = 2;
    [Tooltip("The total bouts per round at the start of the match")]
    [Range(0, 10)] public int finalBoutTotal = 3;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        activeBoutTarget = startBoutTarget;
        activeBoutTotal = startBoutTotal;

        if (activeBoutTotal % 2 != 0)
            activeBoutTotal++;

        HostManager.Get.host.ReloadHost = recoveryMode;
        if (recoveryMode)
            SaveManager.RestoreData();

        DataStorage.CreateDataPath();
        GameplayEvent.Log("Game initiated");
        //HotseatPlayerEvent.Log(PlayerObject, "");
        //AudiencePlayerEvent.Log(PlayerObject, "");
        EventLogger.PrintLog();            
    }

    [Button]
    public void ProgressGameplay()
    {
        GameplayManager.Get.ProgressGameplay();
    }
}
