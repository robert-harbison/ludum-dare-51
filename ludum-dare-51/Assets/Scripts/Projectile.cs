using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 500000000.0f;
    // Start is called before the first frame update
    private Rigidbody rb;
    private float maxDuration = 6f;
    private float duration = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        duration += Time.deltaTime;
        if (duration >= maxDuration) Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie")){
            collision.gameObject.SendMessage("DamageZombie", 10);
        }

        Destroy(gameObject);
    }
}
