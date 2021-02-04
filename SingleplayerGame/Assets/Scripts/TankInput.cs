using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankInput : MonoBehaviour
{

    #region Attributes
    private Camera camera;
    private Vector3 reticlePosition;
    public string cameraTag = "MainCamera";

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

    private void Start()
    {
        this.camera = GameObject.FindWithTag(this.cameraTag).GetComponent<Camera>();
    }

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
