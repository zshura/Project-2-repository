using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject cubePrefab;
	Vector3 cubePosition;  
	public static GameObject selectedCube;
	public static GameObject activeCube;
	int airplaneX, airplaneY; 
	GameObject [,] grid;
	int gridY, gridX;
	bool activePlane;

	public void ProcessClick (GameObject clickedCube, int x, int y) {
		//when we click if active, deactivate
		if (x == airplaneX && y == airplaneY && activePlane) {
			activePlane = false;
			clickedCube.GetComponent<Renderer> ().material.color = Color.red;
		}
			//if we click the airplane, activate it
		else if (x == airplaneX && y == airplaneY && !activePlane) {
			activePlane = true;
			clickedCube.GetComponent<Renderer> ().material.color = Color.yellow;
		}
		//Clicks a sky block
		else {
			if (activePlane) {
				//remove plane from its old spot
				grid[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
				//add airplane to new spot
				airplaneX = x;
				airplaneY = y;
				clickedCube.GetComponent<Renderer> ().material.color = Color.yellow;
			}
		}
	}

	// Use this for initialization
	void Start () {
		gridX = 16;
		gridY = 9;
		grid = new GameObject[gridX, gridY];
		//create the 9 by 16 grid of cubes
		for (int y = 0; y < gridY; y++) {
			for (int x = 0; x < gridX; x++) {
				cubePosition = new Vector3 (x * 2, y * 2, 0);
				grid [x, y] = Instantiate (cubePrefab, cubePosition, Quaternion.identity);
				grid [x, y].GetComponent<CubeController> ().myX = x;
				grid [x, y].GetComponent<CubeController> ().myY = y;
			}
		}
		//starting location of airplane
		airplaneX = 0;
		airplaneY = 8; 
		grid [airplaneX, airplaneY].GetComponent<Renderer> ().material.color = Color.red;
		activePlane = false;

	}

	// Update is called once per frame
	void Update () {
	}
}
