using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject hazard;

	public Vector3 spawnValues;

	public int hazardCount;

	public float spawnWait;

	public float startWait;

	public float waveWait;

	private int score;

	private bool gameOver;

	private bool restart;

	void Start ()
	{
		gameOver = false;
		restart = false;
		score = 0;
		StartCoroutine (SpawnWaves ());
	}
	
	void Update ()
	{
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);

		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);

				yield return new WaitForSeconds (spawnWait);
			}

			yield return new WaitForSeconds	(waveWait);

			if (gameOver) {
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
	}

	public void GameOver ()
	{
		gameOver = true;
	}

	void OnGUI ()
	{
		GUI.Label (new Rect(20, 20, 100, 20), "Score: " + score);

		if (gameOver) {
			GUI.Label (new Rect(20, 50, 100, 20), "Game Over!");
		}

		if (restart) {
			GUI.Label (new Rect(20, 80, 200, 20), "Press 'R' For Restart");
		}
	}
}







