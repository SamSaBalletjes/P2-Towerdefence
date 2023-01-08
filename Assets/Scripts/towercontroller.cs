using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class towercontroller : MonoBehaviour
{
    public Transform towerTop;
    public Transform enemy;
    public GameObject entityRadius;


    // Update is called once per frame
    void Update()
    {
        
        //towerTop.LookAt(enemy);
        towerTop.rotation *= Quaternion.Euler(0, 300 * Time.deltaTime,0);
    }
}
