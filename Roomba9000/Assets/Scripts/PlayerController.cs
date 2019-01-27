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
	private Camera orbitCamera;

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
		orbitCamera = GameObject.Find("OrbitCamera").GetComponent<Camera>();

		overheadCamera.enabled = true;
		firstPersonCamera.enabled = false;
		orbitCamera.enabled = false;
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
		// Camera mode order is Overhead -> First Person -> Orbit -> Overhead.
		if (Input.GetKeyDown(KeyCode.C))
		{
			if (overheadCamera.enabled)
			{
				firstPersonCamera.enabled = true;
				overheadCamera.enabled = false;
			}
			else if (firstPersonCamera.enabled)
			{
				orbitCamera.enabled = true;
				firstPersonCamera.enabled = false;
			}
			else
			{
				overheadCamera.enabled = true;
				orbitCamera.enabled = false;
			}
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
