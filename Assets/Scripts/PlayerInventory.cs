using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class PlayerInventory : MonoBehaviour
{
    private Player player;
    private Transform playerHmdTranfrom;
    private float playerHeight = 0.0f;

    private Vector3 offsetBoxCollider;
    private BoxCollider boxInventory;

    [SerializeField] GameObject axePrefab;
    private Axe axeScript;
    internal int axeCount;

    Interactable interactable;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject.GetComponentInParent<Player>();
        interactable = GetComponent<Interactable>();
        playerHmdTranfrom = player.hmdTransform;
        playerHeight = playerHmdTranfrom.position.y;

        offsetBoxCollider = player.transform.position;
        boxInventory = this.gameObject.GetComponent<BoxCollider>();
        boxInventory.center = new Vector3(0.0f, playerHeight / 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        offsetBoxCollider = this.gameObject.transform.position;
        boxInventory.center = new Vector3(playerHmdTranfrom.position.x - offsetBoxCollider.x, playerHeight / 2.0f, playerHmdTranfrom.position.z - offsetBoxCollider.z);
        gameObject.transform.position = new Vector3(playerHmdTranfrom.position.x, 0, playerHmdTranfrom.position.z);
    }

    protected virtual void HandHoverUpdate(Hand hand)
    {
        GrabTypes interactGrabType = hand.GetGrabStarting();
        if (interactGrabType == GrabTypes.Grip )
        {
            if (axeCount > 0)
            {
                GameObject axeCreate = GameObject.Instantiate<GameObject>(axePrefab);
                axeScript = axeCreate.GetComponent<Axe>();
                Interactable axeInteractable = axeCreate.GetComponent<Interactable>();

                hand.HoverLock(axeInteractable);
                if (hand.handType == SteamVR_Input_Sources.LeftHand)
                {
                    hand.AttachObject(axeCreate, interactGrabType, axeScript.attachmentFlags, axeScript.axeOffsetL);
                }
                else if (hand.handType == SteamVR_Input_Sources.RightHand)
                {
                    hand.AttachObject(axeCreate, interactGrabType, axeScript.attachmentFlags, axeScript.axeOffsetR);
                }

                axeScript.handGrab = hand;
                axeScript.isAttachHand = true;
                axeScript.canDetachFromhand = true;

                axeCount--;
            }
        }
    }
    

}
