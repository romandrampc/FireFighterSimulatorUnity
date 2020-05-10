using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class RayForMainMenu : MonoBehaviour
{
    public LayerMask uiInteractMask;

    public LineRenderer hitScanMenuLine;

    public GameObject PivotRay;

    public RaycastHit shootHit;

    public SteamVR_Input_Sources handType;

    public SteamVR_Action_Boolean grabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    [SerializeField] float range = 2.0f;

    Ray shootRay = new Ray();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowHitScan();
    }

    public void ShowHitScan()
    {
        if (hitScanMenuLine != null)
        {
            shootRay.origin = PivotRay.transform.position;
            shootRay.direction = PivotRay.transform.forward;
            if (Physics.Raycast(shootRay, out shootHit, range, uiInteractMask))
            {
                hitScanMenuLine.SetPosition(0, PivotRay.transform.position);
                hitScanMenuLine.SetPosition(1, shootHit.point);
            }
            else
            {
                hitScanMenuLine.SetPosition(0, PivotRay.transform.position);
                hitScanMenuLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }

            if (grabPinchAction.GetStateDown(handType))
            {
                shootHit.collider.gameObject.SendMessage("OnAction", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
