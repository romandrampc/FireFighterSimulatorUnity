using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VRRayButton : MonoBehaviour
{
    public UnityEvent callBack;

    SceneController sceneController;

    [SerializeField] ModeGame modeGame;

    [Header("Setting Highlight")]
    [SerializeField] Image panelImage;

    [SerializeField] Color TargetColor;

    Color originalColor;
    
    private void Start()
    {
        sceneController = SceneController.instanceScene;

        originalColor = panelImage.color;
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
        else if (modeGame == ModeGame.MainMenu)
        {
            callBack.AddListener(sceneController.LoadMainMenu);
        }
    }

    public void OnAction()
    {
        if (callBack != null)
        {
            callBack.Invoke();
        }
    }

    internal void OnHighlight()
    {
        panelImage.color = TargetColor;
    }

    internal void OnDisHighlight()
    {
        panelImage.color = originalColor;
    }
}
