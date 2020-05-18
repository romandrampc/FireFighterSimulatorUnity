using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    GameController gameController;

    public static SceneController instanceScene;
    // Start is called before the first frame update
    void Start()
    {
        if (instanceScene != null && instanceScene != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instanceScene = this;
            gameController = GameController.instanceGame;
        }
        
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(gameController.mainMenuSceneName);
    }

    public void LoadTrainingScene()
    {
        gameController.modeGame = ModeGame.Training;
        SceneManager.LoadScene(gameController.playSceneName);
    }

    public void LoadSurvivorScene()
    {
        gameController.modeGame = ModeGame.Survivor;
        SceneManager.LoadScene(gameController.playSceneName);
    }

    public void LoadFireExtinScene()
    {
        gameController.modeGame = ModeGame.FireExtin;
        SceneManager.LoadScene(gameController.fireExtinSceneName);
    }
}
