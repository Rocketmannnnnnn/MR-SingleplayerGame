﻿using System.Collections;
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        HandleShooting();
        HandleTurret();
    }

    protected virtual void HandleShooting()
    {

        if (shellShootable && lineOfSight)
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

        if (this.player)
        {

            Vector3 direction = (this.player.transform.position - this.transform.position);
            Ray ray = new Ray(this.transform.position, direction);
        }

        yield return new WaitForSeconds(lineOfSightDelay);
        lineOfSight = false;
    }

    private void shootShell()
    {

        var shellToShoot = Instantiate(shell, turretEnd.position, turretEnd.rotation);
        shellToShoot.GetComponent<Rigidbody>().velocity = shellToShoot.transform.forward * shellSpeed;

        Destroy(shellToShoot, shellDespawnTime);
    }
}
