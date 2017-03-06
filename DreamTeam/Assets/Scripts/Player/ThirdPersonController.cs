using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour {

	public bool isjumping;
	public bool iswalking;
	public float movespeed;
	public float zoomvalue = 0;
	public float mousesensitivity = 10f;
	public Transform player;
	public Transform playercamera;
	public Transform centerpoint;

	private float _moveFB;
	private float _moveLR;
	private float _mousex;
	private float _mousey;
	private float _maxzoomin = -4.0f;
	private float _maxzoomout = -1.0f;
	private float _rotatespeed = 2.0f;

	// Use this for initialization
	void Start () {
		zoomvalue = _maxzoomout;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gm.thirdPersonActive) {

		//float x = Input.GetAxis ("Horizontal");
		//float z = Input.GetAxis ("Vertical");
		zoomvalue += Input.GetAxis ("Mouse ScrollWheel");
		if (zoomvalue < _maxzoomin) {
			zoomvalue = _maxzoomin;
		}

		if (zoomvalue > _maxzoomout) {
			zoomvalue = _maxzoomout;
		}

		if (Input.GetMouseButton (1)) {
			_mousex += Input.GetAxis ("Mouse X") * _rotatespeed;
			_mousey -= Input.GetAxis ("Mouse Y") * _rotatespeed;
			rotateCamera (_mousex, _mousey);
		}
			
		zoomCamera (zoomvalue);
	}
	}

	void zoomCamera(float camerazoom) {
		Camera.main.transform.localPosition = new Vector3 (0, 0, camerazoom);
	}

	void rotateCamera(float x, float y){
		y = Mathf.Clamp (y, -60f, 60f);
		playercamera.LookAt (centerpoint);
		centerpoint.localRotation = Quaternion.Euler (y, x, 0);
	}
}
