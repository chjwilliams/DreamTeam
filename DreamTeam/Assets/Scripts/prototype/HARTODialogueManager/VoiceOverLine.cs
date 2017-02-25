using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverLine : MonoBehaviour 
{
	public string voiceOverLine = "";
	public AudioClip voiceOver;

	// Use this for initialization
	void Start () 
	{
	
	}

	public AudioClip LoadAudioClip(string npcName, string dialogueType, string filename, bool emotionalResponse)
	{
		voiceOver = Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType);

		return voiceOver;
	}
	public AudioClip LoadAudioClip(string npcName, string dialogueType, string filename)
	{
		voiceOver = Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename);

		return voiceOver;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
