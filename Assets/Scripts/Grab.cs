using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using DG.Tweening;
using System;

[RequireComponent(typeof(Interactable))]
public class Grab : MonoBehaviour
{
	public event Action OnPinchSuccessHandler;
	public SteamVR_Action_Boolean grabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
	public SteamVR_Input_Sources rightHand,leftHand;
	public Hand leftH, rightH;
	FireExtinguisher fireExtinguisher;
	Interactable interactable;
	Teleport teleport;
	AudioSource audioSource;

	public FollowHand[] followhand;

	bool checkFire;
	public bool isGrabRight, isGrabLeft;

	

	void Awake()
    {
		interactable = GetComponent<Interactable>();
		fireExtinguisher = GetComponent<FireExtinguisher>();
        audioSource = GetComponent<AudioSource>();
		teleport = FindObjectOfType<Teleport>();
	}

	private void Start()
	{
		if(leftH==null || rightH ==null)
		{
			Hand[] playerHand = FindObjectsOfType<Hand>();
			foreach(var hand in playerHand)
			{
				if(hand.handType == SteamVR_Input_Sources.LeftHand)
				{
					leftH = hand;
				}
				else if(hand.handType == SteamVR_Input_Sources.RightHand)
				{
					rightH = hand;
				}
			}
		}
		rightHand = SteamVR_Input_Sources.RightHand;
		leftHand = SteamVR_Input_Sources.LeftHand;
		if (teleport != null)
		{
			teleport.CancelTeleportHint();
		}
		leftH.ShowController();
		rightH.ShowController();
	}

	private void OnHandHoverBegin(Hand hand)
	{
		ControllerButtonHints.ShowButtonHint(hand, grabPinchAction);
		ControllerButtonHints.ShowTextHint(hand, grabPinchAction, "Grab");
	}

	private void OnHandHoverEnd(Hand hand)
	{
		HideAllHint(hand);
	}

	private static void HideAllHint(Hand hand)
	{
		ControllerButtonHints.HideAllTextHints(hand);
		ControllerButtonHints.HideAllTextHints(hand.otherHand);
		ControllerButtonHints.HideAllButtonHints(hand);
		ControllerButtonHints.HideAllButtonHints(hand.otherHand);
	}

	public void HandHoverUpdate(Hand hand)
	{
		//check for grab
		if(grabPinchAction.GetStateDown(rightHand) && !isGrabLeft && !isGrabRight)
		{
			foreach(var x in followhand)
			{
				if(x.Follow == BodyType.Nozzle)
				{
					x.SnapToHand(rightH.otherHand);
				}
				else if(x.Follow == BodyType.Tank)
				{
					x.SnapToHand(rightH);
				}
			}
			isGrabRight = true;
			HideAllHint(hand);
			ControllerButtonHints.ShowButtonHint(rightH, grabPinchAction);
			ControllerButtonHints.ShowTextHint(rightH, grabPinchAction, "Fire");
			interactable.StopHighLighting();
			hand.HideController(true);
			interactable.highlightOnHover = false;
			hand.otherHand.HideController();
			OnPinchSuccessHandler?.Invoke();
			fireExtinguisher.SwapModel();
            ControllerButtonHints.isGrabbing = true;
			return;
		}
		else if(grabPinchAction.GetStateDown(leftHand) && !isGrabLeft && !isGrabRight)
		{
			foreach (var x in followhand)
			{
				if (x.Follow == BodyType.Nozzle)
				{
					x.SnapToHand(leftH.otherHand);
				}
				else if (x.Follow == BodyType.Tank)
				{
					x.SnapToHand(leftH);
				}
			}
			isGrabLeft = true;
			HideAllHint(hand);
			ControllerButtonHints.ShowButtonHint(leftH, grabPinchAction);
			ControllerButtonHints.ShowTextHint(leftH, grabPinchAction, "Fire");
			interactable.StopHighLighting();
			hand.HideController(true);
			hand.otherHand.HideController();
			interactable.highlightOnHover = false;
			OnPinchSuccessHandler?.Invoke();
			fireExtinguisher.SwapModel();
            ControllerButtonHints.isGrabbing = true;
            return;
		}

		//check if player pinch the right hand
		
		if(isGrabRight)
        {
            GrabRight(hand);
        }
        else if(isGrabLeft)
        {
            GrabLeft(hand);
        }
    }

    public void GrabLeft(Hand hand)
    {
        if (grabPinchAction.GetStateDown(leftHand))
        {
            fireExtinguisher.PlayParticle();
            fireExtinguisher.checkFire = true;
            StartCoroutine(Pulsing(leftH, leftHand));
            if (fireExtinguisher.foam.gameObject.activeInHierarchy)
            {
                audioSource.DOFade(1, 0.25f);
            }
            HideAllHint(hand);
        }
        else if (grabPinchAction.GetStateUp(leftHand))
        {
            fireExtinguisher.StopParticle();
            fireExtinguisher.checkFire = false;
            ControllerButtonHints.ShowButtonHint(leftH, grabPinchAction);
            ControllerButtonHints.ShowTextHint(leftH, grabPinchAction, "Fire");
            audioSource.DOFade(0, 0.25f);
            StopAllCoroutines();
        }
    }

    public void GrabRight(Hand hand)
    {
        if (grabPinchAction.GetStateDown(rightHand))
        {
            fireExtinguisher.PlayParticle();
            fireExtinguisher.checkFire = true;
            StartCoroutine(Pulsing(rightH, rightHand));
            if (fireExtinguisher.foam.gameObject.activeInHierarchy)
            {
                audioSource.DOFade(1, 0.25f);
            }
            HideAllHint(hand);
        }
        else if (grabPinchAction.GetStateUp(rightHand))
        {
            fireExtinguisher.StopParticle();
            fireExtinguisher.checkFire = false;
            ControllerButtonHints.ShowButtonHint(rightH, grabPinchAction);
            ControllerButtonHints.ShowTextHint(rightH, grabPinchAction, "Fire");
            audioSource.DOFade(0, 0.25f);
            StopAllCoroutines();
        }
    }

    IEnumerator Pulsing(Hand hand, SteamVR_Input_Sources handInput)
	{
		while(grabPinchAction.GetState(handInput))
		{
			hand.TriggerHapticPulse(1000);
			yield return new WaitForSeconds(1);
		}
	}

	
}
