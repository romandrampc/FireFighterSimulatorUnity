using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeFireExtinController : MonoBehaviour
{
    internal static ModeFireExtinController instanceGameModeTrainFireExtinguisher;

    private GameController gameController;

    GameObject[] arrayFireInScene;

    bool isComplete;

    private void Awake()
    {
        if (instanceGameModeTrainFireExtinguisher != null && instanceGameModeTrainFireExtinguisher != this)
        {
            Destroy(this.gameObject.GetComponent<ModeFireExtinController>());
        }
        else
        {
            instanceGameModeTrainFireExtinguisher = this;
            gameController = GameController.instanceGame;
        }
    }

    private void OnEnable()
    {
        isComplete = false;   
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        arrayFireInScene = GameObject.FindGameObjectsWithTag("Fire");
        if (arrayFireInScene.Length == 0)
        {
            if (!isComplete)
            {
                Debug.Log("Quest Complete");
                isComplete = true;
            }
        }
    }
}
