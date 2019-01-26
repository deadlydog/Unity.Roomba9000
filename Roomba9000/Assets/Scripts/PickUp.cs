using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
	public int points;
	public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnDestroy()
	{
		//gameController.score += points;
	}

	//private void OnTriggerEnter(Collider other)
	//{
	//	Debug.Log(other.name);
	//	if (other.tag != "PlayerVacuum")
	//		return;

	//	//gameController.score += points;

	//	Destroy(gameObject);
	//}
}
