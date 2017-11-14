using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public GameObject cubePrefab;
	public Text playerScoreText;
	public Text cargoText;
	Vector3 cubePosition;  
	public static GameObject selectedCube;
	public static GameObject activeCube;
	int airplaneX, airplaneY, bayX, bayY, depotX, depotY; 
	GameObject [,] grid;
	int gridY, gridX;
	bool activePlane;
	float turnTime, takeATurn;
	int planeCargo;
	int planeMaxHold;
	int cargoLoad;
	int playerScore;

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
				//keep depot black
				if (airplaneX == depotX && airplaneY == depotY) {
					grid[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.black;
				}
				//add airplane to new spot
				airplaneX = x;
				airplaneY = y;
				clickedCube.GetComponent<Renderer> ().material.color = Color.yellow;
			}
		}
	}

	// Use this for initialization
	void Start () {
		turnTime = 1.5f;
		takeATurn = turnTime;
		playerScore = 0;
		SetPlayerScore ();
		planeCargo = 0;
		SetCargo ();
		planeMaxHold = 90;
		cargoLoad = 10;
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
		bayX = 0;
		bayY = 8;
		airplaneX = bayX;
		airplaneY = bayY; 
		grid [airplaneX, airplaneY].GetComponent<Renderer> ().material.color = Color.red;
		activePlane = false;
		depotX = 15;
		depotY = 0;
		grid [depotX, depotY].GetComponent<Renderer> ().material.color = Color.black;
	}

	// Update is called once per frame
	void Update () {

		if (Time.time > takeATurn) {
			//Check if the plane is in bay to give cargo
			if (airplaneX == bayX && airplaneY == bayY && planeCargo < planeMaxHold) {
				//load cargo
				planeCargo += cargoLoad; 
				SetCargo ();
				//do not go over planeMaxHold
			}
			//Check if plane in depo to deliver cargo
			if (airplaneX == depotX && airplaneY == depotY) {
			//add cargo to score
				playerScore += planeCargo;
				SetPlayerScore ();
				planeCargo = 0;
				SetCargo ();
			}
			print ("Cargo:" + planeCargo + ". . . Score:" + playerScore); 
			takeATurn += turnTime;
		}
	}

	void SetPlayerScore () {
		playerScoreText.text = "Score: " + playerScore.ToString ();
	}
	void SetCargo () {
		cargoText.text = "Cargo: " + planeCargo.ToString ();
	}
}