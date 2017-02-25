using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionalResponseScript : ResponseScript {

	private const string HARTO_REF = "HARTO";
	private HARTO astridHARTO;

	public VoiceOverLine[] possibleLines;

	// Use this for initialization
	void Start () 
	{
		astridHARTO = GameObject.FindGameObjectWithTag(HARTO_REF).GetComponent<HARTO>();
	}

	override public void PlayLine(HARTO.Emotions myEmotion)
	{
		myLine.LoadAudioClip(characterName, "HARTO", transform.name, true);
	}
	
}
