using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridCell", menuName = "TowerDefence/Grid Cell")]
public class GridCellObject : ScriptableObject
{
    public enum CellType { path, Ground}
    public CellType cellType;
    public GameObject cellPrefab;
    public int yRotation;
}
