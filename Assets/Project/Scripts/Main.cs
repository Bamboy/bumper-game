using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour 
{
	public static Main instance;
	void Start()
	{
		instance = this;
	}

	public void Win()
	{
		Debug.Log("You win!");
	}

	public void Lose()
	{
		Debug.Log("You lose!");
	}
}
