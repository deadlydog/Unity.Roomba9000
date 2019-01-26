using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
	public int score;
	public GameObject pickUp;

	public int numberOfPickUps;
	public Vector3 pickUpSpawnValues;

    // Start is called before the first frame update
    void Start()
    {
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
	void Update()
    {
        
    }
}
