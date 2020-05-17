using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerForGame : MonoBehaviour
{
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.instanceGame;
    }
    
    void LoadMainMenu()
    {

    }

    void LoadTrainingScene()
    {
        
    }

    void LoadSurvivorScene()
    {

    }

    void LoadFireExtinScene()
    {

    }
}
