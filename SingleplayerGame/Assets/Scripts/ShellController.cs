using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {

        Destroy(collision.transform.root.gameObject);
        Destroy(this.transform.root.gameObject);
    }
}
