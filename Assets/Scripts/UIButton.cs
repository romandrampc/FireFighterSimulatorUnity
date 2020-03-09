using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class UIButton : UIElement
{
    protected override void Awake()
    {
        base.Awake();

        
    }

    protected override void OnButtonClick()
    {
        base.OnButtonClick();

    }

    public void PlaySound(AudioClip tempclip)
    {
        
        Debug.Log("Test1");
        SoundController.instanceSound.AudioPlay(tempclip);
    }
}
