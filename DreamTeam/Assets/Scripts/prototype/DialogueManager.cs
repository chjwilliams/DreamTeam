using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

	public GameObject Event1;
	public GameObject Event2;
	public GameObject Event3;
	public GameObject Event4;
	public GameObject Event5;
	private string NPCname;
	private int RelationLevel;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(NPCname == "MoM"){
			if (RelationLevel == 1) {
				Event1.SetActive (true);
			}
		}
		
	}
}
