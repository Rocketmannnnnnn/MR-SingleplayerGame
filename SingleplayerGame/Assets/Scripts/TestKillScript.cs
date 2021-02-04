using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKillScript : MonoBehaviour
{
    public bool isPlayer = false;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P) && isPlayer)
        {
            Destroy(this.gameObject);
        }
        
        if(Input.GetKeyUp(KeyCode.E) && !isPlayer)
        {
            Destroy(this.gameObject);
        }
    }
}
