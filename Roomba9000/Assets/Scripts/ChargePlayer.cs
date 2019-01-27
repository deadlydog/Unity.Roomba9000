using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePlayer : MonoBehaviour
{
    const float CHARGING_RATE = 20;

    private GameController gameController;

    private bool charging = false;

    // Start is called before the first frame update
    void Start()
    {
		gameController = GameObject.FindWithTag("GameController")?.GetComponent<GameController>();
        if (gameController == null)
		{
			Debug.Log("gameController in CollectPickUps.cs is null.");
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (charging && gameController.GetEnergy() < 100) {
            gameController.UpdateEnergy(Time.deltaTime * CHARGING_RATE);
        }
    }

    private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "ChargingStation")
		{
			charging = true;
		}
	}
    

    private void OnTriggerExit(Collider other)
	{
		if (other.tag == "ChargingStation")
		{
			charging = false;
		}
	}
}
