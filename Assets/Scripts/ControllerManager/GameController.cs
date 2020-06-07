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

    #region Teleport Area Target Name
    [Header("Teleport Area Name")]
    [SerializeField] internal string elevatorArea = "TeleportAreaRoomZone3_5_Elevetor";
    [SerializeField] internal string exitArea = "TeleportAreaRoomZone4_Exit";
    #endregion

    #region Timer Setting
    [Header("Timer Setting")]
    [SerializeField] internal float timeForSurvive = 120.0f;
    #endregion

    #region Scene Name
    [Header("Scene Name")]
    [SerializeField] internal string mainMenuSceneName;
    [SerializeField] internal string playSceneName;
    [SerializeField] internal string fireExtinSceneName;
    [SerializeField] internal string scoreSceneName;
    [SerializeField] internal string gameOverSceneName;
    #endregion

    #region Score Part
    internal int pushAlarmPartScore = 0;
    internal int douseFireScore = 0;
    internal int useAxeScore = 0;
    internal int timeScore = 0;
    #endregion

    string tempStr;

    internal bool wasEnter = false;


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
                    pushAlarmPartScore = 0;
                    douseFireScore = 0;
                    useAxeScore = 0;
                    timeScore = 0;
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
        if (tempTeleAreaName == elevatorArea)
        {
            OnGameOver();
        }
        else if (tempTeleAreaName == exitArea)
        {
            if (modeGame == ModeGame.Survivor)
            {
                if (modeSurvivor.timeCountDown >= timeForSurvive / 2.0f)
                {
                    timeScore = 20;
                }
                else
                {
                    float tempScore = ( 20 / (timeForSurvive / 2.0f) ) * modeSurvivor.timeCountDown;
                    timeScore = Mathf.RoundToInt(tempScore);
                }
                StartCoroutine(WaitForCalculate(1.5f));
            }
            else if (modeGame == ModeGame.Training)
            {
                SceneController.instanceScene.LoadMainMenu();
            }
        }
    }

    public void OnGameOver()
    {
        Debug.Log("GameOver");
        SceneController.instanceScene.LoadGameOver();
    }

    internal void OnDousedFire()
    {
        SceneController.instanceScene.LoadMainMenu();
    }

    private IEnumerator WaitForCalculate(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneController.instanceScene.LoadScoreScene();
    }
}
