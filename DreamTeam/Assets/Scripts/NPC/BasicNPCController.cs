using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	BasicNPCController: Handles baisc npc behavior								        */
/*			Functions:																	*/
/*					public:																*/
/*						startGestureAnimation()											*/
/*						OnTriggerEnter()											    */
/*						OnTriggerStay()      											*/
/*						OnTriggerStay()      											*/
/*						OnTriggerExit()													*/
/*--------------------------------------------------------------------------------------*/


public class BasicNPCController : MonoBehaviour {

	public bool gestureAnimationDone;        //whether we have done the gesture animation
	public HartoTuningController HARTO;      //Game object HARTO
	public Color myColor;
	public float myFrequency;					//
	public float range;
	public bool acknowledgePlayer;


	// Use this for initialization
	void Start () {
		HARTO = GameObject.FindGameObjectWithTag ("HARTO").GetComponent<HartoTuningController> ();    //get the HARTO thourgh find the tag of HARTO
		myFrequency = 60;							//the frequency you can talk to me
		range = 2.5f;								// the range of the frequency you can talk to me
		acknowledgePlayer = false;					//I am not talking with the player
		gestureAnimationDone = false;				//at the start, gesture animation has not shown yet
		myColor = GetComponent<MeshRenderer>().material.color;
		GetComponent<MeshRenderer>().material.color = new Color (1, 1, 0, 1);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//the function we start play the animation
	public void startGestureAnimation() {
		gestureAnimationDone = true;   //set this bool to true  after we played the animation
		//GetComponent<MeshRenderer> ().material.color = new Color (0.0f, 0.0f, 0.0f); // change the color of the object after we played the animation

	}

	//the function we call when the other collider enter this collision
	void OnTriggerEnter(Collider other) {   
		if (other.gameObject.CompareTag ("Astrid")) {     //if the other collider has the tag of "Astrid" , do the stuff
			if (!acknowledgePlayer) {					//whether the player is talking with other game object? if not, do the stuff
				//transform.Translate (Vector3.up);		//lift me up if I am talking to player. That's why I am shaking.
				acknowledgePlayer = true;				//Other script will know I am talking to player
				GetComponent<MeshRenderer>().material.color  = new Color (0, 1, 0, 1);
			}
		}
	}

	//the function  we call when the trigger hang there
	void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag ("Astrid")) {			//if the other collider has the tag of "Astrid" , do the stuff
			Vector3 delta = new Vector3(other.gameObject.transform.position.x - transform.position.x, 0.0f, other.gameObject.transform.position.z - transform.position.z);  //get the position of the collider who enter
			Quaternion rotation = Quaternion.LookRotation(delta); //rotation in that direction?

			transform.rotation = rotation;  //make the rotation to the enter's rotation?

		// 	//if you are in the right frequency, change the color, to let player know they got it right.
		// 	if (HARTO.currentfrequency > myFrequency - range && HARTO.currentfrequency < myFrequency + range) {
		// 		GetComponent<MeshRenderer> ().material.color = new Color (0.0f, 0.0f, 1.0f);   
		// 	}

		// 	//else if the animation has not shown yet, change the color
		// 	else  if (!gestureAnimationDone){
		// 		GetComponent<MeshRenderer> ().material.color = new Color (1.0f, 0.0f, 0.0f);
		// 	}
		}
	}

	//if the collider leave me, stop talking to the player, and also let other game object know that.
	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Astrid"))
		{
			GetComponent<MeshRenderer>().material.color  = new Color (1, 1, 0, 1);
			acknowledgePlayer = false;
		}
	}
}
