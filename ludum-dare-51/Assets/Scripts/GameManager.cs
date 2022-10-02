using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance { get; private set; }

	public int currentEnemyCount = 0;

	public int playerKills = 0;

	public TMPro.TMP_Text killText;
	public TMPro.TMP_Text gameOverKillsText;
	public TMPro.TMP_Text highscoreText;
	public GameObject player;

	// Game start
	public GameObject gameStartPanel;

	// Game over
	public GameObject gameOverPanel;

	private void GameStart()
    {
		player.SetActive(true);
		gameOverPanel.SetActive(false);
		gameStartPanel.SetActive(true);
    }

	public void GameEnd()
    {
		gameOverKillsText.text = "Kills: " + playerKills;
		highscoreText.text = playerKills >= PlayerPrefs.GetInt("Highscore", 0) ?
			"New Highscore: " + playerKills
			: "Highscore: " + PlayerPrefs.GetInt("Highscore", 0).ToString();
		gameStartPanel.SetActive(false);
		gameOverPanel.SetActive(true);
		Cursor.lockState = CursorLockMode.None;
    }

	private void Awake() {
		if (instance == null) {
			instance = this;
			GameStart();
		}
	}

	private void Update() {
		killText.text = "Kills: " + playerKills;
	}

	public void SaveHighscore() {
		if (PlayerPrefs.GetInt("Highscore", 0) < playerKills) {
			PlayerPrefs.SetInt("Highscore", playerKills);
		} 
	}
}
