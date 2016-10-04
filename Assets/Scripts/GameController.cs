using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public GameObject hazard; // Drag n' drop in Unity
	public Vector3 spawnValues; // Set in Unity
	public int hazardCount; // Set in Unity
	public float spawnWait; // Time between spawning each asteroid 
	public float startWait; // Wait before the 1st wave
	public float waveWait; // Wait before each wave

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

	private bool gameOver;
	private bool restart;
	private int score; // Score is set in Asteroid prefab in Unity


	// Use this for initialization
	void Start () {
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";

		score = 0;
		UpdateScore ();

		StartCoroutine (SpawnWaves ());
	}

	void Update () {
		// When 'restart' is true, let users press 'R' to restart the game
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);

		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (
				// A random spawn point for asteroids between the range of neg & 
				// pos x values set in Unity editor
				           Random.Range (-spawnValues.x, spawnValues.x), 
				           spawnValues.y, 
				           spawnValues.z);

				// Spawn with no additional rotation
				Quaternion spawnRotation = Quaternion.identity; 

				Instantiate (hazard, spawnPosition, spawnRotation);

				// Wait for the specified number of seconds set in Unity
				yield return new WaitForSeconds (spawnWait);
			}

			yield return new WaitForSeconds (waveWait);

			// if gameOver is true
			if (gameOver) {
				// Prompt user to restart
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break; // Break out spawning new waves
			}
		}
	}


	public void AddScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}

	public void GameOver () {
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
