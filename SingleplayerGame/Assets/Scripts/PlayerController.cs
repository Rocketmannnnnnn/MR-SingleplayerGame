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
    public float shellFireDelay;

    [Header("Movement Properties")]
    public float tankMovementSpeed;
    public float tankRotationSpeed;

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

    protected virtual void HandleMovement()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal"), Time.deltaTime * tankRotationSpeed, 0);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * tankMovementSpeed);
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
        if (Input.GetMouseButton(0) && shellShootable)
        {
            shellShootable = false;
            shootShell();
            StartCoroutine(ShootingYield());
        }
    }

    private IEnumerator ShootingYield()
    {
        yield return new WaitForSeconds(shellFireDelay);
        shellShootable = true;
    }

    private void shootShell()
    {
        var shellToShoot = Instantiate(shell, turretEnd.position, turretEnd.rotation);
        shellToShoot.GetComponent<Rigidbody>().velocity = shellToShoot.transform.forward * shellSpeed;

        Destroy(shellToShoot, shellDespawnTime);
    }
}
