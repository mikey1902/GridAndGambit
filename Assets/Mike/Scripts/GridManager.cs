using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager: MonoBehaviour
{
    public List<GameObject> objectsOnGrid = new List<GameObject>();
    public List<Transform> nlist = new List<Transform>();
    public GameObject[,] gridCells;
    public GameObject cellPrefab;
    public int width = 8;
    public int height = 8;

 void Start()
    {
       createNodeGrid();
    }

   void createNodeGrid()
    {
        gridCells = new GameObject[width, height];
        Vector2 gridOffset = new Vector2(width / 2.0f - 0.5f, height / 2.0f - 0.5f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 gridPos = new Vector2(x, y);
                Vector2 spawnPos = gridPos - gridOffset;

                GameObject cell = Instantiate(cellPrefab, spawnPos, Quaternion.identity);
                cell.transform.SetParent(transform);

                cell.GetComponent<Node>().gridIndex = gridPos;
                cell.GetComponent<Node>().gcord = gridPos;

                if (x % 2 == 0){
                cell.GetComponent<Renderer>().material.color = y % 2 == 0 ? Color.black : Color.white;
                } else {
                cell.GetComponent<Renderer>().material.color = y % 2 == 0 ? Color.white : Color.black; 
                }
                cell.GetComponent<Node>().originalColor = cell.GetComponent<Renderer>().material.color;

                gridCells[x, y] = cell;
                nlist.Add(cell.transform);
            }
        }
    }

    public bool AddObjectToGrid(GameObject obj, Vector2 cellPos)
	{
        if (cellPos.x >= 0 && cellPos.x < width && cellPos.y >= 0 && cellPos.y < height)
        {
            //searches 2D array to find the cell
            Node cell = gridCells[(int)cellPos.x, (int)cellPos.y].GetComponent<Node>();

            if (cell.cellOccupied) return false;
			else
			{
                //spawns object and sets position to the chosen cell
                GameObject gridObj = Instantiate(obj, cell.GetComponent<Transform>().position, Quaternion.identity);
                gridObj.transform.SetParent(transform);

                //adds the object to the list and the object var in the cell
                objectsOnGrid.Add(gridObj);
                cell.objectInCell = gridObj;
                cell.cellOccupied = true;

                return true;
			}
        }
        else return false;
	}
}
