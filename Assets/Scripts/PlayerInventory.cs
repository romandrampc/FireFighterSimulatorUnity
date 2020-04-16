using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerInventory : MonoBehaviour
{
    private Player player;
    private Transform playerHmdTranfrom;
    private float playerHeight = 0.0f;

    private Vector3 offsetBoxCollider;
    private BoxCollider boxInventory;

    private GameObject axeObject;
    private Axe axeScript;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject.GetComponent<Player>();
        playerHmdTranfrom = player.hmdTransform;
        playerHeight = playerHmdTranfrom.position.y;

        offsetBoxCollider = this.gameObject.transform.position;
        boxInventory = this.gameObject.GetComponent<BoxCollider>();
        boxInventory.center = new Vector3(0.0f, playerHeight / 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        boxInventory.center = new Vector3(playerHmdTranfrom.position.x - offsetBoxCollider.x, playerHeight / 2.0f, playerHmdTranfrom.position.z - offsetBoxCollider.z);

    }
    
    void PickUp(GameObject tempAxe)
    {
        axeObject = tempAxe;
        axeScript = axeObject.GetComponent<Axe>();
    }

    protected virtual void HandHoverUpdate(Hand hand)
    {
        Debug.Log("Hand");
        GrabTypes interactGrabType = hand.GetGrabStarting();
        if (interactGrabType == GrabTypes.Grip || Input.GetKey(KeyCode.R))
        {
            //set axe active
            axeObject.gameObject.SetActive(true);

            // Call this to continue receiving HandHoverUpdate messages,
            // and prevent the hand from hovering over anything else
            hand.HoverLock(axeScript.interactable);

            // Attach axe to the hand
            hand.AttachObject(axeObject.gameObject, interactGrabType, axeScript.attachmentFlags, axeScript.axeOffset);

            axeScript.handGrab = hand;
            axeScript.isAttachHand = true;
            axeScript.canDetachFromhand = true;
        }
    }

}
