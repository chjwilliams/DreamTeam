using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HartoTuningController : MonoBehaviour {

	//	Public variables
	public float movespeed = 5.0f;
	public float currentfrequency;
	public float frequencyincrement = 5.0f;
	public KeyCode increasefrequency = KeyCode.Alpha1;
	public KeyCode decreasefrequency = KeyCode.Alpha2;

	//	Private variables
	private Transform _transform;

	// Use this for initialization
	void Start () {
		_transform = GameObject.FindGameObjectWithTag ("HartoKnob").transform;
		currentfrequency = _transform.localRotation.eulerAngles.y;
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
	}

	float getFrequency(){
		return currentfrequency;
	}

	void adjustFrequency(){
		
	}
}
