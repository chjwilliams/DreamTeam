using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------------------------------------------------------------*/
/*                                                                                                 	*/
/*       HARTOTuningv2Script: land in good zone to tune HARTO	                                	*/
/*             	void Start()                                                                        */
/*				void UpdateHARTOButtons(KeyCode key 												*/
/*             	IEnumerator FadeIn ()                                                              	*/
/*             	void Update()                                                                     	*/
/*             	void toggleHARTO (float alpha)                                                     	*/
/*              void setHARTOActiveTo(bool canUse, bool canTune, bool canEmote)                   	*/
/*              void defaultHARTOEmotions()                                                       	*/
/*              bool canUseHARTO()                                                                 	*/
/*              bool canTuneHARTO()                                                               	*/
/*              bool canEmoteHARTO()                                                              	*/
/*                                                                                                 	*/
/*--------------------------------------------------------------------------------------------------*/
public class HARTOTuningv2Script : MonoBehaviour 
{
	//	Public variables
	public bool inGoodZone;						//	Checks if we are in good zone
	public float dialMoveSpeed = 1.0f;			//	How fast the dial moves
	public KeyCode stopDail = KeyCode.R;	//	Stops dial
	public GameObject[] bar;						//	Reference to HARTO tuning bar
	public GameObject[] dial;						//	Reference to HARTO tuning dial
	public GameObject[] goodZone;					//	Reference to HARTO good zone


	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Start: Runs once at the begining of the game. Initalizes variables.					*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Start ()
	{
		// bar = GameObject.Find("HARTOBar");
		// dial = GameObject.Find("HARTODial");
		// goodZone = GameObject.Find("GoodZone");
	}

	void MoveDial()
	{
		for (int i = 0; i < 3; i++)
		{
		Vector3 newPosition = dial[i].transform.localPosition;
		if (Input.GetKeyDown(KeyCode.Z))
		{
			dialMoveSpeed *= -1;
		}
		newPosition.x -= dialMoveSpeed * Time.deltaTime;

		dial[i].transform.localPosition = newPosition;
		}

	}
	
	void OnCollisionEnter(Collision other)
	{

	
	}

	IEnumerator CheckTuning ()
	{
		float temp = dialMoveSpeed;
		dialMoveSpeed = 0.0f;
		yield return new WaitForSeconds(1.0f);
		dialMoveSpeed = temp;
	}
	
	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Update: Called once per frame														*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Update () 
	{
		MoveDial();	

		if (Input.GetKeyDown(stopDail))
		{
			StartCoroutine(CheckTuning());
		}
	}
}
