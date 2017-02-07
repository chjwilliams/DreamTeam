using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

	//declare the variables we need
	public bool isjumping;
	public bool iswalking;
	public float movespeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");

		//isjumping = Input.GetAxis("Jump");

		PlayerMove (x,z);
	}

	//This is the function makes player moving.
	void PlayerMove(float x, float z){

		transform.position = new Vector3 (transform.position.x + x * movespeed * Time.deltaTime,
			transform.position.y, transform.position.z + z * movespeed * Time.deltaTime);
		
	}
}
