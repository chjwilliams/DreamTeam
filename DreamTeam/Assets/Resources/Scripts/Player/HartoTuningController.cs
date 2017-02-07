using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HartoTuningController : MonoBehaviour {

	//	Public variables
	public bool HARTOisActive;
	public float movespeed = 5.0f;
	public float currentfrequency;
	public float frequencyincrement = 5.0f;
	public KeyCode increasefrequency = KeyCode.Alpha1;
	public KeyCode decreasefrequency = KeyCode.Alpha2;
	public KeyCode activateHARTO = KeyCode.Tab;


	//	Private variables
	private Transform _transform;

	// Use this for initialization
	void Start () {
		_transform = GameObject.FindGameObjectWithTag ("HartoKnob").transform;
		currentfrequency = _transform.localRotation.eulerAngles.y;
		HARTOisActive = false;
	}
		
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(decreasefrequency)) {
			currentfrequency += frequencyincrement;
			if (currentfrequency > 135.0f) {
				currentfrequency = 135.0f;
			}
			_transform.localRotation = Quaternion.Euler (0, currentfrequency, 0);
		}
		else if (Input.GetKey(increasefrequency)) {
			currentfrequency -= frequencyincrement;
			if (currentfrequency < -135.0f) {
				currentfrequency = -135.0f;
			}
			_transform.localRotation = Quaternion.Euler (0, currentfrequency, 0);
		}

		if (Input.GetKey(activateHARTO)) {
			HARTOisActive = !HARTOisActive;
			toggleHARTO (HARTOisActive);
		}
			
	}

	void toggleHARTO (bool b){
		if (b) {
			
		}
		else {
			
		}
	}

	float getFrequency(){
		return currentfrequency;
	}

	void adjustFrequency(){
		
	}
}
