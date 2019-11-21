using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Grab : Throwable
{
    private FireExtinguisher fireExtinguisher;
    private void Start()
    {
        fireExtinguisher = this.gameObject.GetComponent<FireExtinguisher>();
    }


    protected override void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if (interactable.attachedToHand == null && startingGrabType == GrabTypes.Grip)
        {
            
                // Call this to continue receiving HandHoverUpdate messages,
                // and prevent the hand from hovering over anything else
                hand.HoverLock(interactable);

                // Attach this object to the hand
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
            
        }
        else if (isGrabEnding)
        {

            Debug.Log("re");
            // Detach this object from the hand
            hand.DetachObject(gameObject);

            // Call this to undo HoverLock
            hand.HoverUnlock(interactable);

            //// Restore position/rotation
            //transform.position = oldPosition;
            //transform.rotation = oldRotation;
        }

    }

    protected override void HandAttachedUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if (hand.grabPinchAction.GetState(hand.handType))
        {
            Debug.Log("Trigger");
            fireExtinguisher.PlayParticle();
            fireExtinguisher.checkFire = true;
        }
        else
        {
            fireExtinguisher.StopParticle();
            fireExtinguisher.checkFire = false;
        }

        if (interactable.attachedToHand == null && startingGrabType == GrabTypes.Grip)
        {
            // Detach this object from the hand
            hand.DetachObject(gameObject);

            // Call this to undo HoverLock
            hand.HoverUnlock(interactable);
        }
    }
}
