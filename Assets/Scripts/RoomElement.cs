using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomElement : MonoBehaviour {

    public bool horizontal = true;
    public bool vertical = false;

    private GridElement gridParent;

    public GridElement SetGridParent
    {
        set
        {
            gridParent = value;
        }
    }
    
    public List<GridElement> GetHorizontalAviableGrid
    {
        get
        {
            return gridParent.GetHorizontalNeighbors;
        }
    }

    public List<GridElement> GetVerticalAviableGrid
    {
        get
        {
            return gridParent.GetVerticalNeighbors;
        }
    }
}
