using UnityEngine;
using System.Collections;
using Excelsion.Unit;
public class Main : MonoBehaviour 
{
	public static Main instance;
	public AnimationCurve unitRecovery;
	void Awake()
	{
		instance = this;
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
