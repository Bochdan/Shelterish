using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIEvent : MonoBehaviour {

    private bool canBuild;
    private GenerateGrid grid;
    private Transform prefabToBuild;

    public Material greenMaterial;
    public Material defMaterial;

    private void Start()
    {
        grid = GenerateGrid.instance;
        canBuild = false;
    }

    public void ButtonAction(Transform prefab)
    {
        var unOccupiedElements = from element in grid.GetGrid
                                 where element.GetIsOccupied.Equals(false)
                                 select (element);


        foreach (var gridElement in unOccupiedElements)
        {
            var meshRenderer = gridElement.GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                meshRenderer.material = greenMaterial;
            }
        }

        canBuild = true;
        prefabToBuild = prefab;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canBuild)
        {
            canBuild = false;
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
            if (gridElement.GetIsOccupied.Equals(false))
            {
                Instantiate(prefabToBuild, clickPoint.transform);
                gridElement.SetIsOccupied = true;
            }
        }
        DeactivateUnUsed();
    }

    private void DeactivateUnUsed()
    {
        var unOccupiedElements = from element in GenerateGrid.instance.GetGrid
                                 where element.GetIsOccupied.Equals(false)
                                 select (element);


        foreach (var gridElement in unOccupiedElements)
        {
            var meshRenderer = gridElement.GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                meshRenderer.material = defMaterial;
            }
        }
    }
}
