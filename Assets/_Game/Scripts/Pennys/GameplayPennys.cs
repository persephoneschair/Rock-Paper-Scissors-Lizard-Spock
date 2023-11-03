using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using Newtonsoft.Json;
using NaughtyAttributes;
using System.Linq;
using TMPro;

public class GameplayPennys : SingletonMonoBehaviour<GameplayPennys>
{
    private PlayerShell playerList;
    private readonly string path = @"D:\Unity Projects\HerokuPennyData\PennyStorage";

    [Range (1, 50)] public int multiplyFactor = 10;

    [Button]
    public void UpdatePennysAndMedals()
    {
        AwardPennys();
        WriteNewFile();
    }

    private void LoadJSON()
    {
        playerList = JsonConvert.DeserializeObject<PlayerShell>(File.ReadAllText(path + @"\NewPennys.txt"));
    }

    private void AwardPennys()
    {
        List<PlayerObject> list = PlayerManager.Get.players.OrderByDescending(p => p.totalboutsWon).ThenBy(p => p.twitchName).Where(x => x.totalboutsWon > 0).ToList();
        PlayerPennyData ppd;

        LoadJSON();
        foreach (PlayerObject p in list)
        {
            ppd = playerList.playerList.FirstOrDefault(x => x.PlayerName.ToLowerInvariant() == p.twitchName.ToLowerInvariant());
            if (ppd == null)
                CreateNewPlayer(p);
            else
            {
                ppd.CurrentSeasonPennys += (p.totalboutsWon * multiplyFactor);
                ppd.AllTimePennys += (p.totalboutsWon * multiplyFactor);
            }
        }
    }

    private void CreateNewPlayer(PlayerObject p)
    {
        PlayerPennyData newP = new PlayerPennyData()
        {
            PlayerName = p.twitchName.ToLowerInvariant(),
            CurrentSeasonPennys = (p.totalboutsWon * multiplyFactor),
            AllTimePennys = (p.totalboutsWon * multiplyFactor)
        };
        playerList.playerList.Add(newP);
    }

    private void WriteNewFile()
    {
        string pennyPath = Operator.Get.testMode ? path + @"\NewPennysTest.txt" : path + @"\NewPennys.txt";
        string newDataContent = JsonConvert.SerializeObject(playerList, Formatting.Indented);
        File.WriteAllText(pennyPath, newDataContent);

        if (Operator.Get.testMode)
            DebugLog.Print("TEST DATA WRITTEN TO DRIVE", DebugLog.StyleOption.Bold, DebugLog.ColorOption.Orange);
        else
            DebugLog.Print("DATA WRITTEN TO DRIVE", DebugLog.StyleOption.Bold, DebugLog.ColorOption.Green);
    }
}
