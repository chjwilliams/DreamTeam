using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*

		//	Gibberish first then HARTO dialogue
		//	Store dialouge on HARTO
		//	One big Dial two options: When in conversation rotary wheel for emotions. Not in conversation: Scroll through recordings
		//	Make player look up
		//	Emotion options 

		//	Look at ways to tune things
			Lock picking
			power measure
			Hit button in the window
		
		//	HARTO v1
			Emotion scroll wheel.
		
		//	HARTO v2
			Tune to atmosphere

		//	HARTO v3
			combine 1 and 2

		//	HARTO v4
			Master tune then fine tune throughout the day


		//	Navigating dialouge like a node tree

 */

/*-------------------------------------------------------------------------------------------------*/
/*                                                                                                 */
/*       Gamemanager: Manages state of the game                                                    */
/*             void Start();                                                                       */
/*             IEnumerator FadeIn ()                                                               */
/*             void Update();                                                                      */
/*             void toggleCamera(bool b);                                                          */
/*                                                                                                 */
/*-------------------------------------------------------------------------------------------------*/
public class GameManager : MonoBehaviour {

	//    Public Variables
	public static GameManager gm;						//	Refernece to  the gamemanager

	public bool disableInput;							//	Disables Input from player
	public bool thirdPersonActive;						//	Refernece to whether the thirdpersoncamera is active
	public KeyCode restartScene = KeyCode.Alpha0;		//	Restarts the scene
	public KeyCode switchCameras = KeyCode.LeftShift;	//	Switches between cameras
	public KeyCode hartoV1 = KeyCode.Alpha7;
	public KeyCode hartoV2 = KeyCode.Alpha9;
	public KeyCode hartoV3 = KeyCode.Alpha8;
	public FirstPersonController astrid;				//	Reference to player character
	public BasicNPCController mali;						//	Reference to Mali
	public GameObject firstPersonCamera;				//	Refernece to  the firstpersoncamera
	public GameObject thirdPersonCamera;				//	Refernece to  the thirdpersoncamera
	public GameObject hartoV1GO;
	public GameObject hartoV2GO;
	public GameObject hartoV3GO;


	//	Private Variables
	

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Start: Runs once at the begining of the game. Initalizes variables.					*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Start () {

		//if gm is undifined, get the refernce to GameManger
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();  
		}

		mali = GameObject.Find ("Mali").GetComponent<BasicNPCController>();
		astrid = GameObject.Find ("Astrid").GetComponent<FirstPersonController> ();

		//initial thirdpersoncamera inactive
		thirdPersonActive = false;
		
		disableInput = false;

		hartoV1GO = GameObject.Find("TemporaryHARTO");
		hartoV2GO = GameObject.Find("HARTOv2");
		hartoV3GO = GameObject.Find("HARTOv3");

		hartoV1GO.SetActive(true);
		hartoV2GO.SetActive(false);
		hartoV3GO.SetActive(false);
	}

	
	
	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Update: Called once per frame														*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Update () {

		//if left shift key is down, set the active of thirdpersoncamera the opposite 
		if (Input.GetKeyDown(switchCameras)){
			thirdPersonActive = !thirdPersonActive;

			//call toggleCamera function
			toggleCamera (thirdPersonActive);
		}

		if (Input.GetKeyDown(restartScene))
		{
			Scene scene = SceneManager.GetActiveScene();
        	SceneManager.LoadScene(scene.name);
		}

		if (Input.GetKeyDown(hartoV1))
		{
			hartoV1GO.SetActive(true);
			hartoV2GO.SetActive(false);
			hartoV3GO.SetActive(false);
		}
		else if (Input.GetKeyDown(hartoV2))
		{
			hartoV1GO.SetActive(false);
			hartoV2GO.SetActive(true);
			hartoV3GO.SetActive(false);
		}
		else if (Input.GetKeyDown(hartoV3))
		{
			hartoV1GO.SetActive(false);
			hartoV2GO.SetActive(false);
			hartoV3GO.SetActive(true);
		}
	}



	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	toggleCamera: switches between first person and third person camera					*/
    /*		param: bool b - switches the camera												*/
	/*																						*/
    /*--------------------------------------------------------------------------------------*/
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
