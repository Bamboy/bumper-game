using UnityEngine;
using System.Collections;
using Excelsion.Characters;

public class Hazard : MonoBehaviour 
{

	void OnTriggerEnter2D( Collider2D c )
	{
		CharacterBehaviour unit = c.gameObject.GetComponent< CharacterBehaviour >();
		Debug.Log ("Entered pit!");
		if( unit != null )
		{
			Debug.Log ("Entered pit and has unit!");
			unit.OnPitEnter();
		}
	}

}
