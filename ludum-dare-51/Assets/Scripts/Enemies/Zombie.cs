using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    private GameObject player;
    public NavMeshAgent agent;

    public int range;

	private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update() {
        float dist = Vector3.Distance(this.transform.position, player.transform.position);


       // if (dist < range) {

            agent.destination = player.transform.position;
		//}
    }
}
