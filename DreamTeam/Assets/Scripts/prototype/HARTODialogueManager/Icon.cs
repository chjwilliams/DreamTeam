using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour 
{
	public Image myIcon;
	public bool selected;
	public static float alphaLimit;
	public static Color inactiveColor;
	public static Color activeColor;

	protected HARTOTuningv3Script astridHARTO;

	// Use this for initialization
	protected void Start () 
	{
		selected = false;
		alphaLimit = 0.5f;
		myIcon = GetComponent<Image>();	
		inactiveColor = new Color (1.0f, 1.0f, 1.0f, alphaLimit);
		activeColor  = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		astridHARTO = GameObject.Find("EmotionWheelUI").GetComponent<HARTOTuningv3Script>();

		myIcon.color = inactiveColor;
	}
	
	// Update is called once per frame
	protected void Update () 
	{
		if (!astridHARTO.isHARTOActive)
		{
			selected = false;
		}
		
		if (myIcon.color.a > alphaLimit && astridHARTO.isHARTOActive)
		{	
			myIcon.color = inactiveColor;
		}

		
	}
}
