using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;
	public int score;
	private GameController gameController;

	void Start () {
		GameObject gameObjectController = GameObject.FindWithTag ("GameController");

		if (gameObjectController != null) {
			gameController = gameObjectController.GetComponent<GameController> ();
		}
		if (gameObjectController == null) {
			Debug.Log ("Cannot find GameController script");
		}
	}

	// When another object comes into contact with this trigger
	void OnTriggerEnter(Collider other) {
		// The code below won't be executed if the other's tag is "Boundary"
		if (other.tag == "Boundary") { return; }

		//Debug.Log (other.name);

		// Spawn a new explosion with the position and rotation applied
		Instantiate (explosion, transform.position, transform.rotation);

		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, 
				other.transform.rotation);

			// When player collides with asteroid, call GameOver () in 
			// GameController.cs
			gameController.GameOver ();
		}

		// They're both destroyed at the end of the same frame, so the order 
		// they're listed doesn't matter
		Destroy(other.gameObject);
		Destroy (gameObject);
		gameController.AddScore (score);
	}
}
