using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIEvent : MonoBehaviour {

    public Material greenMaterial;
    public Material defMaterial;

    private bool canBuild;
    private GenerateGrid grid;
    private RoomElement prefabToBuild;
    public List<GridElement> horizontalAviableGrid = new List<GridElement>();
    public List<GridElement> veritcalAviableGrid = new List<GridElement>();
    private List<GridElement> aviableGrid = new List<GridElement>();

    private void Start()
    {
        grid = GenerateGrid.instance;
        canBuild = false;
        horizontalAviableGrid.Add(grid.GetGrid[0][0]);
    }

    public void ButtonAction(RoomElement prefab)
    {
        Switch(false);
        aviableGrid.Clear();

        if(prefab.horizontal == true)
        {
            aviableGrid.AddRange(horizontalAviableGrid);
        }

        if(prefab.vertical == true)
        {
            aviableGrid.AddRange(veritcalAviableGrid);
        }

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
                if (gridElement.GetIsOccupied.Equals(false) )
                {
                    canBuild = false;

                    var instancedRoom = Instantiate(prefabToBuild, clickPoint.transform);
                    gridElement.SetIsOccupied = true;
                    instancedRoom.SetGridParent = gridElement;
                    UpdateAviableGrid(instancedRoom);

                    ChangeMaterial(GetGridElements(false), defMaterial);
                    Switch(false);
                    aviableGrid.Clear();
                }
            }
        }
    }

    private void UpdateAviableGrid(RoomElement roomElement)
    {

        if (roomElement.horizontal == true)
        {
            foreach (var horizontalGrid in roomElement.GetHorizontalAviableGrid)
            {
                if (!horizontalAviableGrid.Contains(horizontalGrid))
                {
                    horizontalAviableGrid.Add(horizontalGrid);
                }
            }
        }

        if (roomElement.vertical == true)
        {
            foreach (var verticalGrid in roomElement.GetVerticalAviableGrid)
            {
                if (!veritcalAviableGrid.Contains(verticalGrid))
                {
                    veritcalAviableGrid.Add(verticalGrid);
                }
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
