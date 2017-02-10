using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNPCController : MonoBehaviour {

	public bool gestureAnimationDone;
	public HartoTuningController HARTO;
	public float myFrequency;
	public float range;
	public bool acknowledgePlayer;


	// Use this for initialization
	void Start () {
		HARTO = GameObject.FindGameObjectWithTag ("HARTO").GetComponent<HartoTuningController> ();
		myFrequency = 60;
		range = 2.5f;
		acknowledgePlayer = false;
		gestureAnimationDone = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startGestureAnimation() {
		gestureAnimationDone = true;
		GetComponent<MeshRenderer> ().material.color = new Color (0.0f, 0.0f, 0.0f);

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Astrid")) {
			if (!acknowledgePlayer) {
				transform.Translate (Vector3.up);
				acknowledgePlayer = true;
			}
		}
	}


	void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag ("Astrid")) {
			Vector3 delta = new Vector3(other.gameObject.transform.position.x - transform.position.x, 0.0f, other.gameObject.transform.position.z - transform.position.z);

			Quaternion rotation = Quaternion.LookRotation(delta);

			transform.rotation = rotation;

			if (HARTO.currentfrequency > myFrequency - range && HARTO.currentfrequency < myFrequency + range) {
				GetComponent<MeshRenderer> ().material.color = new Color (0.0f, 0.0f, 1.0f);
			}
			else  if (!gestureAnimationDone){
				GetComponent<MeshRenderer> ().material.color = new Color (1.0f, 0.0f, 0.0f);
			}
		}
	}

	void OnTriggerExit(Collider other) {
		acknowledgePlayer = false;
	}
}
