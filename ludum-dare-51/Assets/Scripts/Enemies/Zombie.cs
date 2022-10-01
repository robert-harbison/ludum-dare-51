using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    private GameObject player;
    public NavMeshAgent agent;

    public GameObject healthUp;
    public GameObject forcefieldUp;

    public float range = 1f;

	private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update() {
        float dist = Vector3.Distance(this.transform.position, player.transform.position);

       if (dist > range) {
            agent.destination = player.transform.position;
	    } else {
            player.SendMessage("ZombieBite");
            KillZombie(false);
		}
    }

    private void KillZombie(bool withDrop) {
        if (withDrop) {
            SpawnDeathDropChance();
        }
        Destroy(gameObject);
    }

    private void SpawnDeathDropChance() {
        int rand = Random.Range(0, 11);

        Vector3 spawnPos = transform.position;
        spawnPos.y = 1.5f;

        if (rand == 1) {
            Instantiate(healthUp, spawnPos, Quaternion.identity);
		} else if (rand >= 2 && rand <= 9) {
            Instantiate(forcefieldUp, spawnPos, Quaternion.identity);
        }
	}
}
