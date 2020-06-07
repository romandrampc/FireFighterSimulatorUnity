using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    GameController gameController;

    [SerializeField] Text txtPushAlarmScore;
    [SerializeField] Text txtFireScore;
    [SerializeField] Text txtUseAxeScore;
    [SerializeField] Text txtTimeScore;
    private void Awake()
    {
        gameController = GameController.instanceGame;
    }
    // Start is called before the first frame update
    void Start()
    {
        txtPushAlarmScore.text = gameController.pushAlarmPartScore + " / 05";
        txtFireScore.text = gameController.douseFireScore + " / 10";
        txtUseAxeScore.text = gameController.useAxeScore + " / 05";
        txtTimeScore.text = gameController.timeScore + " / 20";
    }
}
