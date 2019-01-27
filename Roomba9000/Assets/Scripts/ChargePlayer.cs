using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePlayer : MonoBehaviour
{
    const float CHARGING_RATE = 1;

    public GameController gameController;

    private bool charging = false;

    // Start is called before the first frame update
    void Start()
    {
        if (gameController == null)
		{
			Debug.Log("gameController in CollectPickUps.cs is null.");
		}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (charging && gameController.energy < 100) {
            gameController.energy += Time.deltaTime * CHARGING_RATE;
        }
    }

    private void OnTriggerEnter(Collider other)
	{
        charging = other.tag == "ChargingStation";
	}
    

    private void OnTriggerExit(Collider other)
	{
        charging = false;
	}
}
