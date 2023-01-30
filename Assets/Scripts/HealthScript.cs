using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    public TMP_Text healthText;
    public int TotalHealth;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") == true)
        {
            //print($"Totalhealth = " + TotalHealth);
            TotalHealth = TotalHealth - 1;
            //print($"Totalhealth = " + TotalHealth);
            Destroy(other.gameObject);
            healthText.SetText("Health: " + TotalHealth);

            if (TotalHealth == 0)
            {
                SceneManager.LoadScene("Main menu");
            }
        }        
    }
}
