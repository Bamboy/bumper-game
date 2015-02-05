using UnityEngine;
using System.Collections;
using Excelsion.Units;
public class Main : MonoBehaviour 
{
	public static Main instance;
	public static GameObject player;
	public static float maxVelocity = 10.0f;
	public AnimationCurve unitRecovery;
	void Awake()
	{
		instance = this;
		player = GameObject.FindGameObjectWithTag("Player");
		Unit.recoveryCurve = unitRecovery;
	}

	//To support non-monobehaviour coroutines being called:
	public void StartChildCoroutine(IEnumerator coroutineMethod)
	{
		StartCoroutine(coroutineMethod);
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
