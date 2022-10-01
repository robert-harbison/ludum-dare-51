using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public TMPro.TMP_Text hasForcefieldText;
	public TMPro.TMP_Text healthText;

	public float speed = 6.0F;
	public float gravity = 20.0F;
	public float sprintEnergy = 100;

	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;

	private int health = 3;
	public GameObject forcefield;

	private bool hasForcefield = true;

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

	void Update()
	{
		Move();
		Rotate();
		HandleForcefield();
		healthText.text = "Health: " + health + " / 3";
		if (hasForcefield) {
			hasForcefieldText.color = Color.blue;
		} else {
			hasForcefieldText.color = Color.gray;
		}
	}

	private void HandleForcefield() {
		if (Input.GetKey(KeyCode.F)) {
			if (hasForcefield) {
				forcefield.SetActive(true);
				hasForcefield = false;
			}
		} else if (forcefield.activeSelf) {
			forcefield.SetActive(false);
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "PowerUp") {
			PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();

			switch (powerUp.type) {
				case PowerUpType.FORCEFIELD:
					hasForcefield = true;
					break;
				case PowerUpType.HEALTH:
					health += 1;
					break;
				default:
					break;
			}
		}
	}

	public void ZombieBite() {
		health -= 1;
		if (health <= 0) {
			KillPlayer();
		}
	}

	private void KillPlayer() {
		Destroy(gameObject);
	}

	public void BombExploded() {
		if (!forcefield.activeSelf) {
			KillPlayer();
		}
	}
}