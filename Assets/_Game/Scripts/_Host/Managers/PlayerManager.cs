using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    public List<PlayerObject> pendingPlayers = new List<PlayerObject>();
    public List<PlayerObject> players = new List<PlayerObject>();

    [Header("Controls")]
    public bool pullingData = true;
    [Range(0,39)] public int playerIndex;

    public List<Podium> instancedPodia = new List<Podium>();
    public GameObject playerPodiumToInstance;
    public Transform podiumTransformTarget;


    private PlayerObject _focusPlayer;
    public PlayerObject FocusPlayer
    {
        get { return _focusPlayer; }
        set
        {
            if(value != null)
            {
                _focusPlayer = value;
                playerName = value.playerName;
                twitchName = value.twitchName;
                profileImage = value.profileImage;
                eliminated = value.eliminated;

                boutsWonThisRound = value.boutsWonThisRound;
                boutsPlayedThisRound = value.boutsPlayedThisRound;
            }
            else
            {
                playerName = "OUT OF RANGE";
                twitchName = "OUT OF RANGE";
                profileImage = null;
                eliminated = false;

                boutsWonThisRound = 0;
                boutsPlayedThisRound = 0;
            }                
        }
    }

    [Header("Fixed Fields")]
    [ShowOnly] public string playerName;
    [ShowOnly] public string twitchName;
    public Texture profileImage;
    [ShowOnly] public bool eliminated;

    [Header("Variable Fields")]
    public int boutsWonThisRound;
    public int boutsPlayedThisRound;

    void UpdateDetails()
    {
        if (playerIndex >= players.Count)
            FocusPlayer = null;
        else
            FocusPlayer = players.OrderBy(x => x.playerName).ToList()[playerIndex];
    }

    private void Update()
    {
        if (pullingData)
            UpdateDetails();
    }

    [Button]
    public void SetPlayerDetails()
    {
        if (pullingData)
            return;
        SetDataBack();
    }

    [Button]
    public void RestoreOrEliminatePlayer()
    {
        if (pullingData)
            return;
        pullingData = true;

    }

    void SetDataBack()
    {
        FocusPlayer.boutsWonThisRound = boutsWonThisRound;
        FocusPlayer.boutsPlayedThisRound = boutsPlayedThisRound;
        pullingData = true;
    }

    public void InstantiateNewPodium(PlayerObject po)
    {
        var x = Instantiate(playerPodiumToInstance, podiumTransformTarget);
        Podium xP = x.GetComponent<Podium>();
        po.podium = xP;
        xP.containedPlayer = po;
        xP.playerAvatar.texture = po.profileImage;
        instancedPodia.Add(xP);

        if (!Operator.Get.autoRun)
            MatchManager.Get.UpdateRoundInfo();
    }
}
