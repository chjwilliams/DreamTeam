using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour 
{
	public Image myIcon;
	public Color myColor;
	public static Color inactiveColor = new Color (1.0f, 1.0f, 1.0f, 0.5f);
	public static Color activeColor  = new Color (1.0f, 1.0f, 1.0f, 1.0f);

	// Use this for initialization
	protected void Start () 
	{
		myIcon = GetComponent<Image>();	
		myColor = myIcon.color;

		myColor = inactiveColor;
	}
	
	// Update is called once per frame
	protected void Update () 
	{
		//myIcon.color = myColor;
	}
}
