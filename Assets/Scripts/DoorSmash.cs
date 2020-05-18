using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSmash : MonoBehaviour
{
    [SerializeField] Transform doorPivot;
    [SerializeField] float targetAngle =80.0f;
    [SerializeField] float speedRotate = 0.1f;

    bool wasSmash = false;

    
    // Start is called before the first frame update
    void Start()
    {
        wasSmash = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SmashButt();

        if (wasSmash)
            doorPivot.rotation = Quaternion.Lerp(doorPivot.rotation, Quaternion.Euler(doorPivot.rotation.eulerAngles.x, targetAngle, doorPivot.rotation.eulerAngles.z), Time.deltaTime * speedRotate);
        
    }

    void SmashButt()
    {
        wasSmash = true;
    }
}
