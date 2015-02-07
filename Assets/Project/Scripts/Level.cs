using UnityEngine;
using System.Collections;
using Excelsion.ModularAI;

[RequireComponent( typeof(Rigidbody2D), typeof(Collider2D) )]
public class Level : MonoBehaviour 
{
	public enum ResetType{
		Exit,
		Enter
	}
	public ResetType resetType = ResetType.Enter;

	void Start()
	{
		collider2D.isTrigger = true;
		rigidbody2D.isKinematic = true;
	}

	void OnTriggerExit2D( Collider2D obj )
	{
		if( resetType == ResetType.Exit )
		{
			Entity outOfBoundsUnit = obj.GetComponent< Entity >();
			if( outOfBoundsUnit != null )
			{
				outOfBoundsUnit.OnOutOfBounds();

			}
		}

	}
	void OnTriggerEnter2D( Collider2D obj )
	{
		if( resetType == ResetType.Enter )
		{
			Entity outOfBoundsUnit = obj.GetComponent< Entity >();
			if( outOfBoundsUnit != null )
			{
				outOfBoundsUnit.OnOutOfBounds();
				
			}
		}

	}
}
