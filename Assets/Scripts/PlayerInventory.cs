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

    [SerializeField] GameObject axePrefab;
    private Axe axeScript;

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

    }

    protected virtual void HandHoverUpdate(Hand hand)
    {
        Debug.Log("Hand");
        GrabTypes interactGrabType = hand.GetGrabStarting();
        if (interactGrabType == GrabTypes.Grip )
        {
            GameObject axeCreate = GameObject.Instantiate<GameObject>(axePrefab);
            axeCreate.transform.position = hand.transform.position;
            axeCreate.transform.rotation = hand.transform.rotation;
            axeScript = axeCreate.GetComponent<Axe>();
            
            //hand.HoverLock(axeScript.interactable);
            //hand.AttachObject(axeCreate, interactGrabType, axeScript.attachmentFlags, axeScript.axeOffset);

            //axeScript.handGrab = hand;
            //axeScript.isAttachHand = true;
            //axeScript.canDetachFromhand = true;
            //axeScript.isInventory = false;

        }
    }
    

}
