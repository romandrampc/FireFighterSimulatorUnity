using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeTrainingController : MonoBehaviour
{
    internal static ModeTrainingController instanceGameModeTraining;

    private GameController gameController;

    private void Awake()
    {
        if (instanceGameModeTraining != null && instanceGameModeTraining != this)
        {
            Destroy(this.gameObject.GetComponent<ModeTrainingController>());
        }
        else
        {
            instanceGameModeTraining = this;
        }
    }

    private void OnEnable()
    {
        gameController = GameController.instanceGame;
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
