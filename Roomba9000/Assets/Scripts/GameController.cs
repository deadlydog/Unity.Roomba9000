﻿using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
	private float powerConsumptionRate = 1.0f;

	private int score = 0;
	private float energy = 100;
	public GameObject pickUp;
	public GameObject hazard;

	private int numberOfInitialPickUps = 20;
	private float secondsBetweenPickUpSpawns = 0.5f;
	public Vector3 pickUpSpawnValues;

	private int numberOfInitialHazards = 5;

	private float secondsBetweenHazardSpawns = 1.0f;
	private Vector3 hazardSpawnValues = new Vector3(World.MAX_SIZE * World.TILE_SIZE, 1, World.MAX_SIZE * World.TILE_SIZE);
	
	private Text scoreText;
	private Text energyText;

	// Runs before Start()
	private void Awake()
	{
		if (pickUp == null)
		{
			Debug.Log("pickUp is null in GameController.cs.");
		}

		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		energyText = GameObject.Find("EnergyText").GetComponent<Text>();
	}

	// Start is called before the first frame update
	void Start()
    {
		Debug.Log(World.MAX_SIZE * World.TILE_SIZE);
		DrawScore();
		DrawEnergy();

		SpawnInitialPickUps();
		StartCoroutine(SpawnPickUpsContinually());

		SpawnInitialHazards();
		StartCoroutine(SpawnHazardsContinually());
    }

	private void SpawnInitialPickUps()
	{
		for (int i = 0; i < numberOfInitialPickUps; i++)
		{
			CreatePickUp();
		}
	}

	private void CreatePickUp()
	{
		var position = new Vector3(Random.Range(-pickUpSpawnValues.x, pickUpSpawnValues.x), pickUpSpawnValues.y, Random.Range(-pickUpSpawnValues.z, pickUpSpawnValues.z));
		var rotation = Quaternion.identity;
		Instantiate(pickUp, position, rotation);
	}

	private IEnumerator SpawnPickUpsContinually()
	{
		while (true)
		{
			CreatePickUp();
			yield return new WaitForSeconds(secondsBetweenPickUpSpawns);
		}
	}

	private void SpawnInitialHazards()
	{
		for (int i = 0; i < numberOfInitialHazards; i++)
		{
			CreateHazard();
		}
	}

	private void CreateHazard()
	{
		var position = new Vector3(Random.Range(-hazardSpawnValues.x, hazardSpawnValues.x), hazardSpawnValues.y, Random.Range(-hazardSpawnValues.z, hazardSpawnValues.z));
		var rotation = Quaternion.identity;
		Instantiate(hazard, position, rotation);
	}

	private IEnumerator SpawnHazardsContinually()
	{
		while (true)
		{
			CreateHazard();
			yield return new WaitForSeconds(secondsBetweenHazardSpawns);
		}
	}

	// Update is called once per frame
	void Update()
    {
		GetInputAndGoBackToTitleScreenIfNecessary();

		var energyUsed = (Time.deltaTime * powerConsumptionRate);
		UpdateEnergy(-energyUsed);

		if (energy < 0)
		{
			CrossSceneInformation.GameOverReason = GameOverReason.NoPower;
			CrossSceneInformation.GameOverReview = GenerateReviewNoPower();
			EndGame();
		}

		if (FindObjectsOfType<PickUp>().Length > 500)
		{
			CrossSceneInformation.GameOverReason = GameOverReason.TooDirty;
			CrossSceneInformation.GameOverReview = GenerateReviewTooDirty();
			EndGame();
		}
    }

	private void GetInputAndGoBackToTitleScreenIfNecessary()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("TitleScreen");
		}
	}

	public void UpdateScore(int points)
	{
		score += points;
		DrawScore();
	}

	private void DrawScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void UpdateEnergy(float delta)
	{
		energy += delta;
		DrawEnergy();
	}

	private void DrawEnergy()
	{
		energyText.text = "Energy: " + energy.ToString("N0");
	}

	public float GetEnergy()
	{
		return energy;
	}

	private void EndGame()
	{
		SceneManager.LoadScene("EndGameScene");
	}

	private string GenerateReviewNoPower() {
		if (score > 100) {
			return "I use to think it was a perpetual motion generator. Until the battery died.";
		} else if (score > 50) {
			return "Battery life is kind of crap...";
		}
		return "I don't think it ever turned on...";
	}

	private string GenerateReviewTooDirty()
	{
		if (score > 100)
		{
			return "It worked well for the first century.";
		}
		else if (score > 50)
		{
			return "It might have worked at one time...";
		}
		return "My house is dirty because of this thing...";
	}
}
