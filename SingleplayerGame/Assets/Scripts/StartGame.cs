using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public string playerTag = "Player";
    public string enemyTag = "Enemy";
    public GameObject introPanel;

    private void Awake()
    {
        SetTanksActive(false);
    }

    public void SetTanksActive(bool enable)
    {
        List<GameObject> tanks = new List<GameObject>();
        tanks.AddRange(GameObject.FindGameObjectsWithTag(this.playerTag));
        tanks.AddRange(GameObject.FindGameObjectsWithTag(this.enemyTag));
        foreach(GameObject tank in tanks)
        {
            foreach(MonoBehaviour mono in tank.GetComponentsInChildren<MonoBehaviour>())
            {
                mono.enabled = enable;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SetTanksActive(true);
            this.introPanel.SetActive(false);
            this.enabled = false;
        }
    }
}
