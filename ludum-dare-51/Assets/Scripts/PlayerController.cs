using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public TMPro.TMP_Text hasForcefieldText;
	public TMPro.TMP_Text healthText;
	public TMPro.TMP_Text ammoText;

	public float speed = 6.0F;
	public float gravity = 20.0F;
	public float sprintEnergy = 100;

	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;

	private int health = 3;
	public GameObject forcefield;
	private float forceFieldMaxTime = 3f;
	private float forceFieldTimer = 0f;

	private bool hasForcefield = true;
	private int ammo = 10;
	private bool isDead = false;

	void Start()
	{
		// Store reference to attached component
		controller = GetComponent<CharacterController>();
		forcefield.SetActive(false);
	}

	void Move()
    {
		// Character is on ground (built-in functionality of Character Controller)
		if (controller.isGrounded)
		{
			// Use input up and down for direction, multiplied by speed
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
		}
		// Apply gravity manually.
		moveDirection.y -= gravity * Time.deltaTime;
		// Move Character Controller
		controller.Move(moveDirection * Time.deltaTime);
	}

	private void Rotate()
    {
		transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 4f, 0));
	}

	public int GetAmmo() {
		return ammo;
	}

	void Update()
	{
		if (isDead) return;
		Move();
		Rotate();
		HandleForcefield();
		healthText.text = "Health: " + health + " / 3";
		ammoText.text = "Ammo: " + ammo + "/ 10";
		if (hasForcefield) {
			hasForcefieldText.color = Color.blue;
		} else {
			hasForcefieldText.color = Color.gray;
		}
	}

	public bool GetIsDead() {
		return isDead;
	}

	private void HandleForcefield() {
		if (Input.GetKey(KeyCode.LeftShift) && forceFieldTimer < forceFieldMaxTime) {
			forceFieldTimer += Time.deltaTime;
			if (hasForcefield) {
				forcefield.SetActive(true);
				hasForcefield = false;
			}
		} else if (forcefield.activeSelf) {
			forceFieldTimer = 0;
			forcefield.SetActive(false);
		}
	}

	public void ReduceAmmo() {
		ammo--;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "PowerUp") {
			PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();

			switch (powerUp.type) {
				case PowerUpType.FORCEFIELD:
					hasForcefield = true;
					break;
				case PowerUpType.HEALTH:
					if (health < 3) health += 1;
					break;
				case PowerUpType.AMMO:
					if (ammo < 10)
                    {
						ammo++;
					}
					break;
				default:
					break;
			}

			Destroy(other.gameObject);
		}
	}

	public void ZombieBite() {
		health -= 1;
		if (health <= 0) {
			KillPlayer();
		}
	}

	private void KillPlayer() {
		//Destroy(gameObject);
		isDead = true;
		GetComponent<MeshRenderer>().enabled = false;
	}

	public bool IsForcefieldOpen() {
		return forcefield.activeSelf;
	}

	public void BombExploded() {
		if (!forcefield.activeSelf) {
			KillPlayer();
		}
	}
}