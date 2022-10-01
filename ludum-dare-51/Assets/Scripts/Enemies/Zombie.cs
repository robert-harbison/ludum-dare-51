using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    private GameObject player;
    public NavMeshAgent agent;
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
            Destroy(gameObject);
	   }
        if (health <= 0) Destroy(gameObject);
    }

    public void DamageZombie(float damage)
    {
        health -= damage;
    }
}
