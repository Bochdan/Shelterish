using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour {

    private bool isOccupied = false;
	
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
}
