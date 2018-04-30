using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerateGrid : MonoBehaviour {

    public GridElement gridPrefab;
    public Vector2 numberOfElements;
    public float size;

    public static GenerateGrid instance;

    private List<List<GridElement>> grid = new List<List<GridElement>> ();

    private void Awake()
    {
        instance = this;
        MakeGrid();
    }

	void Update () {
		
	}

    private void MakeGrid()
    {

        List<GridElement> gridElements;
        for (int i = 0; i < numberOfElements.y; i++)
        {
            gridElements = new List<GridElement>();

            var ypos = size * i;

            for (int j = 0; j < numberOfElements.x; j++)
            {

                var xpos = size * j;
                var gridElement = Instantiate(gridPrefab, new Vector3(xpos, ypos, 0), gridPrefab.transform.rotation);
                gridElements.Add(gridElement);

                var mesh = gridElement.GetComponent<MeshRenderer>();
                if (mesh != null)
                    mesh.enabled = false;


                if (j > 0)
                {
                    gridElement.GetNeighbors.Add(gridElements[(j - 1)]);
                    gridElements[j - 1].GetNeighbors.Add(gridElement);
                }

                
               if(i > 0)
                {
                    gridElement.GetNeighbors.Add(grid[i - 1][j]);
                    grid[i - 1][j].GetNeighbors.Add(gridElement);
                }
                
            }

            grid.Add(gridElements);
        }
    }

    public List<List<GridElement>> GetGrid
    {
        get
        {
            return grid;
        }
    }
}
