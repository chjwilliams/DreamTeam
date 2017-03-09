using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseScript : MonoBehaviour {

	[Range(0.0f, 1.0f)]
	public float volume = 1.0f;
	
	public float elapsedSeconds;
	public VoiceOverLine myLine;

	public AudioSource characterAudioSource;
	//	public AudioSource gibberishAudioSource;
	public string characterName;

	// Use this for initialization
	protected void Start () 
	{
		characterAudioSource = GetComponentInParent<AudioSource>();
		//	gibberishAudioSource = 
		characterName = transform.parent.name;
		myLine = GetComponentInChildren<VoiceOverLine>();
	}

	virtual public void PlayLine()
	{
		characterAudioSource.PlayOneShot(myLine.LoadAudioClip(characterName, "HARTO", transform.name), volume);
		elapsedSeconds = myLine.LoadAudioClip(characterName, "HARTO", transform.name).length;
	}

	virtual public void PlayLine(Emotions myEmotion)
	{
	}
	
}
