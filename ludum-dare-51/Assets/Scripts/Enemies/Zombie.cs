using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    private GameObject player;
    public NavMeshAgent agent;

    public GameObject healthUp;
    public GameObject forcefieldUp;
    public GameObject ammoUp;

    public float range = 1f;
    private float health;

	private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        health = 10;
	}

	private void Update() {
        float dist = Vector3.Distance(this.transform.position, player.transform.position);

       if (dist > range) {
            agent.destination = player.transform.position;
	   } else {
            player.SendMessage("ZombieBite");
            KillZombie(false);

       }
        if (health <= 0) KillZombie(true);
    }

    public void DamageZombie(float damage) {
        health -= damage;
    }

    private void KillZombie(bool withDrop) {
        if (withDrop) {
            SpawnDeathDropChance();
        }
        Destroy(gameObject);
    }

    private Vector3 getRandomSpawnPos()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y = 1.5f;
        spawnPos.x += Random.Range(-1, 1);
        spawnPos.z += Random.Range(-1, 1);
        return spawnPos;
    }
    private void SpawnDeathDropChance() {
        int rand = Random.Range(0, 12);

        Vector3 spawnPos = getRandomSpawnPos();

        if (rand == 1) {
            Instantiate(healthUp, spawnPos, Quaternion.identity);
		} else if (rand >= 2 && rand <= 9) {
            Instantiate(forcefieldUp, spawnPos, Quaternion.identity);
        } else {
            Instantiate(ammoUp, spawnPos, Quaternion.identity);
        }
        Instantiate(ammoUp, getRandomSpawnPos(), Quaternion.identity);
    }
}
