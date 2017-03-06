using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEvents;
using GameEventManager;

public class EventScript : MonoBehaviour 
{
	public bool canAcess;
	public bool waitingForEmotionalInput;

	public const string PLAYER_ASTRID = "Player_Astrid";
	public const string VO = " VO";
	public const string ASTRID_TALKS_FIRST = "@";
	public const string NO_EMOTION_SELECTED = "None";
	public int totalResponses;
	public int astridLines;
	public int npcLines;
	public int totalLines;
	public ResponseScript response;
	public GameObject thisResponse;
	public List<AudioSource> myCharacters;
	

	private HARTO astridHARTO;
	// Use this for initialization
	void Start () 
	{
		AudioSource[] thisEventsCharacters = GetComponentsInChildren<AudioSource>();
		for(int i = 0; i < transform.childCount; i++)
		{
			myCharacters.Add(thisEventsCharacters[i]);
		}

		astridHARTO = GameObject.FindGameObjectWithTag("HARTO").GetComponent<HARTO>();

		
	
	}

	public void InitResponseScriptWith(string characterName)
	{
		GameEventsManager.Instance.Fire(new BeginDialogueEvent());

		totalLines = 0;
		astridLines = 0;
		npcLines = 0;

		if (GameObject.Find(characterName))
		{
			for (int i = 0; i < myCharacters.Count; i++)
			{
				if (myCharacters[i].name == characterName || myCharacters[i].name == PLAYER_ASTRID)
				{
					totalResponses += myCharacters[i].transform.childCount;
				}
			}


			if (transform.name.Contains(ASTRID_TALKS_FIRST))
			{
				GameObject firstResponse = transform.FindChild(PLAYER_ASTRID).gameObject.transform.FindChild("Astrid VO" + astridLines).gameObject;
				if (firstResponse.transform.childCount > 1)
				{
					response = firstResponse.GetComponent<EmotionalResponseScript>();
					waitingForEmotionalInput = true;
				}
				else
				{
					response = firstResponse.GetComponent<ResponseScript>();
				}
				astridLines++;
			}
			else
			{
				GameObject firstResponse = transform.FindChild(characterName).gameObject.transform.FindChild(characterName + VO + npcLines).gameObject;
				if (firstResponse.transform.childCount > 1)
				{
					response = firstResponse.GetComponent<EmotionalResponseScript>();
					waitingForEmotionalInput = true;
				}
				else
				{
					response = firstResponse.GetComponent<ResponseScript>();
				}
				
				npcLines++;
			}

			StartCoroutine(PlayEventDialogue(characterName));
		}
	}

	public IEnumerator PlayEventDialogue(string characterName)
	{
		while(totalLines < totalResponses)
		{
			totalLines++;

			//	Redundant check (the first time)
			if (response.transform.childCount > 1)
			{
				//	If I am waiting for emotional input, I keep waiting unitl i get it.
				waitingForEmotionalInput = true;
			}

			//	Checks if response needs to wiat for emotional input
			while(astridHARTO.CurrentEmotion.ToString() == NO_EMOTION_SELECTED && response.transform.childCount > 1)
			{
				yield return new WaitForFixedUpdate();
			}

			//	Redundant check
			if (response.transform.childCount > 1)
			{
				((EmotionalResponseScript)response).PlayEmotionLine(astridHARTO.CurrentEmotion);
				yield return new WaitForSeconds(response.elapsedSeconds * 2.0f);
			}
			else
			{
				response.PlayLine();
				yield return new WaitForSeconds(response.elapsedSeconds * 2.0f);
			}

			//	Another way to wait until the line is done.
			// float t = 0;
			// while (t < response.elapsedSeconds * 40.0f)
			// {
			// 		t += Time.deltaTime;
			// }

			//	Breaks out of while loop when we finished all the reponses.			
			if (totalLines == totalResponses)
			{
				break;
			}

			//	Checks who spoke last. If it was Astrid, play NPC dialouge.
			if (response.characterName == PLAYER_ASTRID)
			{
				thisResponse = transform.FindChild(characterName).gameObject.transform.FindChild(characterName + VO + npcLines).gameObject;
				if (thisResponse.transform.childCount > 1)
				{
					response = thisResponse.GetComponent<EmotionalResponseScript>();
				}
				else
				{
					response = thisResponse.GetComponent<ResponseScript>();
				}
				npcLines++;
			}
			else
			{
				thisResponse = transform.FindChild(PLAYER_ASTRID).gameObject.transform.FindChild("Astrid VO" + astridLines).gameObject;

				if (thisResponse.transform.childCount > 1)
				{
					response = thisResponse.GetComponent<EmotionalResponseScript>();
					waitingForEmotionalInput = true;
				}
				else
				{
					response = thisResponse.GetComponent<ResponseScript>();
				}
				astridLines++;
			}
			
		}
		GameEventManager.GameEventsManager.Instance.Fire(new EndDialogueEvent());
		yield return null;
	}
}
