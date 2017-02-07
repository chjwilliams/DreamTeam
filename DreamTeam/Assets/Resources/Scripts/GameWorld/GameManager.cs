using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************************/
/*       Gamemanager: control camera switch                                                        */
/*                  start();                                                                       */
/*                  update();                                                                      */
/*                  toggleCamera();                                                                */
/*                                                                                                 */
/***************************************************************************************************/
public class GameManager : MonoBehaviour {

	//    Public Variables
	public static GameManager gm;					//Refernece to  the gamemanager

	public GameObject firstPersonCamera;			//Refernece to  the firstpersoncamera
	public GameObject thirdPersonCamera;			//Refernece to  the thirdpersoncamera

	public bool thirdPersonActive;					////Refernece to whether the thirdpersoncamera is active

	// Use this for initialization
	void Start () {

		//if gm is undifined, get the refernce to GameManger
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();  
		}

		//initial thirdpersoncamera inactive
		thirdPersonActive = false;
		
	}
	
	// Update is called once per frame
	void Update () {

		//if left shift key is down, set the active of thirdpersoncamera the opposite 
		if (Input.GetKeyDown(KeyCode.LeftShift)){
			thirdPersonActive = !thirdPersonActive;

			//call toggleCamera function
			toggleCamera (thirdPersonActive);
		}
	}

	//the function we set the active/inactive of firstperson and thirdperson camera
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
