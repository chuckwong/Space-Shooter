using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	public float speed;

	void Start ()
	{
		Rigidbody rigibody = GetComponent<Rigidbody>();

		rigibody.velocity = transform.forward * speed;
	}

}
