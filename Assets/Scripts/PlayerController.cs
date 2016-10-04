using UnityEngine;
using System.Collections;

// Without this attribute, the class won't get serialized and won't appear in
// Unity editor
[System.Serializable] 
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private AudioSource audio;
	public float speed;
	public Boundary boundary;
	public float tilt;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		audio = GetComponent<AudioSource> ();
	}

	void Update (){
		// Time.time returns the time since the game started
		// GetKeyDown () only fires if the key is pressed, GetKey () lets 
		// players hold the button down
		if (Input.GetKey (KeyCode.Space) && Time.time >= nextFire) {
			nextFire = Time.time + fireRate;
			//GameObject clone = 
			Instantiate (shot, shotSpawn.position, 
				shotSpawn.rotation); //as GameObject;

			// Play the attached AudioSource
			audio.Play ();
		}
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		// Tilting along the z-axis according to how fast the ship is moving 
		// left & right multiplied by negative tilt (or else the ship will tilt
		// in the opposite direction)
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);

		rb.position = new Vector3 (
			// Clamping, or limiting the position of the game object rigidbody 
			// within and xMax usint math function
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);
	}
}
