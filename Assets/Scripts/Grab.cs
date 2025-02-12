﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class Grab : MonoBehaviour
{
    [EnumFlags]
    [Tooltip("The flags used to attach this object to the hand.")]
    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;
    
    [Tooltip("How fast must this object be moving to attach due to a trigger hold instead of a trigger press? (-1 to disable)")]
    public float catchingSpeedThreshold = -1;

    [Tooltip("The local point which acts as a positional and rotational offset to use while held with a grip type grab")]
    public Transform gripOffset;

    
    private Hand handGrab;
    private bool canDetachFromhand;

    protected bool isAttachHand;
    protected bool attached = false;

    [HideInInspector]
    public Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();

        isAttachHand = false;
        canDetachFromhand = false;
    }

    protected virtual void OnHandHoverBegin(Hand hand)
    {
       

    }

    protected virtual void HandHoverUpdate(Hand hand)
    {
        GrabTypes interactGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);


        if (interactable.attachedToHand == null && interactGrabType == GrabTypes.Grip)
        {

            // Call this to continue receiving HandHoverUpdate messages,
            // and prevent the hand from hovering over anything else
            hand.HoverLock(interactable);

            // Attach this object to the hand
            hand.AttachObject(gameObject, interactGrabType, attachmentFlags,gripOffset);

            handGrab = hand;
            isAttachHand = true;

        }

        if (isGrabEnding && isAttachHand)
        {
            canDetachFromhand = true;
        }

         if (canDetachFromhand && interactGrabType == GrabTypes.Grip)
        {
            // Detach this object from the hand
            hand.DetachObject(gameObject);

            // Call this to undo HoverLock
            hand.HoverUnlock(interactable);

            //// Restore position/rotation
            //transform.position = oldPosition;
            //transform.rotation = oldRotation;

            handGrab = null;
            isAttachHand = false;
            canDetachFromhand = false;
        }

    }

    private void Update()
    {
        
        if (isAttachHand && (handGrab.grabPinchAction.GetState(handGrab.handType) || Input.GetKey(KeyCode.R)))
        {
            gameObject.SendMessage("PlayParticle", SendMessageOptions.DontRequireReceiver);
            gameObject.SendMessage("DouseToFire", SendMessageOptions.DontRequireReceiver);
           
        }
        else
        {
            gameObject.SendMessage("StopParticle", SendMessageOptions.DontRequireReceiver);
        }
    }
}
