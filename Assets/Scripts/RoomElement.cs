using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomElement : MonoBehaviour {

    public bool horizontal = true;
    public bool vertical = false;

    private GridElement gridParent;
    public List<GridElement> aviableGrid = new List<GridElement>();

    public GridElement SetGridParent
    {
        set
        {
            gridParent = value;

            if(horizontal == true)
            {
                aviableGrid.AddRange(gridParent.GetHorizontalNeighbors);
            }

            if(vertical == true)
            {
                aviableGrid.AddRange(gridParent.GetVerticalNeighbors);
            }
        }
    }
    
    public List<GridElement> GetAviableGrid
    {
        get
        {
            return aviableGrid;
        }
    }
}
