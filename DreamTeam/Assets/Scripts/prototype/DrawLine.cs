using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour 
{

	public const string HARTO_NODE = "HARTONode";
	public bool drawingLine;
	private LineRenderer lineRenderer;
	private float counter;
	private float distance;

	public Vector3 origin;
	public Vector3 destination;

	public float lineDrawSpeed = 6.0f;

	public GameObject thisLine;
	GameObject newLine;

	RaycastHit hit;
	Ray ray;

	// Use this for initialization
	void Start () 
	{
		//lineRenderer = GetComponent<LineRenderer>();
		//lineComplete = false;
	}

	// next step dynamically change the destination positonbased on click and drag
	
	void DrawMyLine(Vector3 originPoint, Vector3 destinationPoint, float distance)
	{
			//counter += 0.1f / lineDrawSpeed;

			//float x = Mathf.Lerp(0, distance, counter);

			//Vector3 pointA = originPoint;
			//Vector3 pointB = destinationPoint;

			//Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

	}

	// Update is called once per frame
	void Update () 
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      	if(Physics.Raycast(ray,out hit))
		  {
        	if(hit.collider.tag == HARTO_NODE)
			{
                if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					newLine = (GameObject)Instantiate(thisLine,origin, transform.rotation);
					Destroy(newLine.GetComponent<DrawLine>());
					lineRenderer = newLine.GetComponent<LineRenderer>();
					lineRenderer.startWidth = 0.1f;
					lineRenderer.endWidth = 0.1f;
					drawingLine = true;
					lineRenderer.SetPosition(1, new Vector3(origin.x, origin.y, 0));
					Debug.Log("SET POSITON");
					//origin.z = 0;
				}

				
			}
      	}

		
		
		if (Input.GetKey(KeyCode.Mouse0) && drawingLine)
		{
			Debug.Log ("UPDATE POSITION");
			
			destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
			lineRenderer.SetPosition(0, new Vector3(destination.x, destination.y, 0));
			
		}

		
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			drawingLine = false;
			if(!Physics.Raycast(ray,out hit))
			{
				Destroy(newLine);
			}
		}
		
	}

	void LateUpdate()
	{
		
	}
}
