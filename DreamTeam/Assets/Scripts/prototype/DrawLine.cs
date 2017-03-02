using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	DrawLine: Draws the line                           									*/
/*			Functions:																	*/
/*					public:																*/
/*						                        										*/
/*					proteceted:															*/
/*                                                                                      */
/*					private:															*/
/*						void Start ()													*/
/*						void Update ()													*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class DrawLine : MonoBehaviour 
{
	//	Public Constant Variables
	public const string HARTO_NODE = "HARTONode";				//	String reference to the HARTONODE tag.

	//	Public Variables
	public bool drawingLine;									//	Returns true if you are drawing a line
	public Vector3 origin;										//	Origin point for the line
	public Vector3 destination;									//	Where the line ends up
	public GameObject thisLine;									//	Literally this line
	public GameObject newLine;									//	The next line to be drawn

	//	Private Variables
	private RaycastHit hit;										//	For cursour detection on HARTONODES
	private Ray ray;											//	For cursor detection on HARTONODES
	private LineRenderer lineRenderer;							//	Reference to Line Renderer
	
	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Start: Runs once at the begining of the game. Initalizes variables.					*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Start () 
	{
		
	}

	/*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Update: Called once per frame														*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
	void Update () 
	{
		//	Connects mose position on screen to game screen
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		//	If the mouse ray collide with something go into this if-statement
      	if(Physics.Raycast(ray,out hit))
		  {
			//	If it collides with a HARTONODE go into this if-statement
        	if(hit.collider.tag == HARTO_NODE)
			{
				//	Waits for player to left click
                if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					//	Sets origin of line to the HARTONODE where the player clicked
					origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					//	Makes a new line at the origin point of the line based on this gameObject
					newLine = (GameObject)Instantiate(thisLine,origin, transform.rotation);
					//	We only need one game object with the DrawLine script. Destroy this script on the new game object
					Destroy(newLine.GetComponent<DrawLine>());
					//	Have lineRenderer reference the LineRender component on the new line
					lineRenderer = newLine.GetComponent<LineRenderer>();
					//	Sets the starting and end width of the line
					lineRenderer.startWidth = 0.1f;
					lineRenderer.endWidth = 0.1f;
					//	We are now drawing a line
					drawingLine = true;
					//	Sets position on the line renderer
					lineRenderer.SetPosition(1, new Vector3(origin.x, origin.y, 0));
				}				
			}
      	}
		
		//	Keep left mouse button down to keep drawing. 
		if (Input.GetKey(KeyCode.Mouse0) && drawingLine)
		{
			//	Sets the end point of the line to where ever your mouse position is on screen (and off screen?)
			destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			lineRenderer.SetPosition(0, new Vector3(destination.x, destination.y, 0));
		}

		//	When you release the left mouse button
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			//	When you release the left mouse button you are no longer drawing the line
			drawingLine = false;
			//	If we are not colliding with a HARTONODE destroy the new line
			if(!Physics.Raycast(ray,out hit))
			{
				//	This line is USELESS! It needs to connect to a node! DESTROY IT!!!!!
				Destroy(newLine);
			}
		}
	}
}
