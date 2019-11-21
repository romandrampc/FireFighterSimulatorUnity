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
    public static GameController instanceGame;

    private void Awake()
    {
        if (instanceGame != null && instanceGame != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instanceGame = this;
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
