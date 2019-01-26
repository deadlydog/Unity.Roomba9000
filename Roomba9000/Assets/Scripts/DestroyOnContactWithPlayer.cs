using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContactWithPlayer : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.name);
		if (other.tag != "PlayerVacuum")
			return;

		Destroy(gameObject);
	}
}
