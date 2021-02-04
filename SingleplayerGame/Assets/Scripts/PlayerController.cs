using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Turret Properties")]
    public Transform turretTransform;
    public Transform turretEnd;

    [Header("Input")]
    private TankInput tankInput;

    [Header("Shell Properties")]
    public GameObject shell;
    public int shellSpeed;
    public float shellDespawnTime;
    private bool shellShootable = true;
    public float shellFireRate;


    // Start is called before the first frame update
    void Start()
    {
        tankInput = GetComponent<TankInput>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleShooting();
        HandleTurret();
    }

    protected virtual void HandleTurret()
    {
        if(turretTransform)
        {
            Vector3 turretLookDirection = turretTransform.position - tankInput.ReticlePosition;
            turretLookDirection.y = 0f;

            turretTransform.rotation = Quaternion.LookRotation(turretLookDirection) ;
        }

    }

    protected virtual void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && shellShootable)
        {
            Debug.Log("PIEF");

            shellShootable = false;
            shootShell();
            StartCoroutine(ShootingYield());
        }
    }

    private IEnumerator ShootingYield()
    {
        yield return new WaitForSeconds(shellFireRate);
        shellShootable = true;
    }

    private void shootShell()
    {
        var shellToShoot = Instantiate(shell, turretEnd.position, turretEnd.rotation);
        shellToShoot.GetComponent<Rigidbody>().velocity = shellToShoot.transform.forward * shellSpeed;

        Destroy(shellToShoot, shellDespawnTime);
    }
}
