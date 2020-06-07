using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class SceneController : MonoBehaviour
{
    GameController gameController;

    public static SceneController instanceScene;

    private void Awake()
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
    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    
    public void LoadMainMenu()
    {
        gameController.modeGame = ModeGame.MainMenu;
        SceneManager.LoadScene(gameController.mainMenuSceneName);
    }

    public void LoadTrainingScene()
    {
        gameController.wasEnter = false;
        gameController.modeGame = ModeGame.Training;
        SceneManager.LoadScene(gameController.playSceneName);
    }

    public void LoadSurvivorScene()
    {
        gameController.wasEnter = false;
        gameController.modeGame = ModeGame.Survivor;
        SceneManager.LoadScene(gameController.playSceneName);
    }

    public void LoadFireExtinScene()
    {
        gameController.modeGame = ModeGame.FireExtin;
        SceneManager.LoadScene(gameController.fireExtinSceneName);
    }
    
    public void LoadScoreScene()
    {
        gameController.modeGame = ModeGame.MainMenu;
        SceneManager.LoadScene(gameController.scoreSceneName);
    }

    public void LoadGameOver()
    {
        gameController.modeGame = ModeGame.MainMenu;
        SceneManager.LoadScene(gameController.gameOverSceneName);
    }
}
