using UnityEngine;
using System.Collections;
using Excelsion.Units;
public class Main : MonoBehaviour 
{
	public static Main instance;
	public static GameObject player;
	public AnimationCurve unitRecovery;
	void Awake()
	{
		instance = this;
		player = GameObject.FindGameObjectWithTag("Player");
		Unit.recoveryCurve = unitRecovery;
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
