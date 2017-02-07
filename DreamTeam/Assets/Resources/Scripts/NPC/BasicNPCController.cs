using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNPCController : MonoBehaviour {

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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			if (!acknowledgePlayer) {
				transform.Translate (Vector3.up);
				acknowledgePlayer = true;
			}
			if (HARTO.currentfrequency > myFrequency - range && HARTO.currentfrequency < myFrequency + range) {
				GetComponent<MeshRenderer> ().material.color = new Color (0.0f, 0.0f, 1.0f);
			}
			else {
				GetComponent<MeshRenderer> ().material.color = new Color (1.0f, 0.0f, 0.0f);
			}
		}
	}

	void OnTriggerExit(Collider other) {
		acknowledgePlayer = false;
	}
}
