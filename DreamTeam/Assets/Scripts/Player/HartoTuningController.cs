using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------------------------------------------------------------*/
/*                                                                                                 	*/
/*       HartoTuningController: controls functionality of HARTO                                    	*/
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
public class HartoTuningController : MonoBehaviour {


	//	Public variables
	public bool HARTOisActive;									//	Communicates to other GameObjects that HARTO is active
	public bool isHappy;										//	Is HARTO set to happy
	public bool isCurious;										//	Is HARTO set to Curious
	public bool isSad;											//	Is HARTO set to Sad
	public float HARTOalpha;									//	The Alpha of Harto GameObject
	public float incrementAlpha = 2.0f;							//	How much we increment/decrement the alpha channel of HARTO GameObject
	public float movespeed = 5.0f;								//	TBD
	public float currentfrequency;								//	Stores current frequency of HARTO
	public float frequencyincrement = 5.0f;						//	How much you increment the frequncy of HARTO with each press/hold
	public KeyCode increasefrequency = KeyCode.Alpha1;			//	Button to increase frequency
	public KeyCode decreasefrequency = KeyCode.Alpha2;			//	Button to decrease frequency
	public KeyCode activateHARTO = KeyCode.Tab;					//	Button to active HARTO
	public KeyCode setHappy = KeyCode.Z;						//	Button to Set HARTO to Happy
	public KeyCode setCurious = KeyCode.X;						//	Button to set HARTO to Curious
	public KeyCode setSad = KeyCode.C;							//	Button to set HARTO to Sad
	public GameObject HARTOKnotch;								//	Reference to the knoch on the knob
	public GameObject HARTOKnob;								//	Reference to the Knob on HARTO
	public MeshRenderer happyButton;							//	Reference to the Happy Button GameObject
	public MeshRenderer curiousButton;							//	Referecne to the Curious Button GameObject
	public MeshRenderer sadButton;								//	Referecne to the Sad Button GameObject

	//	Private variables
	[SerializeField] private bool m_CanUseHARTO;				//	Tells us if we can or cannot use HARTO
	[SerializeField] private bool m_CanEmoteHARTO;				//	Tells us if we can select an emotion with HARTO
	[SerializeField] private bool m_CanTuneHARTO;				//	Tells us if we can tune HARTO
	private Transform _transform;								//	Reference to GameObject's Transform
	private MeshRenderer _meshRenderer;							//	Reference to GameObject's MeshRenderer
	private MeshRenderer _KnotchMeshRenderer;					//	Reference to the Knotch's MeshRenderer
	private MeshRenderer _KnobMeshRenderer;						//	Reference to Knob's MeshRenderer

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Start: Runs once at the begining of the game. Initalizes variables.					*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Start () {
		//	Sets up transform's reference
		_transform = GameObject.FindGameObjectWithTag ("HartoKnob").transform;

		//	Sets the current frequency
		currentfrequency = _transform.localRotation.eulerAngles.y;

		//	HARTO starts game not active
		HARTOisActive = false;

		//	Sets up the references to the MeshRenderers
		_meshRenderer = GetComponent<MeshRenderer> ();
		_KnotchMeshRenderer = HARTOKnotch.GetComponent<MeshRenderer> ();
		_KnobMeshRenderer = HARTOKnob.GetComponent<MeshRenderer> ();
		happyButton = GameObject.Find("Happy").GetComponent<MeshRenderer>();
		curiousButton = GameObject.Find("Curious").GetComponent<MeshRenderer>();
		sadButton = GameObject.Find("Sad").GetComponent<MeshRenderer>();

		//	Makes HARTO GameObject conglomerate transparent
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
		
		//	HARTO functionality turned off until activated in Prototype
		m_CanUseHARTO = false;
     	m_CanEmoteHARTO = false;
     	m_CanTuneHARTO =  false;

		//	Defeaults all emotions to off
		defaultHARTOEmotions();
	}

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	UpdateHARTOButtons: Changes and displays the active emotion 						*/
    /*		param: KeyCode key - the Key associated with each emotion						*/
	/*																						*/
    /*--------------------------------------------------------------------------------------*/
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

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Update: Called once per frame														*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
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

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	toggleHARTO: Hides and displays HARTO												*/
    /*		param: float alpha - the alpha channel value for the MeshRenderer				*/
	/*																						*/
    /*--------------------------------------------------------------------------------------*/
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

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	setHARTOActiveTo: Toggles functionality of HARTO									*/
    /*		param: bool canUse - can HARTO be activated										*/
	/*		param: bool canTune - can HARTO be tuned										*/
	/*		param: bool canEmote - can HARTO emotions buttons be used						*/
	/*																						*/
    /*--------------------------------------------------------------------------------------*/
	public void setHARTOActiveTo(bool canUse, bool canTune, bool canEmote){
		m_CanUseHARTO = canUse;
		m_CanTuneHARTO = canTune;
		m_CanEmoteHARTO = canEmote;
	}

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	defaultHARTOEmotions: turns all HARTO Emotions off									*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	public void defaultHARTOEmotions() {
		isHappy = false;
		isCurious = false;
		isSad = false;
	}

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	canUseHARTO: returns true if we can use HARTO										*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	public bool canUseHARTO(){
		return m_CanUseHARTO;
	}

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	canTuneHARTO: returns true if we can tune HARTO										*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	public bool canTuneHARTO(){
		return m_CanTuneHARTO;
	}

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	canEmoteHARTO: returns true if we can use HARTO	 emotion buttons					*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	public bool canEmoteHARTO(){
		return m_CanEmoteHARTO;
	}
}
