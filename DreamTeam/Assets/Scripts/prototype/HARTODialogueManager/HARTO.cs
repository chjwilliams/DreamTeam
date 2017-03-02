using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  enum  Emotions
{
	NAN,
	Happy,
	Sad,
	Curious
}
public class HARTO : MonoBehaviour 
{

	private Emotions emotion;
	public Emotions CurrentEmotion
	{
		get
		{
			return emotion;
		}
	}


	// Use this for initialization
	void Start () 
	{
		emotion = Emotions.NAN;
	}
	
	private void SetEmotion(KeyCode key)
	{
		if (Input.GetKey(key))
		{
			emotion = Emotions.Happy;
		}
		else if (key == KeyCode.Alpha1)
		{
			emotion = Emotions.Sad;
		}
		else if (key == KeyCode.Alpha2)
		{
			emotion = Emotions.Curious;
		}

		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.Alpha1))
		{
			emotion = Emotions.Happy;
		}
		else if (Input.GetKey(KeyCode.Alpha2))
		{
			emotion = Emotions.Sad;
		}
		else if (Input.GetKey(KeyCode.Alpha3))
		{
			emotion = Emotions.Curious;
		}

		Debug.Log(emotion.ToString());
	}
}
