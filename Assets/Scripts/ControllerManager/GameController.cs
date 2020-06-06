using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

public enum TypeOfFlame
{
    A_Wood,
    B_OilOrFlammableLiquids,
    C_Electronic,
    D_Metal,
    K_CombustibleCooking
}

public enum ModeGame
{
    Survivor,
    Training,
    MainMenu,
    FireExtin
}

public class GameController : MonoBehaviour
{
    public static GameController instanceGame;

    #region GameMode
    public ModeGame modeGame;

    ModeSurvivorController modeSurvivor;
    ModeTrainingController modeTraining;
    ModeFireExtinController modeFireExtin;

    #endregion

    #region Player
    private float playerHeight = 0.0f;

    internal float PlayerHeight { get => playerHeight; set => playerHeight = value; }
    #endregion

    #region SteamVR
    Teleport teleport;
    #endregion

    [Header("Scene Name")]
    [SerializeField] internal string mainMenuSceneName;
    [SerializeField] internal string playSceneName;
    [SerializeField] internal string fireExtinSceneName;
    [SerializeField] internal string scoreSceneName;

    string tempStr;

    internal bool wasEnter = false;

    #region Score Part

    #endregion

    private void Awake()
    {
        if (instanceGame != null && instanceGame != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instanceGame = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        modeFireExtin = ModeFireExtinController.instanceGameModeTrainFireExtinguisher;
    }

    private void CheckStage()
    {
        if (SceneManager.GetActiveScene().name == mainMenuSceneName)
        {
            modeGame = ModeGame.MainMenu;
        }

        if (modeGame == ModeGame.MainMenu)
        {
            //modeSurvivor.enabled = false;
            //modeTraining.enabled = false;
            modeFireExtin.enabled = false;
            wasEnter = false;
        }

        if (SceneManager.GetActiveScene().name == playSceneName)
        {
            teleport = Teleport.instance;
            modeSurvivor = ModeSurvivorController.instanceGameModeSurvivor;
            modeTraining = ModeTrainingController.instanceGameModeTraining;

            // Check To Set Survivor Control Active
            if (modeGame == ModeGame.Survivor)
            {
                modeSurvivor.enabled = true;
                if (!wasEnter)
                {
                    teleport.onTeleport.AddListener(OnTeleport);
                    wasEnter = true;
                }
            }
            else
            {
                modeSurvivor.enabled = false;
            }

            //Check To Set Training Control Active
            if (modeGame == ModeGame.Training)
            {
                modeTraining.enabled = true;
                if (!wasEnter)
                {
                    teleport.onTeleport.AddListener(OnTeleport);
                    wasEnter = true;
                }
            }
            else
            {
                modeTraining.enabled = false;
            }
        }
        else if (SceneManager.GetActiveScene().name == fireExtinSceneName)
        {
            if (modeGame == ModeGame.FireExtin)
            {
                modeFireExtin.enabled = true;
                modeFireExtin.OnDouseComplete.AddListener(OnDousedFire);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckStage();
    }

    void OnTeleport(string tempTeleAreaName)
    {
        Debug.Log("Tele Area :" + tempTeleAreaName);
    }

    public void OnGameOver()
    {
        Debug.Log("GameOver");
    }

    internal void OnDousedFire()
    {
        SceneController.instanceScene.LoadMainMenu();
    }
}
