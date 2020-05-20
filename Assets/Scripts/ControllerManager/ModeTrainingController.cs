using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeTrainingController : MonoBehaviour
{
    internal static ModeTrainingController instanceGameModeTraining;

    private GameController gameController;

    [Header("Zone1")]
    [SerializeField] GameObject FireZone1;
    [SerializeField] MeshCollider DoorLock;
    int progessZone1=0;
    [SerializeField] int goalZone1 = 3;
    bool wasComZone1 = false;

    [Header("Zone2")]
    [SerializeField] GameObject desk1;
    [SerializeField] GameObject desk2;
    [SerializeField] GameObject BlockingTeleportPart;
    int progessZone2 = 0;
    [SerializeField] int goalZone2 = 1;



    private void Awake()
    {
        if (instanceGameModeTraining != null && instanceGameModeTraining != this)
        {
            Destroy(this.gameObject.GetComponent<ModeTrainingController>());
        }
        else
        {
            instanceGameModeTraining = this;
        }
    }

    private void OnEnable()
    {
        gameController = GameController.instanceGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Quest Zone1

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
            BlockingTeleportPart.SetActive(false);
        }
        #endregion
    }

    void OnFireCabinOpen()
    {
        progessZone1 = progessZone1 | 2;
    }
}
