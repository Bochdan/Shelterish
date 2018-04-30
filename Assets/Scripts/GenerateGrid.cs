using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerateGrid : MonoBehaviour {

    public GridElement gridPrefab;
    public float size;
    public Material greenMaterial;

    public static GenerateGrid instance;

    private List<GridElement> grid = new List<GridElement>();

    private void Awake()
    {
        instance = this;
        MakeGrid();
    }

	// Update is called once per frame
	void Update () {
		
	}

    private void MakeGrid()
    {
        for(int i = 0; i < 10; i++)
        {
            var xpos = size * i;
            var vex = Vector3.zero;
            vex.x = xpos;
            var gridElement = Instantiate(gridPrefab, vex, gridPrefab.transform.rotation);
            grid.Add(gridElement);
        }
    }

    public List<GridElement> GetGrid
    {
        get
        {
            return grid;
        }
    }
}
