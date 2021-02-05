using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{

    private GameObject player;
    private bool lineOfSight = false;

    [Header("Shoots At")]
    public string shootAtTag = "Player";
    public float lineOfSightDelay;

    [Header("Turret Properties")]
    public Transform turretTransform;
    public Transform turretEnd;

    [Header("Shell Properties")]
    public GameObject shell;
    public int shellSpeed;
    public float shellDespawnTime;
    private bool shellShootable = true;
    public float shellFireDelay;

    // Start is called before the first frame update
    void Start()
    {

        this.player = GameObject.FindGameObjectWithTag(this.shootAtTag);
        StartCoroutine(InLineOfSight());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        HandleShooting();
        HandleTurret();
    }

    protected virtual void HandleShooting()
    {

        if (shellShootable && lineOfSight && this.player)
        {

            shellShootable = false;
            lineOfSight = false;

            shootShell();
            StartCoroutine(ShootingYield());
        }
    }

    protected virtual void HandleTurret()
    {

        if (this.player == null)
            this.player = GameObject.FindGameObjectWithTag(this.shootAtTag);

        if (turretTransform && this.player)
        {

            Vector3 turretLookDirection = turretTransform.position - this.player.transform.position;
            turretLookDirection.y = 0f;

            turretTransform.rotation = Quaternion.LookRotation(turretLookDirection);
        }
    }

    private IEnumerator ShootingYield()
    {

        yield return new WaitForSeconds(shellFireDelay);
        shellShootable = true;
    }

    private IEnumerator InLineOfSight()
    {

        while (true)
        {

            if (this.player)
            {

                Vector3 position = this.transform.position;
                Vector3 direction = (this.player.transform.position - this.transform.position);

                direction.y += 1;
                position.y += 1;

                Ray ray = new Ray(position, direction);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 500.0f))
                {

                    lineOfSight = (hit.collider.transform.root.gameObject == this.player);
                    Debug.Log("Is in line of sight: " + lineOfSight);
                }
            }

            yield return new WaitForSeconds(lineOfSightDelay);
        }
    }

    private void shootShell()
    {

        var shellToShoot = Instantiate(shell, turretEnd.position, turretEnd.rotation);
        shellToShoot.GetComponent<Rigidbody>().velocity = shellToShoot.transform.forward * shellSpeed;

        Destroy(shellToShoot, shellDespawnTime);
    }
}
