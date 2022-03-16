using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public static AI instance;

    private bool canPlay = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Update()
    {
        if (canPlay)
        {
            GameManagerScript.instance.AIPlay();
            canPlay = false;
        }
    }

    public void ChangeTurn()
    {
        if (canPlay)
        {
            canPlay = false;
        }
        else
        {
            canPlay = true;
        }
    }
}
