using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance { get; private set; }

	public int currentEnemyCount = 0;

	public int playerKills = 0;

	public TMPro.TMP_Text killText;

	private void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
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
