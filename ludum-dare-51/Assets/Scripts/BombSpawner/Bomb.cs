using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public ParticleSystem particles;

    void Start() {
        StartCoroutine(BlowUp());
    }

    IEnumerator BlowUp() {
        yield return new WaitForSeconds(3);
        particles.Play();
        CameraShake.Shake(0.25f, 2f);
        GameObject.FindGameObjectWithTag("BombSpawner").GetComponent<BombSpawner>().SetBombCountTime(0);
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().BombExploded();
        Destroy(gameObject);
    }
}
