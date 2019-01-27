using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPickUps : MonoBehaviour
{
	private GameController gameController;
	private AudioSource audioData;

	private void Awake()
	{
		audioData = GetComponent<AudioSource>();
	}

	// Start is called before the first frame update
	void Start()
    {
		gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		if (gameController == null)
		{
			Debug.Log("gameController in CollectPickUps.cs is null.");
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.name);
		if (other.tag != "PickUp")
			return;

		var pickUp = other.GetComponent<PickUp>();
		if (pickUp != null)
		{
			gameController.UpdateScore(pickUp.points);
		}
		else
		{
			Debug.Log("Could not find the PickUp object from the given collider in CollectPickUps.");
		}

		Destroy(other.gameObject);

		audioData.Play();
	}
}
