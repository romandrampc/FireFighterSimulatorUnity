using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class ElevatorButton : MonoBehaviour
{
    bool wasPlayAnimation;
    [SerializeField] PlayableDirector playableDirec;
    // Start is called before the first frame update
    void Start()
    {
        wasPlayAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void HandHoverUpdate(Hand hand)
    {
        OnButtonDown();
    }

    void OnButtonDown()
    {
        if (!wasPlayAnimation)
        {
            wasPlayAnimation = true;
            playableDirec.Play();
        }
    }
}
