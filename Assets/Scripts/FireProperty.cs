using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;

public class FireProperty : MonoBehaviour
{
    [SerializeField] const int numberOfParticle=4;
    [SerializeField] float currentBurnLevel=80;
    [SerializeField] float maxBurnLevel=100;
    [SerializeField] float healFire = 7.0f;
	[SerializeField] ParticleSystem[] fireParticle;

    //[Header("CallBack")]
    //public UnityAction CallBack;

    float[] maxEmission;
    float[] maxLifeTime;
    public TypeOfFlame typeOfFlames;

    [Header("Obj To Destroy")]
    [SerializeField] GameObject gameObjectToDestroy;
    

    private void Start()
	{
        fireParticle = GetComponentsInChildren<ParticleSystem>();
        maxEmission = new float[fireParticle.Length];
        maxLifeTime = new float[fireParticle.Length];

        for (int i =0;i<fireParticle.Length;i++)
        {
            var fireEmissionMax = fireParticle[i].emission.rateOverTime.constantMax;
            maxEmission[i] = fireEmissionMax;
            maxLifeTime[i] = fireParticle[i].startLifetime;
        }
    }

	private void FixedUpdate()
    {
        HealFire(healFire);
        if (currentBurnLevel <= 0)
        {
            //if(CallBack != null)
            //{
            //    CallBack.Invoke();
            //}
            if (gameObjectToDestroy != null)
                Destroy(gameObjectToDestroy);

            Destroy(this.gameObject);
        }
        else
        {
            for (int i = 0; i < fireParticle.Length; i++)
            {
                var fireEmission = fireParticle[i].emission;
                var rate = fireEmission.rateOverTime;
                rate.constantMin = maxEmission[i] * (currentBurnLevel / maxBurnLevel);
                rate.constantMax = maxEmission[i] * (currentBurnLevel / maxBurnLevel);
                fireEmission.rateOverTime = rate;

                fireParticle[i].startLifetime = maxLifeTime[i] * (currentBurnLevel / maxBurnLevel);
            }
        }
    }

    public void DouseFire(float damage)
    {
        currentBurnLevel -= (damage * Time.deltaTime);  
    }

    void HealFire(float damage)
    {
        if (damage > 0 && currentBurnLevel <= maxBurnLevel)
        {
            currentBurnLevel += (damage * Time.deltaTime);
        }
    }
}
