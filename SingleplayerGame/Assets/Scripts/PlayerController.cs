using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Turret Properties")]
    public Transform turretTransform;
    public Transform turretEnd;

    [Header("Tank Properties")]
    private Rigidbody tankRigidbody;

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
        tankRigidbody = GetComponent<Rigidbody>();
        tankInput = GetComponent<TankInput>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
        HandleShooting();
        HandleTurret();
    }

    protected virtual void HandleMovement()
    {
        if(tankRigidbody && tankInput)
        {
            // forwards/backwards movement
            Vector3 movementPosition = transform.position + (transform.forward * tankInput.ForwardInput * tankMovementSpeed * Time.deltaTime);
            tankRigidbody.MovePosition(movementPosition);

            //rotation movement
            Quaternion movementRotation = transform.rotation * Quaternion.Euler(Vector3.up * (tankRotationSpeed * tankInput.RotationInput * Time.deltaTime));
            tankRigidbody.MoveRotation(movementRotation);

        }
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
