using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameEventManager;
using GameEvents;
using UnityStandardAssets.Characters.FirstPerson;

public class HARTOTuningv3Script : MonoBehaviour {

	public const string SCROLLWHEEL = "Mouse ScrollWheel";		//	Name reference to the scroll wheel axis
	public const string ASTRID = "Astrid";

	public KeyCode toggleHARTO = KeyCode.Tab;
	public bool isHARTOActive;
	public bool topicSelected;
	public float rotationSpeed = 20.0f;
	public float selectionAreaWidth;
	public GameObject topicWheel;
	public GameObject emotionWheel;
	
	
	public Canvas canvas;
	public Emotions currentEmotion;
	public Icon currentTopic;
	public Icon[] emotionWheelIcons;
	public Icon[] topicWheelIcons;
	public EmotionIcon[] emotionIcons;
	public Image selectionArea;
	float rotateHARTO;

	[SerializeField]
	private FirstPersonController player;
	private ToggleHARTOEvent.Handler onToggleHARTO;
	public Color transparent = new Color(1.0f, 1.0f, 1.0f);
	private Color opaqueWheel = new Color (0.31f, 0.86f, 0.83f, 1.0f);
	public Color selectionAreaColor;
	private Color opaqueTopic = Icon.inactiveColor;
	// Use this for initialization
	void Start () 
	{
		isHARTOActive = true;

		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		topicSelected = false;
		selectionArea = GameObject.Find("SelectionArea").GetComponent<Image>();
		selectionAreaWidth = selectionArea.sprite.bounds.extents.x;
		selectionAreaColor = selectionArea.color;

		topicWheel = GameObject.Find("TopicWheelUI");
		topicWheelIcons = topicWheel.GetComponentsInChildren<Icon>();

		emotionWheel = GameObject.Find("EmotionWheelUI");
		emotionWheelIcons = emotionWheel.GetComponentsInChildren<Icon>();
		emotionIcons = emotionWheel.GetComponentsInChildren<EmotionIcon>();

		onToggleHARTO = new TopicSelectedEvent.Handler(OnToggleHARTO);
		GameEventsManager.Instance.Register<TopicSelectedEvent>(onToggleHARTO);

		player = GameObject.Find(ASTRID).GetComponent<FirstPersonController>();
	}

	void OnToggleHARTO(GameEvent e)
	{
		Debug.Log("HERE");
		
	}

	IEnumerator FadeHARTO()
	{
		Debug.Log("Now");
		
			float t = 0;
			while (t < 1)
			{
				if (isHARTOActive)
				{
					for (int i = 0; i < emotionWheelIcons.Length; i++)
					{
						emotionWheelIcons[i].myIcon.color = Color.Lerp(emotionWheelIcons[i].myColor, transparent, t);
					}
					for (int i = 0; i < topicWheelIcons.Length; i++)
					{
						topicWheelIcons[i].myIcon.color = Color.Lerp(topicWheelIcons[i].myColor, transparent, t);
					}
					for (int i = 0; i < canvas.transform.childCount; i++)
					{
						canvas.transform.GetChild(i).GetComponent<Image>().color = Color.Lerp(canvas.transform.GetChild(i).GetComponent<Image>().color, transparent, t);
					}
				}
				else
				{
					for (int i = 0; i < emotionWheelIcons.Length; i++)
					{
						emotionWheelIcons[i].myIcon.color  = Color.Lerp(transparent, opaqueTopic, t);
					}
					for (int i = 0; i < topicWheelIcons.Length; i++)
					{
						topicWheelIcons[i].myIcon.color  = Color.Lerp(transparent, opaqueTopic, t);
					}
					for (int i = 0; i < canvas.transform.childCount - 1; i++)
					{
						
						if (canvas.transform.GetChild(i) != selectionArea)
						{
							canvas.transform.GetChild(i).GetComponent<Image>().color = Color.Lerp(transparent, opaqueWheel,t);
						}
						else
						{
							canvas.transform.GetChild(i).GetComponent<Image>().color = Color.Lerp(transparent, selectionAreaColor,t);
						}
					}
					canvas.transform.GetChild(2).GetComponent<Image>().color = Color.Lerp(transparent, selectionAreaColor,t);
				}
			t += Time.deltaTime;
		}

		isHARTOActive = !isHARTOActive;

		yield return new WaitForEndOfFrame();
	}
	
	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	RotateLevel: Rotates the enitre level												*/
    /*		param: float z - the value taken from the scroll wheel input					*/
	/*																						*/
    /*--------------------------------------------------------------------------------------*/
	void RotateEmotionWheel(float z, bool topicHasBeenSelected)
	{
		//	Where the rotation magic happens
		if (!topicHasBeenSelected)
		{
			topicWheel.transform.rotation = Quaternion.Euler(topicWheel.transform.rotation.x, topicWheel.transform.rotation.y, z * rotationSpeed);
		}
		else
		{
			emotionWheel.transform.rotation = Quaternion.Euler(emotionWheel.transform.rotation.x, emotionWheel.transform.rotation.y, z * rotationSpeed);
		}
	}

	void SelectIcon(bool topicHasBeenSelected)
	{
		Icon[] myIconArray;
		if (topicHasBeenSelected)
		{
			myIconArray = emotionIcons;
		}
		else
		{
			myIconArray = topicWheelIcons;
		}

		for (int i = 0; i < myIconArray.Length; i++)
		{		
			if (myIconArray[i].transform.position.x < selectionArea.transform.position.x + selectionAreaWidth &&
				myIconArray[i].transform.position.x > selectionArea.transform.position.x - selectionAreaWidth &&
				Input.GetKeyDown(KeyCode.Mouse0))
			{
				myIconArray[i].myColor = Icon.activeColor;
				if (topicHasBeenSelected)
				{
					currentEmotion = ((EmotionIcon)myIconArray[i]).emotion;
					GameEventsManager.Instance.Fire(new EmotionSelectedEvent(this));
				}
				else
				{
					currentTopic = myIconArray[i];
					topicHasBeenSelected = true;
					GameEventsManager.Instance.Fire(new TopicSelectedEvent(this, player));
				}
			}
			else
			{
				myIconArray[i].myColor = Icon.inactiveColor;
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			SelectIcon(topicSelected);
		}

		if (Input.GetKeyDown(toggleHARTO))
		{
			GameEventManager.GameEventsManager.Instance.Fire(new ToggleHARTOEvent());
			StartCoroutine(FadeHARTO());
		}

		rotateHARTO = rotateHARTO +  Input.GetAxis (SCROLLWHEEL) * Time.deltaTime;
		RotateEmotionWheel(rotateHARTO, topicSelected);
	}
}
