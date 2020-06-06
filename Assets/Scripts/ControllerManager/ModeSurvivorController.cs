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
    [SerializeField] UIButton alarmButton;
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
        alarmButton.buttonPushEvents.AddListener(OnAlarmPush);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAlarmPush()
    {

    }
}
