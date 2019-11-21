using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfButton
{
    actionButton,
    playSoundButton
}

public class SoundController : MonoBehaviour
{
    public static SoundController instanceSound;

    private void Awake()
    {
        
    }
}
