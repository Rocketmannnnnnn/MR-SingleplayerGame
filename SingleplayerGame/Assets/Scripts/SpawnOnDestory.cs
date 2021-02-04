using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnOnDestory : MonoBehaviour
{
    public GameObject toSpawn;

    private void OnDestroy()
    {
        GameObject managerObject = GameObject.FindWithTag("GameController");
        if (managerObject) 
        {
            GameManager gm = managerObject.GetComponent<GameManager>();
            if (gm && !gm.loadingScene)
            {
                Instantiate(toSpawn, this.transform.position, this.transform.rotation);
            }
        }
    }
}
