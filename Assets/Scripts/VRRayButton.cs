using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRRayButton : MonoBehaviour
{
    public UnityEvent callBack;
    
    public void OnAction()
    {
        if (callBack != null)
        {
            callBack.Invoke();
        }
    }
    
}
