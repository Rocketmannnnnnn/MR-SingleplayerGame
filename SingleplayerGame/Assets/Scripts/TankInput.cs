using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankInput : MonoBehaviour
{

    #region Attributes
    [Header("Camera Properties")]
    public Camera camera;


    [Header("Mouse Location Properties")]
    private Vector3 reticlePosition;

    public Vector3 ReticlePosition
    {
        get { return reticlePosition; }
    }


    [Header("Movement Properties")]
    private float forwardInput;
    public float ForwardInput
    {
        get { return forwardInput; }
    }

    private float rotationInput;
    public float RotationInput
    {
        get { return rotationInput; }
    }
    #endregion

    #region Built in Methods
    // Update is called once per frame
    void Update()
    {
        if(camera)
        {
            HandleInputs();
        }
    }
    #endregion

    #region Custom Methods

    protected virtual void HandleInputs()
    {
        Ray screenRay = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(screenRay, out hit))
        {
            reticlePosition = hit.point;
        }

        forwardInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");
    }

    #endregion
}
