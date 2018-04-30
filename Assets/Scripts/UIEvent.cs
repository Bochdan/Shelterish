using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIEvent : MonoBehaviour {

    private bool canBuild;
    private GenerateGrid grid;
    private Transform prefabToBuild;
    private List<GridElement> aviableGrid = new List<GridElement>();

    public Material greenMaterial;
    public Material defMaterial;

    private void Start()
    {
        grid = GenerateGrid.instance;
        canBuild = false;
        aviableGrid.Add(grid.GetGrid[0][0]);
    }

    public void ButtonAction(Transform prefab)
    {
        Switch(true);
        ChangeMaterial(GetGridElements(false), greenMaterial);

        canBuild = true;
        prefabToBuild = prefab;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canBuild)
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceCubeNear(hitInfo.collider);
            }
        }
    }

    private void PlaceCubeNear(Collider clickPoint)
    {
        var gridElement = clickPoint.gameObject.GetComponent<GridElement>();
        if (gridElement != null)
        {
            if (aviableGrid.Contains(gridElement))
            {
                if (gridElement.GetIsOccupied.Equals(false))
                {
                    canBuild = false;

                    Instantiate(prefabToBuild, clickPoint.transform);
                    gridElement.SetIsOccupied = true;
                    UpdateAviableGrid(gridElement);

                    ChangeMaterial(GetGridElements(false), defMaterial);
                    Switch(false);
                }
            }
        }
    }

    private void UpdateAviableGrid(GridElement gridElement)
    {
        foreach(var gridNeighbor in gridElement.GetNeighbors)
        {
            if (!aviableGrid.Contains(gridNeighbor))
            {
                aviableGrid.Add(gridNeighbor);
            }
        }
    }

    private void Switch(bool switchTo)
    {
        foreach(var grid in aviableGrid)
        {
            if (grid.GetIsOccupied.Equals(false))
            {
                var meshRenderer = grid.GetComponent<MeshRenderer>();

                if (meshRenderer != null)
                {
                    meshRenderer.enabled = switchTo;
                }
            }
        }
    }

    private IEnumerable<GridElement> GetGridElements(bool state)
    {
        return from element in aviableGrid
               where element.GetIsOccupied.Equals(state)
               select (element); ;
    }

    private void ChangeMaterial(IEnumerable<GridElement> gridElements, Material material)
    {
        if (gridElements != null)
        {
            foreach (var gridElement in gridElements)
            {
                var meshRenderer = gridElement.GetComponent<MeshRenderer>();

                if (meshRenderer != null)
                {
                    meshRenderer.material = material;
                }
            }
        }
    }
}
