using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

	public int myX, myY;
	GameController myGameController;

	// Use this for initialization
	void Start () {
		myGameController = GameObject.Find("ControlObject").GetComponent<GameController>();
		//GameObject.FindObjectOfType<GameController> ();
	}

	// Update is called once per frame
	void Update () {

	}
	void OnMouseDown() {
		myGameController.ProcessClick (gameObject, myX, myY);

	}
}
