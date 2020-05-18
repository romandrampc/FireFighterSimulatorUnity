using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRRayButton : MonoBehaviour
{
    public UnityEvent callBack;

    SceneController sceneController;

    [SerializeField] ModeGame modeGame;


    private void Start()
    {
        sceneController = SceneController.instanceScene;

        if (modeGame == ModeGame.Survivor)
        {
            callBack.AddListener(sceneController.LoadSurvivorScene);
        }
        else if (modeGame == ModeGame.Training)
        {
            callBack.AddListener(sceneController.LoadTrainingScene);
        }
        else if (modeGame == ModeGame.FireExtin)
        {
            callBack.AddListener(sceneController.LoadFireExtinScene);
        }
    }

    public void OnAction()
    {
        if (callBack != null)
        {
            callBack.Invoke();
        }
    }
    
}
