using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour 
{ 

    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandardTurret()
    {
        if (goedAntwoord.coinsGathered > 0)
        {
            buildManager.placeTurret();
            goedAntwoord.coinsGathered -= 1;
            print(goedAntwoord.coinsGathered);
        }
        else
        {
            print("No more Coins to buy!");
        }
    }
}
