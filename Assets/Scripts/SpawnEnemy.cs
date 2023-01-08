using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject Enemys;
    public Transform SpawnLocation;
    public int spawntime;
    private void Start()
    {
        InvokeRepeating("spawnEnemy", 1, spawntime);
    }
    private void spawnEnemy()
    {
        Vector3 position = SpawnLocation.position;
        Instantiate(Enemys, position, Quaternion.identity);
    }
}

