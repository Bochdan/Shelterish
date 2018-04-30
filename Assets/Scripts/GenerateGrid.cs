using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour {

    public Transform gridPrefab;
    public int size;

	// Use this for initialization
	void Start () {
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
            Instantiate(gridPrefab, vex, gridPrefab.rotation);
        }
    }
}
