using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float acceleration;
	public float defaultMaxSpeed;
	private float maxSpeed;
	public float jumpPower;

	public Transform bodyTransform;
	private Quaternion defaultRotation;
	private Rigidbody rigidBody;

	// Start is called before the first frame update
	void Start()
	{
		defaultRotation = transform.rotation;
	}

	// Runs before Start()
	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
		maxSpeed = defaultMaxSpeed;
	}

	// Update is called once per frame
	void Update()
	{
		MovePlayerBasedOnInput();

		if (rigidBody.velocity.magnitude > maxSpeed)
		{
			rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
	}

	private void MovePlayerBasedOnInput()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		rigidBody.AddForce(movement * acceleration);
	}

	private void Jump()
	{
		rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
	}
}
