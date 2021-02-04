using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDriver : MonoBehaviour
{

    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 destination;

    public  string playerTag   = "Player";
    private string setPath     = "SetPath";
    public  float  searchDelay = 0.5f;

    // The awake function is triggerd before the start
    void Awake()
    {
        this.agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find(this.playerTag);
        StartCoroutine(this.setPath);
    }

    IEnumerator SetPath()
    {
        while (true)
        {

            if (this.player && this.player.transform.position != destination)
            {
                this.destination = this.player.transform.position;
                this.agent.SetDestination(destination);
            }
            yield return new WaitForSeconds(this.searchDelay);
        }
    }
}
