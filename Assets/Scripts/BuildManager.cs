using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    public static BuildManager instance;
    private GameObject turretToBuild;
    public Tile[,] tileArray;
    private const string towerName = "Tower";
    private int amountOfTowersBuilt;
    private int maxGridWidth;

    private void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogError("meer dan één buildmanagers!");
            return;
        }
        instance = this;
        amountOfTowersBuilt = 0;
    }

    private void Update()
    {
        if(turretToBuild != null)
        {
            Ray camray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camray, out RaycastHit hitInfo, 100f))
            {
                turretToBuild.transform.position = hitInfo.point;
                Debug.Log("TurretToBuild wordt aangeroepen");
            }
            if (Input.GetMouseButtonDown(0))
            {
                turretToBuild = null;
            }
        }
    }

    public void SetTowerToPlace(GameObject tower)
    {
        turretToBuild = Instantiate(tower, Vector3.zero, Quaternion.identity);
    }
    public GameObject standardTurretPrefab;

    private void Start()
    {
        //turretToBuild = standardTurretPrefab;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void placeTurret()
    {
        List<Vector2Int> directions=new List<Vector2Int>();
        directions.Add(new Vector2Int(0, 1));
        directions.Add(new Vector2Int(1, 0));
        directions.Add(new Vector2Int(-1, 0));
        directions.Add(new Vector2Int(0, -1));
        maxGridWidth = GameObject.Find("GameManager").GetComponent<PathManager>().gridwidth;
        for (int x =  Random.Range(0, maxGridWidth); x < tileArray.GetLength(0); x++)
        {
            for (int y = 0; y < tileArray.GetLength(1); y++)
            {
                if (tileArray[x, y].tileType == 0)
                {
                    for (int i = 0; i < directions.Count; i++)
                    {
                        if (IsInBoundOfArray(x + directions[i].x, y + directions[i].y))
                        {
                            if (tileArray[x + directions[i].x, y + directions[i].y].tileType == 1)
                            {
                                GameObject TempTower = Instantiate(standardTurretPrefab,new Vector3(x,0,y),Quaternion.identity);
                                TempTower.gameObject.name = towerName + amountOfTowersBuilt.ToString();
                                amountOfTowersBuilt += 1;
                                tileArray[x + directions[i].x, y + directions[i].y].tileType += 1;
                                return;
                            }

                        }
                    }
                }
            }
        }
    }

    private bool IsInBoundOfArray (int x,int y)
    {
        if (x < 0 || x > tileArray.GetLength(0))
        {
            return false;
        }
        else if (y < 0 || y > tileArray.GetLength(1))
        {
            return false;
        }
        return true;
    }
    
}
