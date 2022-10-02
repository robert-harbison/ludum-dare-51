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
    private float health;

    private Rigidbody rb;

	private void Start() {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = 10;
	}

	private void Update() {
        float dist = Vector3.Distance(this.transform.position, player.transform.position);

        bool isForceFieldOpen = player.GetComponent<PlayerController>().IsForcefieldOpen();

        float biteRange = isForceFieldOpen ? 3f : range;

       if (dist > biteRange) {
            agent.destination = player.transform.position;
	   } else {
            if (!isForceFieldOpen) {
                player.SendMessage("ZombieBite");
                KillZombie(false);
            } else {
                EnablePhysics();
                Vector3 launch = transform.forward * -1;
                launch.y = 1.5f;
                rb.AddForce(launch, ForceMode.VelocityChange);
            }
       }

        if (health <= 0) KillZombie(true);

        transform.LookAt(player.transform);
    }

	private void OnCollisionStay(Collision collision) {
		if (collision.gameObject.tag == "Arena") {
            DisablePhysics();
		}
	}

	private void EnablePhysics() {
        rb.isKinematic = false;
        agent.enabled = false;
	}

    private void DisablePhysics() {
        rb.isKinematic = true;
        agent.enabled = true;
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
