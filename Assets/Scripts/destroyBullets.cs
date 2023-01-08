using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyBullets : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //print("Iets komt er doorheen");
        Destroy(other.gameObject);
    }
}
