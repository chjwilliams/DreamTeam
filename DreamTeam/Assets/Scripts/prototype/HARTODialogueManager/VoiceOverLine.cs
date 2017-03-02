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

	public AudioClip LoadAudioClip(string npcName, string dialogueType, string filename, string emotionalResponse)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType + "_" + emotionalResponse) == null)
		{
			Debug.Log("Resource Not Found Error: " + "Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType + "_" + emotionalResponse + " not found!");
		}
		
		voiceOver = Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType + "_" + emotionalResponse);

		return voiceOver;
	}
	public AudioClip LoadAudioClip(string npcName, string dialogueType, string filename)
	{
		if (Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType) == null)
		{
			Debug.Log("Resource Not Found Error: " + "Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + " not found!");
		}

		voiceOver = Resources.Load<AudioClip>("Audio/VO/" + npcName + "/" + dialogueType + "/" + filename + "_" + dialogueType);

		return voiceOver;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
