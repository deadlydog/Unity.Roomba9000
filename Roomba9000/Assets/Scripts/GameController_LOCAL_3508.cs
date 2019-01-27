﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

	public float powerConsumptionRate = 0.1f;

	public int score;
	public float energy = 100;
	public GameObject pickUp;

	public int numberOfPickUps;
	public Vector3 pickUpSpawnValues;

	const int NO_POWER = 0;
	const int TOO_DIRTY = 0;

    // Start is called before the first frame update
    void Start()
    {
		if (pickUp == null)
		{
			Debug.Log("pickUp is null in GameController.cs.");
		}

		SpawnPickUps();
    }

	private void SpawnPickUps()
	{
		for (int i = 0; i < numberOfPickUps; i++)
		{
			var position = new Vector3(Random.Range(-pickUpSpawnValues.x, pickUpSpawnValues.x), pickUpSpawnValues.y, Random.Range(-pickUpSpawnValues.z, pickUpSpawnValues.z));
			var rotation = Quaternion.identity;
			Instantiate(pickUp, position, rotation);
		}
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		energy = energy - (Time.deltaTime * powerConsumptionRate);

		if (energy < 0) {
			// TODO end game condition
			endGame(0);
		}

		if (FindObjectsOfType<PickUp>() > 500) {
			endGame(1);
		}
    }

	private Void endGame(int reason) {
		String review = ""
		if (reason == NO_POWER) {
			review = GenerateReviewNoPower();
		} else if (reason == TOO_DIRTY){
			review = GenerateReviewTooDirty();
		}
		Debug.Log(review);
	}

	private String GenerateReviewNoPower() {
		if (score > 9000) {
			return "I think this thing is a perpetual motion generator.";
		} else if (score > 6000) {
			return "Battery life is kind of crap...";
		}
		return "I don't think it ever turned on...";
	}

	private String GenerateReviewTooDirty() {
		if (score > 9000) {
			return "It worked well for the first century.";
		} else if (score > 6000) {
			return "It might have worked at one time...";
		}
		return "My house is dirty because of this thing...";
	}
}