using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Axe : MonoBehaviour
{
    #region Grabable
    [Header("SteamVR Hand Setting")]
    [EnumFlags]
    [Tooltip("The flags used to attach this object to the hand.")]
    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;

    [Tooltip("How fast must this object be moving to attach due to a trigger hold instead of a trigger press? (-1 to disable)")]
    public float catchingSpeedThreshold = -1;
    
    [HideInInspector]
    public Interactable interactable;

    [Tooltip("The local point which acts as a positional and rotational offset to use while held with a grip type grab")]
    [SerializeField] internal Transform axeOffset;
    [SerializeField] internal Transform pickAxeOffset;
    [SerializeField] internal Transform buttAxeOffset;
    
    internal Hand handGrab;
    internal bool canDetachFromhand;

    internal bool isAttachHand;
    protected bool attached = false;
    
    #endregion
    
    [Header("Axe Settings")]    
    [SerializeField] float radiusButtAxe = 0.07f;
    [SerializeField] float radiusPickAxe = 0.1f;
    [SerializeField] float minDegreeSmash = 80.0f;
    [SerializeField] float maxDegreeSmash = 100.0f;

    Vector3 StartPointButtAxe = Vector3.zero;
    Vector3 StartPointPickAxe = Vector3.zero;
    bool checkToDraw = false;
    internal bool isInventory = false;

    // Start is called before the first frame update
    void Start()
    {
        #region GrabableStart
        interactable = GetComponent<Interactable>();

        isAttachHand = false;
        canDetachFromhand = false;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttachHand && (handGrab.grabPinchAction.GetState(handGrab.handType) || Input.GetKey(KeyCode.R)))
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            gameObject.SendMessage("SmashWithButt", SendMessageOptions.DontRequireReceiver);
           
        }
        else
        {
            checkToDraw = false;
        }

        if (isAttachHand)
        {
            gameObject.SendMessage("SmashWithPick", SendMessageOptions.DontRequireReceiver);
        }
        
    }

    void SmashWithPick()
    {
        StartPointPickAxe = pickAxeOffset.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(StartPointPickAxe, radiusPickAxe);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("SmashObject"))
            {
                // this part to destroy object that 
            }
        }
    }

    void SmashWithButt()
    {
        
        Transform tranfromParent = transform.parent.gameObject.transform;
        checkToDraw = true;
        StartPointButtAxe = buttAxeOffset.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(StartPointButtAxe, radiusButtAxe);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Door"))
            {
                if((tranfromParent.rotation.x < minDegreeSmash && tranfromParent.rotation.x > maxDegreeSmash ) || (tranfromParent.rotation.x < minDegreeSmash * -1 && tranfromParent.rotation.x > maxDegreeSmash * -1))
                {
                    // this part to open door with axe
                }
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        if (checkToDraw)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawSphere(StartPointButtAxe, radiusButtAxe);
        }
        //Gizmos.color = new Color(3, 4, 4, 0.5f);
        //Gizmos.DrawSphere(StartPointPickAxe, radiusPickAxe);
        
    }

    protected virtual void OnHandHoverBegin(Hand hand)
    {


    }

    protected virtual void HandHoverUpdate(Hand hand)
    {
        GrabTypes interactGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if (interactable.attachedToHand == null && interactGrabType == GrabTypes.Grip)
        {
            // Call this to continue receiving HandHoverUpdate messages,
            // and prevent the hand from hovering over anything else
            hand.HoverLock(interactable);

            // Attach this object to the hand
            hand.AttachObject(gameObject, interactGrabType, attachmentFlags, axeOffset);

            handGrab = hand;
            isAttachHand = true;
        }

        if (isGrabEnding && isAttachHand)
        {
            canDetachFromhand = true;
        }

        if (canDetachFromhand && interactGrabType == GrabTypes.Grip)
        {
            handGrab = null;
            isAttachHand = false;
            canDetachFromhand = false;
            
            Vector3 startPointSphere = axeOffset.transform.position;
            Collider[] colliders = Physics.OverlapSphere(startPointSphere, radiusPickAxe * 2);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    isInventory = true;
                    collider.SendMessage("PickUp", this.gameObject, SendMessageOptions.DontRequireReceiver);
                    gameObject.SetActive(false);
                }
            }

            if (!isInventory)
            {
                // Detach this object from the hand
                hand.DetachObject(gameObject);

                // Call this to undo HoverLock
                hand.HoverUnlock(interactable);
            }
        }

    }
}
