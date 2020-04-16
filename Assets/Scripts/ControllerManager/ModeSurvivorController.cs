using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSurvivorController : MonoBehaviour
{
    internal static ModeSurvivorController instanceGameModeSurvivor;

    private GameController gameController;

    private void Awake()
    {
        if (instanceGameModeSurvivor != null && instanceGameModeSurvivor != this)
        {
            Destroy(this.gameObject.GetComponent<ModeSurvivorController>());
        }
        else
        {
            instanceGameModeSurvivor = this;
            gameController = GameController.instanceGame;
        }
    }
    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
