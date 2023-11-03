using NaughtyAttributes;
using Shapes;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public bool startActive = false;
    public SpriteRenderer logo;
    public Line[] winLines;
    public Line[] permaLines;
    public Color[] colors;
    public Animator[] winLineAnims;

    public MoveObject[] hitTargets;
    public Animator logoAnim;

    public string[] verbs;

    public enum ColorSetting
    { 
        Active = 0,
        Inactive = 1
    }

    private ColorSetting _colorSetting;
    public ColorSetting colorSetting
    {
        get { return _colorSetting; }
        set
        {
            _colorSetting = value;
            SetColor();
        }
    }

    public void Start()
    {
        foreach (Line l in permaLines)
            l.Color = colors[1];
        colorSetting = startActive ? ColorSetting.Active : ColorSetting.Inactive;
    }

    private void SetColor()
    {
        logo.color = colors[(int)colorSetting];
    }

    public void FireWinLine(int lineIndex)
    {
        DebugLog.Print($"{this.name.ToUpperInvariant()} {verbs[lineIndex]} {hitTargets[lineIndex].name.ToUpperInvariant()}", DebugLog.StyleOption.Bold, DebugLog.ColorOption.Green);
        MatchManager.Get.resultMesh.text = $"{this.name.ToUpperInvariant()} {verbs[lineIndex]} {hitTargets[lineIndex].name.ToUpperInvariant()}";
        logoAnim.SetTrigger("fire");
        winLineAnims[lineIndex].SetTrigger("toggle");
        hitTargets[lineIndex].DeathAnim();
    }

    public void DeathAnim()
    {
        logoAnim.SetTrigger("death");
        Invoke("InvokeDeath", 0.75f);
    }

    void InvokeDeath()
    {
        colorSetting = ColorSetting.Inactive;
    }

    #region Naughty Buttons

    [Button]
    public void ToggleActivation()
    {
        colorSetting = (ColorSetting)(((int)colorSetting + 1) % 2);
    }

    [Button]
    private void FireWinLine1()
    {
        FireWinLine(0);
    }

    [Button]
    private void FireWinLine2()
    {
        FireWinLine(1);
    }

    #endregion

}
