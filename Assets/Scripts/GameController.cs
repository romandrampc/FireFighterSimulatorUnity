using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfFlame
{
    Wood,
    Electronic
}

public class GameController : MonoBehaviour
{
    

    public static GameController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
