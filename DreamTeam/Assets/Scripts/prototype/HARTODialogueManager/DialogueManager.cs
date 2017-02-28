using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

	
	public HARTO astridHARTO;
	public EventScript[] Events;
	private int RelationLevel;

	public const string EVENT_MEETING_ASTRID_STARTS = "Event_Meeting@";
	public const string EVENT_UTAN_ASTRID_STARTS = "Event_Utan@";
	public const string PLAYER_ASTRID = "Player_Astrid";

	public const string NPC_MOM = "NPC_Mom";
	public const string NPC_MALI = "NPC_Mali";


	// Use this for initialization
	void Start () 
	{
		
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
		if (Input.GetKeyDown(KeyCode.M))
		{
			InitDialogueEvent(EVENT_MEETING_ASTRID_STARTS, NPC_MOM);
		}
		else if (Input.GetKeyDown(KeyCode.U))
		{
			InitDialogueEvent(EVENT_UTAN_ASTRID_STARTS, NPC_MALI);

		}
		
	}
}
