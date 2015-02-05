using UnityEngine;
using System.Collections;
using Excelsion.Units;

[RequireComponent( typeof(Rigidbody2D), typeof(Collider2D) )]
public class Level : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerExit2D( Collider2D obj )
	{
		Unit outOfBoundsUnit = obj.GetComponent< Unit >();
		if( outOfBoundsUnit != null )
		{
			outOfBoundsUnit.OnOutOfBounds();

		}

	}
}
