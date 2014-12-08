using UnityEngine;
using System.Collections;

//Our base class for all characters
namespace Excelsion.Characters
{
	[RequireComponent(typeof(Rigidbody2D))]
	public abstract class CharacterBehaviour : MonoBehaviour 
	{
		protected float zSpeed = 0.0f;
		//protected virtual void Update()
		//{
		//	transform.Translate(0,0, zSpeed * Time.deltaTime);
		//}
		protected virtual void FixedUpdate () 
		{
			transform.Translate(0,0, zSpeed * Time.deltaTime);
		}
		
		
		//Called from a pit when a character enters it.
		public virtual void OnPitEnter()
		{
			zSpeed = 10.0f;
			//rigidbody2D.isKinematic = true;
			Kill();
		}
		public virtual void Kill()
		{
			OnKilled();
			Destroy(this.gameObject);
		}
		protected virtual void OnKilled()
		{
			//Do stuff you wanna do before dying.
			return;
		}

		//Called when we hit a wall.
		protected virtual void OnWallCollision( Collision2D col )
		{
			return;
		}

		protected virtual void OnCollisionEnter2D( Collision2D col )
		{
			CharacterBehaviour unit = col.gameObject.GetComponent< CharacterBehaviour >();
			if( unit != null )
			{
				this.OnHitCharacter( unit );
			}
			else
			{
				//Assume we hit a wall.
				this.OnWallCollision( col );
			}
		}
	
		//What we do when we make physical contact with another character
		protected virtual void OnHitCharacter( CharacterBehaviour unit )
		{
			unit.OnCharacterCollision( this as CharacterBehaviour );
		}

		//Called from the other character when they hit us.
		public virtual void OnCharacterCollision( CharacterBehaviour other )
		{
			return;
		}





















	}
}