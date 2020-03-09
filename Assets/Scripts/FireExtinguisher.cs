using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
	public ParticleSystem foam;
	[SerializeField] GameObject defaultModel;
	[SerializeField] GameObject grabModel;
	[SerializeField] GameObject nozzle;
	public Vector3 offsetVector;
	public bool attachToHand;
	public bool checkFire;
	//
	[SerializeField] float damageFire = 20.0f;
	[SerializeField] float radius;
	[SerializeField] float range;
	

	Vector3 startPoint = Vector3.zero;
	Vector3 endPoint = Vector3.zero;

	private void Start()
	{

	}

	public void PlayParticle()
	{
		foam.Play();
		
	}

	public void StopParticle()
	{
		foam.Stop();
	}


	private void FixedUpdate()
	{
		startPoint = nozzle.transform.position;
		endPoint = nozzle.transform.position + (nozzle.transform.forward * range);

		if(checkFire)
		{
			Collider[] hitColliders = Physics.OverlapCapsule(startPoint, endPoint, radius);
			foreach (var collider in hitColliders)
			{
				if (collider.CompareTag("Fire"))
				{
					collider.gameObject.SendMessage("DouseFire", damageFire);
				}
			}
		}
	}
	

	public void ChangeModel()
	{
		grabModel.SetActive(false);
		nozzle.SetActive(false);
		defaultModel.SetActive(true);
	}

	public void SwapModel()
	{
		grabModel.SetActive(true);
		nozzle.SetActive(true);
		defaultModel.SetActive(false);
	}
	

	
}
