using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionalResponseScript : ResponseScript {

	private const string HARTO_REF = "HARTO";
	private const string DELIMITER = "Line_";
	private HARTO astridHARTO;

	public VoiceOverLine[] possibleLines;

	// Use this for initialization
	void Start () 
	{
		base.Start();	
		astridHARTO = GameObject.FindGameObjectWithTag(HARTO_REF).GetComponent<HARTO>();
		possibleLines = GetComponentsInChildren<VoiceOverLine>();
	}

	public Emotions GetEmotionalInput()
	{
		return astridHARTO.CurrentEmotion;
	}

	public void PlayEmotionLine(Emotions emotion)
	{		
		for (int i  = 0; i < possibleLines.Length; i++)
		{
			if (possibleLines[i].name.Contains(emotion.ToString()))
			{	
				characterAudioSource.PlayOneShot(possibleLines[i].LoadAudioClip(characterName, "HARTO", transform.name, emotion.ToString()), volume);
				elapsedSeconds = myLine.LoadAudioClip(characterName, "HARTO", transform.name, emotion.ToString()).length;
			}
		}	
	}
}
