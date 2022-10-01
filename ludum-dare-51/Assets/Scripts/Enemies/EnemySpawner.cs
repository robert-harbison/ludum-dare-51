using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float timeBetweenSpawn = 3f;
	public GameObject enemyToSpawn;

    private float timeSinceLastSpawn = 0;

	private void Update() {
		if (timeSinceLastSpawn >= timeBetweenSpawn) {
			SpawnEnemy();
			timeSinceLastSpawn = 0;
		} else {
			timeSinceLastSpawn += Time.deltaTime;
		}
	}

	private void SpawnEnemy() {
		GameManager.instance.currentEnemyCount++;
		Instantiate(enemyToSpawn, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
	}
}
