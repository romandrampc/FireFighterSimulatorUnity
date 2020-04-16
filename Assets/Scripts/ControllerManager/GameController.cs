using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Training
}

public class GameController : MonoBehaviour
{
    public static GameController instanceGame;

    #region GameMode
    public ModeGame modeGame;

    ModeSurvivorController modeSurvivor;
    ModeTrainingController modeTraining;
    #endregion

    #region Player
    private float playerHeight = 0.0f;

    internal float PlayerHeight { get => playerHeight; set => playerHeight = value; }
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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        modeSurvivor = ModeSurvivorController.instanceGameModeSurvivor;
        modeTraining = ModeTrainingController.instanceGameModeTraining;
    }

    private void CheckStage()
    {
        // Check To Set Survivor Control Active
        if (modeGame == ModeGame.Survivor)
        {
            modeSurvivor.enabled = true;
        }
        else
        {
            modeSurvivor.enabled = false;
        }

        //Check To Set Training Control Active
        if (modeGame == ModeGame.Training)
        {
            modeTraining.enabled = true;
        }
        else
        {
            modeTraining.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckStage();
    }
}
