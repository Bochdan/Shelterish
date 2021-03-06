﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour {

    private bool isOccupied = false;
    private List<GridElement> verticalNeighbors = new List<GridElement>();
    private List<GridElement> horizontalNeighbors = new List<GridElement>();

    public bool GetIsOccupied
    {
        get
        {
            return isOccupied;
        }
    }

    public bool SetIsOccupied
    {
        set
        {
            isOccupied = value;

            if (value == true)
            {
                var meshRenderer = this.GetComponent<MeshRenderer>();

                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }
            }
        }
    }

    public List<GridElement> GetVerticalNeighbors { get { return verticalNeighbors; } }

    public List<GridElement> GetHorizontalNeighbors { get { return horizontalNeighbors; } }
}
