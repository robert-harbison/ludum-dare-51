using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectile;
    private GameObject player;
    public AudioSource shoot;

    private void FireProjectile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Cursor.lockState == CursorLockMode.Locked) {
                PlayerController playerController = player.GetComponent<PlayerController>();
                if (playerController.GetAmmo() > 0 && !playerController.GetIsDead()) {
                    Instantiate(projectile, transform.position, player.transform.rotation);
                    shoot.Play();
                    playerController.ReduceAmmo();
                }
            } else {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FireProjectile();
    }
}
