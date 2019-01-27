using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
	private Text reviewText;

	private void Awake()
	{
		reviewText = GameObject.Find("ReviewText").GetComponent<Text>();
		reviewText.text = CrossSceneInformation.GameOverReview;
	}

	// Update is called once per frame
	void Update()
    {
		GetInputAndGoBackToTitleScreenIfNecessary();
	}

	private void GetInputAndGoBackToTitleScreenIfNecessary()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("TitleScreen");
		}
	}
}
