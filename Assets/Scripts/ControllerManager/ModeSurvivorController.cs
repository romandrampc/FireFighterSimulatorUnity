using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSurvivorController : MonoBehaviour
{
    internal static ModeSurvivorController instanceGameModeSurvivor;

    private GameController gameController;

    [Header("Main Quest Zone1")]
    [SerializeField] GameObject FireZone1;
    [SerializeField] MeshCollider DoorLock;
    int progessZone1 = 0;
    [SerializeField] int goalZone1 = 3;
    bool wasComZone1 = false;

    [Header("Main Quest Zone2")]
    [SerializeField] GameObject desk1;
    [SerializeField] GameObject desk2;
    [SerializeField] GameObject BlockingTeleportPart;
    int progessZone2 = 0;
    [SerializeField] int goalZone2 = 1;

    [Header("Side Quest Zone 1")]
    [SerializeField] UIButton alarmButton_Z1;

    [Header("Side Quest Zone 2")]
    [SerializeField] GameObject firePlug1_Z2;
    [SerializeField] GameObject fireBook1_Z2;
    [SerializeField] GameObject fireBook2_Z2;

    [Header("Side Quest Zone 3")]
    [SerializeField] GameObject fireBook1_Z3;
    [SerializeField] GameObject fireBook2_Z3;
    int progessFireExtra = 0;

    [Header("UI Array")]
    [SerializeField] GameObject[] UITutorial;

    internal float timeCountDown;
    bool canCountDown;
    private void Awake()
    {
        if (instanceGameModeSurvivor != null && instanceGameModeSurvivor != this)
        {
            Destroy(this.gameObject.GetComponent<ModeSurvivorController>());
        }
        else
        {
            instanceGameModeSurvivor = this;
        }
    }
    private void OnEnable()
    {
        gameController = GameController.instanceGame;
        alarmButton_Z1.buttonPushEvents.AddListener(OnAlarmPush);

        timeCountDown = gameController.timeForSurvive;
        canCountDown = true;
        wasComZone1 = false;

        foreach (GameObject ui in UITutorial)
        {
            if (ui.activeInHierarchy)
                ui.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject ui in UITutorial)
        {
            if (ui.activeInHierarchy)
                ui.SetActive(false);
        }
        #region Quest Zone1

        Debug.Log("1." +!FireZone1.activeInHierarchy);
        if (!FireZone1.activeInHierarchy && (progessZone1 & 1) == 0)
        {
            progessZone1 = progessZone1 | 1;
        }

        // example for complete quest
        if (goalZone1 == (progessZone1 & goalZone1))
        {
            Debug.Log("Zone 1 Complete");
            if (!wasComZone1)
            {
                DoorLock.enabled = true;
                wasComZone1 = true;
            }
        }

        #endregion

        #region Quest Zone2

        if ((!desk1.activeInHierarchy && !desk2.activeInHierarchy) && (progessZone2 & 1) == 0)
        {
            progessZone2 = progessZone2 | 1;
        }

        if (goalZone2 == (progessZone2 & goalZone2))
        {
            Debug.Log("Zone 2 Complete");
            gameController.useAxeScore = 5;
            BlockingTeleportPart.SetActive(false);
        }
        #endregion

        #region Side Quest Fire
        if (!firePlug1_Z2.activeInHierarchy && (progessFireExtra & 1) == 0)
        {
            gameController.douseFireScore += 2;
            progessFireExtra = progessFireExtra | 1;
        }

        if (!fireBook1_Z2.activeInHierarchy && (progessFireExtra & 2) == 0)
        {
            gameController.douseFireScore += 2;
            progessFireExtra = progessFireExtra | 2;
        }

        if (!fireBook2_Z2.activeInHierarchy && (progessFireExtra & 3) == 0)
        {
            gameController.douseFireScore += 2;
            progessFireExtra = progessFireExtra | 3;
        }

        if (!fireBook1_Z3.activeInHierarchy && (progessFireExtra & 4) == 0)
        {
            gameController.douseFireScore += 2;
            progessFireExtra = progessFireExtra | 4;
        }

        if (!fireBook2_Z3.activeInHierarchy && (progessFireExtra & 5) == 0)
        {
            gameController.douseFireScore += 2;
            progessFireExtra = progessFireExtra | 5;
        }
        #endregion

        #region timer 
        if (timeCountDown >0.0f && canCountDown)
        {
            timeCountDown -= Time.deltaTime;
        }
        else if (timeCountDown <= 0.0f)
        {
            canCountDown = false;
            gameController.OnGameOver();
        }
        #endregion
    }

    void OnAlarmPush()
    {
        gameController.pushAlarmPartScore = 5;
    }

    void OnFireCabinOpen()
    {
        progessZone1 = progessZone1 | 2;
    }
}
