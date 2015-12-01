using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}


public class PlayerController: MonoBehaviour 
{
	public float speed;
	public float verticalTilt;
	public float horizontalTilt;
	
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;

	private float nextFire;

	private int fireWeapon;

	void Start ()
	{
		// Initial FireWeapon 0;
		fireWeapon = 0;
	}

	void Update ()
	{
		// Fire
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			if (fireWeapon == 0) {
				Instantiate (shot, shotSpawn.position, Quaternion.identity); // as GameObject;
			} else {
				Vector3 position1 = shotSpawn.position;
				Vector3 position2 = shotSpawn.position;
				position1.x = position1.x - 0.3f;
				position2.x = position2.x + 0.3f;
				Instantiate (shot, position1, Quaternion.identity);
				Instantiate (shot, position2, Quaternion.identity);
			}

			GetComponent<AudioSource>().Play ();
		}

		// FireWeapon
		if (Input.GetKeyDown(KeyCode.Space)) {
			fireWeapon = (fireWeapon == 0) ? 1 : 0;
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		Rigidbody rigidbody = GetComponent<Rigidbody>();

		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3
		(
			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
		);

		rigidbody.rotation = Quaternion.Euler (rigidbody.velocity.z * verticalTilt, 0.0f, rigidbody.velocity.x * -horizontalTilt);
	}

	void OnGUI ()
	{
		if (fireWeapon == 0) {
			GUI.Label (new Rect(Screen.width - 70, Screen.height - 50, 50, 30), "Single");
		} else {
			GUI.Label (new Rect(Screen.width - 70, Screen.height - 50, 50, 30), "Double");
		}
	}
}
