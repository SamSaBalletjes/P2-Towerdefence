using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.AI.Navigation;
using Unity.AI;
using UnityEditor;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private PathGenerator pathGenerator;
    public int gridwidth = 16;
    public int gridHeight = 8;
    public int minPathLength = 30;
    public GridCellObject[] gridCells;
    public GridCellObject[] sceneryCells;
    private GameObject PathHolder;
    private GameObject GridHolder;
    private List<NavMeshSurface> NavMeshSurfaces;
    private GameObject holder;
    private bool pathReady = false;
    private bool gridready = false;


    IEnumerator CreateGrid(List<Vector2Int> pathCells)
    {
        yield return StartCoroutine(LayPathCells(pathCells));
        yield return StartCoroutine(LaySceneryCells());
        //print("STARTING NAVMESH BUILDING");
        GridHolder.SetActive(false);
        BuildNavMesh();
        GridHolder.SetActive(true);

    }
    void Start()
    {
        holder = new GameObject("gameObjectHolder");
        GridHolder = new GameObject("gridHolder");
        PathHolder = new GameObject("pathHolder");
        NavMeshSurfaces = new List<NavMeshSurface>();
        pathGenerator = new PathGenerator(gridwidth, gridHeight);

        List<Vector2Int> pathCells = pathGenerator.GeneratePath();
        int pathSize = pathCells.Count;
        while (pathSize < minPathLength)
        {
            pathCells = pathGenerator.GeneratePath();
            pathSize = pathCells.Count;
        }
        AddNavMeshToGameObject(PathHolder);
        StartCoroutine(CreateGrid(pathCells));
        PathHolder.transform.parent = holder.transform;
        GridHolder.transform.parent = holder.transform;

    }


    private void BuildNavMesh()
    {
        foreach (var surface in NavMeshSurfaces)
        {
            var meshFilters = surface.GetComponents<MeshFilter>();

            bool pass = false;
            if (meshFilters != null)
            {
                foreach (var filter in meshFilters)
                {
                    if (!filter.sharedMesh.isReadable)
                    {
                        pass = true;

                    }
                }
            }
            if (!pass)
            {
                //Debug.Log("if surface.built");
                surface.BuildNavMesh();
            }
        }
    }
    private void AddNavMeshToGameObject(GameObject GO)
    {
        GameObjectUtility.SetStaticEditorFlags(GO, StaticEditorFlags.NavigationStatic);
        GO.AddComponent<NavMeshSurface>();
        NavMeshSurfaces.Add(GO.GetComponent<NavMeshSurface>());
    }


    private IEnumerator LayPathCells(List<Vector2Int> pathCells)
    {
        //PathHolder.transform.position = new Vector3(pathCells[1].x,0,pathCells[1].y);
        foreach (Vector2Int pathCell in pathCells)
        {
            int neighbourValue = pathGenerator.getCellNeighbourValue(pathCell.x, pathCell.y);
            //Debug.Log("Tile " + pathCell.x + ", " + pathCell.y + " neighbour value = " + neighbourValue);
            GameObject pathTile = gridCells[neighbourValue].cellPrefab;
            GameObject pathTileCell = Instantiate(pathTile, new Vector3(pathCell.x, 0f, pathCell.y), Quaternion.identity, PathHolder.transform);
            pathTileCell.transform.Rotate(0f, gridCells[neighbourValue].yRotation, 0f, Space.Self);
            pathTileCell.isStatic = true;
            yield return new WaitForSeconds(0.0f);
        }
        yield return null;
    }

    IEnumerator LaySceneryCells()
    {
        //Debug.Log("!!!Dit wordt aangeroepen!!!");
        for (int x = 0; x < gridwidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (pathGenerator.CellIsEmpty(x, y))
                {
                    int randomSceneryCellIndex = Random.Range(0, sceneryCells.Length);
                    Instantiate(sceneryCells[randomSceneryCellIndex].cellPrefab, new Vector3(x, 0f, y), Quaternion.identity, GridHolder.transform);
                    yield return new WaitForSeconds(0.0f);
                }
            }
        }
        yield return null;
    }
}
