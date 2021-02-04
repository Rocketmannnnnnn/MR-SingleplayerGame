using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankInput : MonoBehaviour
{

    #region Attributes
    public Camera camera;

    private Vector3 reticlePosition;

    public Vector3 ReticlePosition
    {
        get { return reticlePosition; }
    }

    private Vector3 reticleNormal;
    public Vector3 ReticleNormal
    {
        get { return reticleNormal; }
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
            reticleNormal = hit.normal;
        }
    }

    #endregion
}
