using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HARTOParticleManager : MonoBehaviour 
{
	public float cycleInterval = 0.007f;					//	increase to make force greater
	private List<HARTODisplayParticle> hartoParticles;
	private List<MovingBrocaParticle> brocaParticles;

	// Use this for initialization
	void Start () 
	{
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
	void Update () {
		
	}
}
