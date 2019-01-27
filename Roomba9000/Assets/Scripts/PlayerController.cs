using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float acceleration;
	public float maxMovementSpeed;
	public float rotationSpeed;
	public float jumpPower;

	private Rigidbody playerRigidbody;

	private Camera overheadCamera;
	private Camera firstPersonCamera;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Runs before Start()
	private void Awake()
	{
		playerRigidbody = GetComponent<Rigidbody>();

		overheadCamera = GameObject.Find("OverheadCamera").GetComponent<Camera>();
		firstPersonCamera = GameObject.Find("FirstPersonCamera").GetComponent<Camera>();

		overheadCamera.enabled = false;
		firstPersonCamera.enabled = true;
	}

	// Update is called once per frame
	void Update()
	{
		RotatePlayerBasedOnInput();
		MovePlayerBasedOnInput();
		JumpBasedOnInput();
		ChangeCameraBasedOnInput();
		EnsurePlayerIsNotMovingTooFast();
	}

	private void EnsurePlayerIsNotMovingTooFast()
	{
		if (playerRigidbody.velocity.magnitude > maxMovementSpeed)
		{
			playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxMovementSpeed;
		}
	}

	private void ChangeCameraBasedOnInput()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			overheadCamera.enabled = !overheadCamera.enabled;
			firstPersonCamera.enabled = !firstPersonCamera.enabled;
		}
	}

	private void RotatePlayerBasedOnInput()
	{
		// Get how much input was received.
		float inputAmount = Input.GetAxis("Horizontal");
		if (inputAmount == 0f)
			return;

		Vector3 rotationAmount = new Vector3(0, inputAmount, 0) * rotationSpeed * Time.deltaTime;
		playerRigidbody.transform.Rotate(rotationAmount);
	}

	private void MovePlayerBasedOnInput()
	{
		// Get how much input was received.
		float inputAmount = Input.GetAxis("Vertical");
		if (inputAmount == 0f)
			return;

		float movementAmount = inputAmount * acceleration * Time.deltaTime;
		playerRigidbody.AddRelativeForce(0, 0, movementAmount);
	}

	private void JumpBasedOnInput()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			playerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
		}
	}
}
