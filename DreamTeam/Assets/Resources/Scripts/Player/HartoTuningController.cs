using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HartoTuningController : MonoBehaviour {

	//	Public variables
	public bool HARTOisActive;
	public float HARTOalpha;
	public float incrementAlpha = 2.0f;
	public float movespeed = 5.0f;
	public float currentfrequency;
	public float frequencyincrement = 5.0f;
	public KeyCode increasefrequency = KeyCode.Alpha1;
	public KeyCode decreasefrequency = KeyCode.Alpha2;
	public KeyCode activateHARTO = KeyCode.Tab;
	public GameObject HARTOKnotch;
	public GameObject HARTOKnob;

	//	Private variables
	private Transform _transform;
	private MeshRenderer _meshRenderer;
	private MeshRenderer _KnotchMeshRenderer;
	private MeshRenderer _KnobMeshRenderer;

	// Use this for initialization
	void Start () {
		_transform = GameObject.FindGameObjectWithTag ("HartoKnob").transform;
		currentfrequency = _transform.localRotation.eulerAngles.y;
		HARTOisActive = false;
		_meshRenderer = GetComponent<MeshRenderer> ();
		_KnotchMeshRenderer = HARTOKnotch.GetComponent<MeshRenderer> ();
		_KnobMeshRenderer = HARTOKnob.GetComponent<MeshRenderer> ();
		_meshRenderer.material.color = new Color(_meshRenderer.material.color.r, 
													_meshRenderer.material.color.g, 
													_meshRenderer.material.color.b, 
													0.0f);
		_KnotchMeshRenderer.material.color = new Color(_KnotchMeshRenderer.material.color.r, 
														_KnotchMeshRenderer.material.color.g, 
														_KnotchMeshRenderer.material.color.b, 
														_meshRenderer.material.color.a);

		_KnobMeshRenderer.material.color = new Color(_KnobMeshRenderer.material.color.r, 
														_KnobMeshRenderer.material.color.g, 
														_KnobMeshRenderer.material.color.b, 
														_meshRenderer.material.color.a);
	}
		
	// Update is called once per frame
	void Update () {
		if (!GameManager.gm.thirdPersonActive) {
			if (Input.GetKey (decreasefrequency)) {
				currentfrequency += frequencyincrement;
				if (currentfrequency > 135.0f) {
					currentfrequency = 135.0f;
				}
				_transform.localRotation = Quaternion.Euler (0, currentfrequency, 0);
			} else if (Input.GetKey (increasefrequency)) {
				currentfrequency -= frequencyincrement;
				if (currentfrequency < -135.0f) {
					currentfrequency = -135.0f;
				}
				_transform.localRotation = Quaternion.Euler (0, currentfrequency, 0);
			}

			if (Input.GetKeyDown (activateHARTO)) {
				HARTOisActive = !HARTOisActive;

			}

			if (HARTOisActive) {
				HARTOalpha += incrementAlpha * Time.deltaTime;
				if (HARTOalpha > 1.0f) {
					HARTOalpha = 1.0f;
				}
			} else {
				HARTOalpha -= incrementAlpha * Time.deltaTime;
				if (HARTOalpha < 0.0f) {
					HARTOalpha = 0.0f;
				}
			}
			
			toggleHARTO (HARTOisActive, HARTOalpha);
		}
		else {
			toggleHARTO (HARTOisActive, 0);

		}

	}

	void toggleHARTO (bool b, float alpha){
		
		_meshRenderer.material.color = new Color(_meshRenderer.material.color.r, 
													_meshRenderer.material.color.g, 
													_meshRenderer.material.color.b, 
													alpha);
		
		_KnotchMeshRenderer.material.color = new Color(_KnotchMeshRenderer.material.color.r, 
														_KnotchMeshRenderer.material.color.g, 
														_KnotchMeshRenderer.material.color.b, 
														_meshRenderer.material.color.a);

		_KnobMeshRenderer.material.color = new Color(_KnobMeshRenderer.material.color.r, 
														_KnobMeshRenderer.material.color.g, 
														_KnobMeshRenderer.material.color.b, 
														_meshRenderer.material.color.a);
		

	}

	float getFrequency(){
		return currentfrequency;
	}

	void adjustFrequency(){
		
	}
}
