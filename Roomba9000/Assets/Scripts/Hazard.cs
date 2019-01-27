using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
	public int energyDrain;
	private float rotationSpeed = 100.0f;

	private void Update()
	{
		float rotateAmount = rotationSpeed * Time.deltaTime;
		Vector3 rotation = new Vector3(0, rotateAmount, 0);
		transform.Rotate(rotation);
	}
}
