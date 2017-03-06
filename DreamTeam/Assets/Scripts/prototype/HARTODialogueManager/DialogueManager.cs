﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEventManager;
using GameEvents;

public class DialogueManager : MonoBehaviour {

	
	public HARTO astridHARTO;
	public EventScript[] Events;
	private int RelationLevel;

	public const string EVENT_PREFIX = "Event_";
	public const string EVENT_ASTRID_TALKS_FIRST = "@";
	public const string EVENT_MEETING_ASTRID_STARTS = "Event_Meeting@";
	public const string EVENT_UTAN_ASTRID_STARTS = "Event_Utan@";
	public const string PLAYER_ASTRID = "Player_Astrid";

	public const string NPC_TAG = "NPC_";
	public const string NPC_MOM = "NPC_Mom";
	public const string NPC_MALI = "NPC_Mali";

	private EmotionSelectedEvent.Handler onTopicSelected;

	// Use this for initialization
	void Start () 
	{
		onTopicSelected = new TopicSelectedEvent.Handler(OnTopicSelected);
		GameEventsManager.Instance.Register<TopicSelectedEvent>(onTopicSelected);
	}

	void OnTopicSelected(GameEvent e)
	{
		string selectedEvent = EVENT_UTAN_ASTRID_STARTS; //EVENT_PREFIX + ((TopicSelectedEvent)e).hartoTopic.currentTopic.ToString() + EVENT_ASTRID_TALKS_FIRST;
		Debug.Log(EVENT_PREFIX + ((TopicSelectedEvent)e).hartoTopic.currentTopic.name + EVENT_ASTRID_TALKS_FIRST);
		try
		{
			InitDialogueEvent(selectedEvent, NPC_TAG + ((TopicSelectedEvent)e).player.npcAstridIsTalkingTo.name);
		}
		catch (Exception ex)
		{
			Debug.Log("You are not talking to an NPC or the current NPC is not attached to this event. Stack Trace: " + ex.StackTrace);
		}
		
	}

	void InitDialogueEvent(string topic, string npcName)
	{
		if (GameObject.Find(topic))
		{
			EventScript thisEvent = GameObject.Find(topic).GetComponent<EventScript>();
			thisEvent.InitResponseScriptWith(npcName);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
