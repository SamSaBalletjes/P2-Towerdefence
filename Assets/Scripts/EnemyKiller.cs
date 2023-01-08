using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball") == true)
        {
            enemyDetector enemyDetector = GameObject.Find(other.gameObject.transform.parent.gameObject.name).GetComponent<enemyDetector>();
            if (enemyDetector != null)
            {
                enemyDetector.removeFromList(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
