using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour 
{
	public bool canAcess;

	public const string PLAYER_ASTRID = "Player_Astrid";
	public const string VO = " VO";
	public const string ASTRID_TALKS_FIRST = "@";
	public int totalResponses;
	public int astridLines;
	public int npcLines;
	public int totalLines;
	public ResponseScript response;
	public List<AudioSource> myCharacters;
	// Use this for initialization
	void Start () 
	{
		AudioSource[] thisEventsCharacters = GetComponentsInChildren<AudioSource>();
		for(int i = 0; i < transform.childCount; i++)
		{
			myCharacters.Add(thisEventsCharacters[i]);
		}	
	}

	public void InitResponseScriptWith(string characterName)
	{
		totalLines = 0;
		astridLines = 1;
		npcLines = 1;
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
				response = GameObject.Find("Astrid VO" + astridLines).GetComponent<ResponseScript>();
				astridLines++;
			}
			else
			{
				response = GameObject.Find(characterName + VO + npcLines).GetComponent<ResponseScript>();
				npcLines++;
			}
			Debug.Log(characterName);
			response.PlayLine();
			StartCoroutine(PlayEventDialogue(characterName));
		}
	}

	public IEnumerator PlayEventDialogue(string characterName)
	{
		float seconds = response.elapsedSeconds;
		while(totalLines < totalResponses)
		{
			yield return new WaitForSeconds(seconds * 2);
			if (response.characterName == PLAYER_ASTRID)
			{
				response = GameObject.Find(characterName + VO + npcLines).GetComponent<ResponseScript>();
				
				response.PlayLine();
				npcLines++;
			}
			else
			{
				response = GameObject.Find("Astrid VO" + astridLines).GetComponent<ResponseScript>();
				response.PlayLine();
				astridLines++;
			}
			

			totalLines++;
		}

		yield return null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
