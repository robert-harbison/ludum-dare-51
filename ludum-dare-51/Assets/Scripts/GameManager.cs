using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance { get; private set; }

	public int currentEnemyCount = 0;

	private void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}
}
