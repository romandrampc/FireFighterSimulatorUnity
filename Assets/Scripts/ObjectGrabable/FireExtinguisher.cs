using UnityEngine;
using Valve.VR.InteractionSystem;


public class FireExtinguisher : MonoBehaviour
{
    #region Grabable
    [Header("SteamVR Hand Setting")]
    [EnumFlags]
    [Tooltip("The flags used to attach this object to the hand.")]
    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;

    [Tooltip("How fast must this object be moving to attach due to a trigger hold instead of a trigger press? (-1 to disable)")]
    public float catchingSpeedThreshold = -1;

    [Tooltip("The local point which acts as a positional and rotational offset to use while held with a grip type grab")]
    public Transform gripOffset;


    private Hand handGrab;
    private bool canDetachFromhand;

    protected bool isAttachHand;
    protected bool attached = false;

    [HideInInspector]
    public Interactable interactable;
    #endregion

    [Header("Fire Extinguisher Settings")]
    public ParticleSystem foam;
	[SerializeField] GameObject pivotHose;
    public TypeOfFlame typeOfExtinToDestroyFlame;
	
	[SerializeField] float damageFire = 20.0f;
	[SerializeField] float radius;
	[SerializeField] float range;
	

	Vector3 startPoint = Vector3.zero;
	Vector3 endPoint = Vector3.zero;

	private void Start()
	{
        #region GrabableStart
        interactable = GetComponent<Interactable>();

        isAttachHand = false;
        canDetachFromhand = false;
        #endregion
    }

    private void Update()
    {

        if (isAttachHand && (handGrab.grabPinchAction.GetState(handGrab.handType) || Input.GetKey(KeyCode.R)))
        {
            gameObject.SendMessage("PlayParticle", SendMessageOptions.DontRequireReceiver);
            gameObject.SendMessage("DouseToFire", SendMessageOptions.DontRequireReceiver);

        }
        else
        {
            gameObject.SendMessage("StopParticle", SendMessageOptions.DontRequireReceiver);
        }
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
            hand.AttachObject(gameObject, interactGrabType, attachmentFlags, gripOffset);

            handGrab = hand;
            isAttachHand = true;

        }

        if (isGrabEnding && isAttachHand)
        {
            canDetachFromhand = true;
        }

        if (canDetachFromhand && interactGrabType == GrabTypes.Grip)
        {
            // Detach this object from the hand
            hand.DetachObject(gameObject);

            // Call this to undo HoverLock
            hand.HoverUnlock(interactable);

            //// Restore position/rotation
            //transform.position = oldPosition;
            //transform.rotation = oldRotation;

            handGrab = null;
            isAttachHand = false;
            canDetachFromhand = false;
        }

    }

    public void PlayParticle()
    {
        if (!foam.isPlaying)
        {
            foam.Play();
        }
		
	}

	public void StopParticle()
	{
		foam.Stop();
	}

    private void DouseToFire()
    {
        startPoint = pivotHose.transform.position;
        endPoint = pivotHose.transform.position + (pivotHose.transform.forward * range);

        Collider[] hitColliders = Physics.OverlapCapsule(startPoint, endPoint, radius);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Fire"))
            {
                TypeOfFlame tempTypeOfFlame = collider.GetComponent<FireProperty>().typeOfFlames;
                if (typeOfExtinToDestroyFlame == tempTypeOfFlame)
                {
                    collider.gameObject.SendMessage("DouseFire", damageFire,SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        
    }
}
