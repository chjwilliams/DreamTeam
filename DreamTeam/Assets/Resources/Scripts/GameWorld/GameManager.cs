using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public GameObject firstPersonCamera;
	public GameObject thirdPersonCamera;

	public bool thirdPersonActive;

	// Use this for initialization
	void Start () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		}

		thirdPersonActive = false;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.LeftShift)){
			thirdPersonActive = !thirdPersonActive;
			toggleCamera (thirdPersonActive);
		}
	}

	void toggleCamera(bool b){
		if (b){
			// make camera view thirdperson camera
			thirdPersonCamera.SetActive(true);
			firstPersonCamera.SetActive (false);
		} 
		else {
			//	make camera view first person camera
			thirdPersonCamera.SetActive(false);
			firstPersonCamera.SetActive(true);
		}
	}
}
