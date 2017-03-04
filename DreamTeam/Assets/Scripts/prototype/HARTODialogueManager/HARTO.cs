﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEventManager;
using GameEvents;


public  enum  Emotions
{
	None,
	Happy,
	Sad,
	Curious
}
public class HARTO : MonoBehaviour 
{
	[SerializeField]
	private Emotions emotion;
	public Emotions CurrentEmotion
	{
		get
		{
			return emotion;
		}
	}

	private EmotionSelectedEvent.Handler onEmotionSelected;
	// Use this for initialization
	void Start () 
	{
		onEmotionSelected = new EmotionSelectedEvent.Handler(OnEmotionSelected);
		GameEventsManager.Instance.Register<EmotionSelectedEvent>(onEmotionSelected);
	}
	
	void OnEmotionSelected(GameEvent e)
	{
		 emotion = ((EmotionSelectedEvent)e).hartoEmotion.currentEmotion;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
