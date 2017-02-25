using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseScript : MonoBehaviour {

	[Range(0.0f, 1.0f)]
	public float volume = 1.0f;
	
	public VoiceOverLine myLine;

	public string characterName;

	// Use this for initialization
	void Start () 
	{
		characterName = transform.parent.name;
	}

	virtual public void PlayLine()
	{
		myLine.LoadAudioClip(characterName, "HARTO", transform.name);
	}

	virtual public void PlayLine(HARTO.Emotions myEmotion)
	{
	}
	
}
