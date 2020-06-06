using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;

public class UIButton : UIElement
{
    internal UnityEvent buttonPushEvents;

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
        buttonPushEvents.Invoke();
        SoundController.instanceSound.AudioPlay(tempclip);
    }
}
