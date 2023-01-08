using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private PathGenerator pathGenerator;
    public int gridwidth = 16;
    public int gridHeight = 8;
    public int minPathLength = 30;
    public GridCellObject[] gridCells;
    public GridCellObject[] sceneryCells;
    void Start()
    {
        pathGenerator = new PathGenerator(gridwidth, gridHeight);
        
        List<Vector2Int> pathCells = pathGenerator.GeneratePath();
        int pathSize = pathCells.Count;
        while (pathSize < minPathLength)
        {
            pathCells = pathGenerator.GeneratePath();
            pathSize = pathCells.Count;
        }
        StartCoroutine(CreateGrid(pathCells));
        //StartCoroutine(LayPathCells(pathCells));
        //LaySceneryCells();
    }

    IEnumerator CreateGrid(List<Vector2Int> pathCells)
    {
        yield return LayPathCells(pathCells);
        yield return LaySceneryCells();
    }

    private IEnumerator LayPathCells(List<Vector2Int> pathCells)
    { 
        foreach (Vector2Int pathCell in pathCells)
        {
            int neighbourValue = pathGenerator.getCellNeighbourValue(pathCell.x, pathCell.y);
            //Debug.Log("Tile " + pathCell.x + ", " + pathCell.y + " neighbour value = " + neighbourValue);
            GameObject pathTile = gridCells[neighbourValue].cellPrefab;
            GameObject pathTileCell = Instantiate(pathTile, new Vector3(pathCell.x, 0f, pathCell.y), Quaternion.identity);
            pathTileCell.transform.Rotate(0f, gridCells[neighbourValue].yRotation, 0f, Space.Self);

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    IEnumerator LaySceneryCells()
    {
        Debug.Log("!!!Dit wordt aangeroepen!!!");
        for (int x = 0; x < gridwidth; x++)
        {
            for (int  y = 0;  y < gridHeight;  y++)
            {
                if (pathGenerator.CellIsEmpty(x, y))
                {
                    int randomSceneryCellIndex = Random.Range(0, sceneryCells.Length);
                    Instantiate(sceneryCells[randomSceneryCellIndex].cellPrefab, new Vector3(x, 0f, y), Quaternion.identity);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }

        yield return null;
    }
}
