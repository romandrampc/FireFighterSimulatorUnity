using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Valve.VR.InteractionSystem;

public enum BodyType
{
	Nozzle,
	Tank
}

public class FollowHand : MonoBehaviour
{
	public bool shouldFollow;
	public Vector3 offsetVector;
	Hand currentHand;

	
	public BodyType Follow;

	

	void LateUpdate()
	{
		if (shouldFollow)
		{
			transform.position = currentHand.transform.position;
			transform.eulerAngles = currentHand.transform.eulerAngles + offsetVector;
		}
	}

	public void SnapToHand(Hand hand)
	{
		currentHand = hand;
		transform.DOMove(currentHand.transform.position, 0.25f);
		transform.DORotate(currentHand.transform.eulerAngles + offsetVector, 0.25f).OnComplete(() => shouldFollow = true);
	}
}
