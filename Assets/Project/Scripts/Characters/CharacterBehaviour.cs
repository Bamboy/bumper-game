using UnityEngine;
using System.Collections;

//Our base class for all characters
namespace Excelsion.Characters
{
	[RequireComponent(typeof(Rigidbody2D))]
	public abstract class CharacterBehaviour : MonoBehaviour 
	{
		private float stunTime = 0.0f;
		//Accessors
		public Vector2 Position
		{
			get{ return new Vector2( transform.position.x, transform.position.y ); }
			set{ 
				Vector3 newPos = new Vector3( value.x, value.y, transform.position.z );
				transform.position = newPos;
			}
		}
		public bool isStunned
		{ get{ return (stunTime > 0.0f); } }


		protected virtual void Update()
		{
			if( isStunned )
				stunTime -= Time.deltaTime;
		}

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
				this.OnHitCharacter( unit, col );
			}
			else
			{
				//Assume we hit a wall.
				this.OnWallCollision( col );
			}
		}
	
		//What we do when we make physical contact with another character
		protected virtual void OnHitCharacter( CharacterBehaviour unit, Collision2D col )
		{
			unit.Stun( 0.2f );
			unit.AddForce( col.relativeVelocity * 0.4f );
			
			unit.OnCharacterCollision( this as CharacterBehaviour );
		}

		//Called from the other character when they hit us.
		public virtual void OnCharacterCollision( CharacterBehaviour other )
		{
			return;
		}

		public virtual void Stun( float duration )
		{
			stunTime = duration;
		}
		public virtual void AddForce( Vector2 force )
		{
			rigidbody2D.AddForce( force, ForceMode2D.Impulse );
		}





















	}
}