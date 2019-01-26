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
		if (Input.GetAxis("Horizontal") != 0)
		{
			MoveDirection(Input.GetAxis("Horizontal"));
		}

		if (rigidBody.velocity.magnitude > maxSpeed)
		{
			rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
	}

	private void MoveDirection(float direction)
	{
		rigidBody.AddForce(Vector3.right * direction * acceleration);

		if (direction > 0)
		{
			bodyTransform.localEulerAngles = Vector3.zero;
		}
		else
		{
			bodyTransform.localEulerAngles = new Vector3(0, 180, 0);
		}
	}

	private void Jump()
	{
		rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
	}
}
