using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPickUps : MonoBehaviour
{
	public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
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
			gameController.score += pickUp.points;
		}
		else
		{
			Debug.Log("Could not find the PickUp object from the given collider in CollectPickUps.");
		}

		Destroy(other.gameObject);
	}
}
