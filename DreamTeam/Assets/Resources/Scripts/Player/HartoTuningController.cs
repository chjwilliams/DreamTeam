using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HartoTuningController : MonoBehaviour {


	//	Public variables
	public bool HARTOisActive;
	public bool isHappy;
	public bool isCurious;
	public bool isSad;
	public float HARTOalpha;
	public float incrementAlpha = 2.0f;
	public float movespeed = 5.0f;
	public float currentfrequency;
	public float frequencyincrement = 5.0f;
	public KeyCode increasefrequency = KeyCode.Alpha1;
	public KeyCode decreasefrequency = KeyCode.Alpha2;
	public KeyCode activateHARTO = KeyCode.Tab;
	public KeyCode setHappy = KeyCode.Z;
	public KeyCode setCurious = KeyCode.X;
	public KeyCode setSad = KeyCode.C;
	public GameObject HARTOKnotch;
	public GameObject HARTOKnob;
	public MeshRenderer happyButton;
	public MeshRenderer curiousButton;
	public MeshRenderer sadButton;

	//	Private variables
	[SerializeField] private bool m_CanUseHARTO;
	[SerializeField] private bool m_CanEmoteHARTO;
	[SerializeField] private bool m_CanTuneHARTO;
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
		happyButton = GameObject.Find("Happy").GetComponent<MeshRenderer>();
		curiousButton = GameObject.Find("Curious").GetComponent<MeshRenderer>();
		sadButton = GameObject.Find("Sad").GetComponent<MeshRenderer>();
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
		m_CanUseHARTO = false;
     	m_CanEmoteHARTO = false;
     	m_CanTuneHARTO =  false;

		 defaultHARTOEmotions();
	}

	public void UpdateHARTOButtons(KeyCode key) {
			if (key == setHappy) {
				isHappy = true;
				isCurious = false;
				isSad = false;
				happyButton.material.color = new Color(1.0f, 1.0f, 1.0f);
				curiousButton.material.color = new Color(0.2f, 0.2f, 0.2f);
				sadButton.material.color = new Color(0.2f, 0.2f, 0.2f);
			}
			else if (key == setCurious) {
				isHappy = false;
				isCurious = true;
				isSad = false;
				curiousButton.material.color = new Color(1.0f, 1.0f, 1.0f);
				happyButton.material.color = new Color(0.2f, 0.2f, 0.2f);
				sadButton.material.color = new Color(0.2f, 0.2f, 0.2f);
			} 
			else if (key == setSad) {
				isHappy = false;
				isCurious = false;
				isSad = true;
				sadButton.material.color = new Color(1.0f, 1.0f, 1.0f);
				curiousButton.material.color = new Color(0.2f, 0.2f, 0.2f);
				happyButton.material.color = new Color(0.2f, 0.2f, 0.2f);
			}
			else{
				isHappy = false;
				isCurious = false;
				isSad = false;
				sadButton.material.color = new Color(0.2f, 0.2f, 0.2f);
				curiousButton.material.color = new Color(0.2f, 0.2f, 0.2f);
				happyButton.material.color = new Color(0.2f, 0.2f, 0.2f);
			}
	}

	// Update is called once per frame
	void Update () {
		if (!GameManager.gm.thirdPersonActive && m_CanUseHARTO) {
			
			if(Input.GetKeyDown(setHappy))
				UpdateHARTOButtons(setHappy);
			if(Input.GetKeyDown(setCurious))
				UpdateHARTOButtons(setCurious);
			if(Input.GetKeyDown(setSad))
				UpdateHARTOButtons(setSad);

			if (m_CanTuneHARTO) {
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
			
			toggleHARTO (HARTOalpha);
		}
		else {
			toggleHARTO (0);

		}

	}

	void toggleHARTO (float alpha){
		
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
		sadButton.material.color = new Color(sadButton.material.color.r, 
												sadButton.material.color.g, 
												sadButton.material.color.b, 
												_meshRenderer.material.color.a);
		curiousButton.material.color = new Color(curiousButton.material.color.r, 
													curiousButton.material.color.g, 
													curiousButton.material.color.b, 
													_meshRenderer.material.color.a);
		happyButton.material.color = new Color(happyButton.material.color.r, 
												happyButton.material.color.g, 
												happyButton.material.color.b, 
												_meshRenderer.material.color.a);

	}


	public void setHARTOActiveTo(bool canUse, bool canTune, bool canEmote){
		m_CanUseHARTO = canUse;
		m_CanTuneHARTO = canTune;
		m_CanEmoteHARTO = canEmote;
	}

	public void defaultHARTOEmotions()
	{
		isHappy = false;
		isCurious = false;
		isSad = false;
	}

	public bool canUseHARTO(){
		return m_CanUseHARTO;
	}

	public bool canTuneHARTO(){
		return m_CanTuneHARTO;
	}

	public bool canEmoteHARTO(){
		return m_CanEmoteHARTO;
	}
}
