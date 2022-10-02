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

    public Animator anim;

    public float range = 1f;
    private float health;

    private Rigidbody rb;

    private PlayerController playerController;

	private void Start() {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        health = 10;
	}

	private void Update() {
        float dist = Vector3.Distance(this.transform.position, player.transform.position);

        bool isForceFieldOpen = player.GetComponent<PlayerController>().IsForcefieldOpen();

        float biteRange = isForceFieldOpen ? 3f : range;

       if (dist > biteRange) {
            if (!playerController.GetIsDead()) {
                agent.destination = player.transform.position;
            }
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
        anim.SetBool("isLaunching", true);

    }

    private void DisablePhysics() {
        rb.isKinematic = true;
        agent.enabled = true;
        anim.SetBool("isLaunching", false);
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
