using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyDetector : MonoBehaviour
{
    public Transform towerTop;
    public GameObject bullet;
    public Transform bulletShooter;
    private Vector3 position;
    [Range(0.1f,3f)]
    public float fireRate;
    private float timer;
    public List<GameObject> enemies= new List<GameObject>();
    public int firePower;
    private void Awake()
    {
        //fireRate = 1f;
        timer = 0.00f;
    }

    private void Start()
    {
        //InvokeRepeating("OnTriggerStay", 5, 5);
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    //print("dit werkt");
    //    towerTop.LookAt(enemy);
    //    Vector3 position = bulletShooter.position;
    //    Instantiate(bullet, position, Quaternion.identity);

    //}enemies[0].transform + enemies[0].transform.forward

    private void Update()
    {
        if (enemies.Count > 0)
        {
            if (enemies[0] != null)
            {
                towerTop.LookAt(enemies[0].transform);
                if (timer >= fireRate)
                {
                    position = bulletShooter.position;
                    Quaternion rotation = bullet.transform.rotation;
                    GameObject ball = Instantiate(bullet, position, Quaternion.identity);
                    ball.transform.parent = transform;
                    ball.transform.LookAt(enemies[0].transform.position + (enemies[0].transform.forward * 5));
                    Rigidbody bulletBody = ball.GetComponent<Rigidbody>();
                    bulletBody.AddForce(ball.transform.forward * firePower, ForceMode.Impulse);
                    timer = 0.00f;
                }
            }
            
        }
        timer += Time.deltaTime;
    }

    public void removeFromList(GameObject GO)
    {
        enemies.Remove(GO);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            print($"{other.gameObject} has entered the area");
            enemies.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
            print($"{other.gameObject} has left the area");
        }
    }
}

