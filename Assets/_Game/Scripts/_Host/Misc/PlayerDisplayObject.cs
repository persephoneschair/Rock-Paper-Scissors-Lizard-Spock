using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplayObject : MonoBehaviour
{
    public Animator anim;
    public RawImage playerAvatar;
    public TextMeshProUGUI playerNameMesh;

    public GameObject[] moveObject;

    public void ToggleDisplay()
    {
        anim.SetTrigger("toggle");
    }

    public void SetDisplay(PlayerObject po)
    {
        playerAvatar.texture = po.profileImage;
        playerNameMesh.text = po.playerName;
        foreach (GameObject go in moveObject)
            go.SetActive(false);
    }

    public void SetMove(RPSLSManager.RPSLS move)
    {
        moveObject[(int)move].SetActive(true);
    }
}
