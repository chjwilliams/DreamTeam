using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBrocaParticle : HARTODisplayParticle 
{
	public float mass = 1.0f;
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		UpdateColor();
		rb = gameObject.AddComponent<Rigidbody>();
		rb.mass = mass;
		rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
