using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {
    public TMPro.TMP_Text text;

	private BombSpawner spawner;

	private void Start() {
		spawner = GameObject.FindGameObjectWithTag("BombSpawner").GetComponent<BombSpawner>();
		text.text = GetTimeTillExplosion().ToString();
	}

	private void Update() {
		float timeLeft = GetTimeTillExplosion();
		text.text = timeLeft.ToString();
		if (timeLeft < 3) {
			text.color = Color.red;
		} else {
			text.color = Color.white;
		}
	}

	private float GetTimeTillExplosion() {
		float timeLeft = 10f - spawner.GetBombCountTime();
		return Mathf.Round(timeLeft * 10.0f) * 0.1f;
	}
}
