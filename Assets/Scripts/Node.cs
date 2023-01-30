using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;
    private GameObject turret;
    
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Hier staat al een Turret!");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();

        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
        Debug.Log("Er wordt geklikt!");
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
