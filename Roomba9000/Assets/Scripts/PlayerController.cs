using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float acceleration;
	public float maxMovementSpeed;
	public float rotationSpeed;
	public float jumpPower;

	public Transform bodyTransform;
	private Rigidbody rigidBody;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Runs before Start()
	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		RotatePlayerBasedOnInput();
		MovePlayerBasedOnInput();

		if (rigidBody.velocity.magnitude > maxMovementSpeed)
		{
			rigidBody.velocity = rigidBody.velocity.normalized * maxMovementSpeed;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
	}

	private void RotatePlayerBasedOnInput()
	{
		// Get how much input was received.
		float inputAmount = Input.GetAxis("Horizontal");
		if (inputAmount == 0f)
			return;

		Vector3 rotationAmount = new Vector3(0, inputAmount, 0) * rotationSpeed * Time.deltaTime;
		rigidBody.transform.Rotate(rotationAmount);
	}

	private void MovePlayerBasedOnInput()
	{
		// Get how much input was received.
		float inputAmount = Input.GetAxis("Vertical");
		if (inputAmount == 0f)
			return;

		float movementAmount = inputAmount * acceleration * Time.deltaTime;
		Vector3 movement = new Vector3(0f, 0f, movementAmount);
		rigidBody.AddForce(movement);
	}

	private void Jump()
	{
		rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
	}
}
