using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicked : MonoBehaviour
{
	public Button yourButton;
	public SpawnableManager spawnManage;

	public GameObject thisPoster;

	void Start()
	{
		yourButton = GetComponent<Button>();

		yourButton.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		spawnManage.selectPoster(thisPoster);
	}
}
