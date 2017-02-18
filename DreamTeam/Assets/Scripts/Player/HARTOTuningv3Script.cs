using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HARTOTuningv3Script : MonoBehaviour {

	float rotateHARTO;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rotateHARTO = rotateHARTO +  Input.GetAxis ("Mouse ScrollWheel");
		transform.localRotation = Quaternion.EulerAngles(transform.rotation.x, rotateHARTO, transform.rotation.z);
	}
}
