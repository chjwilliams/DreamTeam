using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HARTODisplayParticle : MonoBehaviour 
{
	public float charge = 1.0f;


	public Color hartoNode = new Color (0.234f, 0.234f, 0.234f);
	public Color positiveColor = new Color (1.0f, 1.0f, 0f);
	public Color negativeColor = new Color (0.234f, 0.449f, 0.691f);
	// Use this for initialization
	void Start () {
		UpdateColor();
		if (CompareTag("HARTONode"))
		{
			GetComponent<Renderer>().material.color = hartoNode;
		}
	}

	public void UpdateColor()
	{
		Color color = charge > 0? positiveColor: negativeColor;
		GetComponent<Renderer>().material.color = color;
	}
	

	
}
