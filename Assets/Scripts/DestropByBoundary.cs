using UnityEngine;
using System.Collections;

public class DestropByBoundary : MonoBehaviour 
{
	void OnTriggerExit (Collider other)
	{
		Destroy (other.gameObject);
	}
}
