using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float speed = 6.0F;
	public float gravity = 20.0F;
	public float sprintEnergy = 100;

	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;

	void Start()
	{
		// Store reference to attached component
		controller = GetComponent<CharacterController>();
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
	}
}