using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour { 

    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard turret purchase");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }
}
