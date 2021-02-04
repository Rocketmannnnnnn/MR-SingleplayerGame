using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnExit : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.transform.root.gameObject);
    }
}
