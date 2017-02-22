using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HARTOParticleManager : MonoBehaviour 
{
	public float cycleInterval = 0.007f;					//	increase to make force greater

	public float moveSpeed = 5.0f;
	public KeyCode upKey = KeyCode.UpArrow;			//	Input for moving up
	public KeyCode downKey = KeyCode.DownArrow;			//	Input for moving down
	public KeyCode rightKey = KeyCode.RightArrow;		//	Input for moving right
	public KeyCode leftKey = KeyCode.LeftArrow;			//	Input for moving left


	public const string HORIZONTAL = "Horizontal";
	public const string VERTICAL = "Vertical";
	public GameObject hartoNode;

	private List<HARTODisplayParticle> hartoParticles;
	private List<MovingBrocaParticle> brocaParticles;

	// Use this for initialization
	void Start () 
	{
		hartoNode = GameObject.FindGameObjectWithTag("HARTONode");
		hartoParticles = new List<HARTODisplayParticle>(FindObjectsOfType<HARTODisplayParticle>());
		brocaParticles = new List<MovingBrocaParticle>(FindObjectsOfType<MovingBrocaParticle>());

		foreach(MovingBrocaParticle brocaParticle in brocaParticles)
		{
			StartCoroutine(Cycle(brocaParticle));
		}

	}

	public IEnumerator Cycle(MovingBrocaParticle brocaParticle)
	{
		bool isFirst = true;
		while(true)
		{
			if (isFirst)
			{
				isFirst = false;
				yield return new WaitForSeconds(Random.value * cycleInterval);
			}
			ApplyMagneticForce(brocaParticle);
			yield return new WaitForSeconds(cycleInterval);
		}
	}

	private void ApplyMagneticForce(MovingBrocaParticle brocaParticle)
	{
		Vector3 newFocrce = Vector3.zero;

		foreach(HARTODisplayParticle hartoParticle in hartoParticles)
		{
			if (hartoParticle == brocaParticle)
			{
				continue;
			}

			float distance = Vector3.Distance(brocaParticle.transform.position, hartoParticle.gameObject.transform.position);
			float force = 1000 * brocaParticle.charge *  hartoParticle.charge / Mathf.Pow(distance, 2);

			Vector3 direction = brocaParticle.transform.position - hartoParticle.transform.position;
			direction.Normalize();

			newFocrce += force * direction * cycleInterval;

			//	If force is undefined (divide by zero) the newForce is zero
			if (float.IsNaN(newFocrce.x))
			{
				newFocrce = Vector3.zero;
			}

			brocaParticle.rb.AddForce(newFocrce);
		}
	}
	
	// Update is called once per frame
	void Move(float dx, float dy)
	{
		//GetComponent<Rigidbody>().AddForce(new Vector3(0, dy * moveSpeed * Time.deltaTime, dx * moveSpeed * Time.deltaTime));
	}

	void MoveNode(KeyCode key)
	{
		if (Input.GetKey(rightKey))
		{
			transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey(leftKey))
		{
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}

		if (Input.GetKey(upKey))
		{
			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
		}
		else if (Input.GetKey(downKey))
		{
			transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
		}
	}

	// Update is called once per frame
	void Update () {
		if (CompareTag("HARTONode"))
		{
			float x = Input.GetAxis(HORIZONTAL);
			float y = Input.GetAxis(VERTICAL);
			//Move(-x, y);
			MoveNode(rightKey);
			MoveNode(leftKey);
			MoveNode(upKey);
			MoveNode(downKey);
			
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (CompareTag("HARTONode"))
		{
			if (other.CompareTag("Attract"))
			{
				foreach (MovingBrocaParticle brocaParticle in brocaParticles)
				{
					brocaParticle.rb.constraints = RigidbodyConstraints.FreezePositionX;
				}
			}

			if (other.CompareTag("Repel"))
			{
				hartoNode.GetComponent<HARTODisplayParticle>().charge = -15;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (CompareTag("HARTONode"))
		{
			if (other.CompareTag("Attract"))
			{
				foreach (MovingBrocaParticle brocaParticle in brocaParticles)
				{
					brocaParticle.rb.constraints = RigidbodyConstraints.None;
				}
			}

			if (other.CompareTag("Repel"))
			{
				hartoNode.GetComponent<HARTODisplayParticle>().charge = 1;
			}
		}
	}
}
