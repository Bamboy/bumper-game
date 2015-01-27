using UnityEngine;
using System.Collections;

namespace Excelsion.Unit
{
	[RequireComponent( typeof(Collider2D), typeof(Rigidbody2D) )]
	public abstract class Unit : MonoBehaviour 
	{
		public Stats stats;
		public AnimationCurve recoveryCurve;
		public float recoveryTime;
		public bool inControl { get{ return recoveryTime <= 0.0f; } }
		public Vector2 inputVelocity;
		public Vector2 velocity;


		public virtual void Start () 
		{
			stats = new Stats();
		}
		//Called by Level when this unit goes off of the playing field. (When the unit falls into a pit.)
		public virtual void OnOutOfBounds()
		{
			Destroy( this.gameObject );
		}

		#region Knockback
		/* Knockback works like so:
		 * 
		 * Set drag to high value.
		 * Apply a high amount of force to the Unit.
		 * Do NOT disable user input. Drag will make the user's inputs less effective.
		 * Have drag lower itself over time.

		OR
		Movement works like so:
		Drag gets set to high value when no inputs are being pressed
		Knockback sets drag to low value
*/

		public void Knockback( Vector2 direction, float power, float baseRecoverTime )
		{
			recoveryTime = baseRecoverTime / ((float)stats.RES + 0.5f);
			kb_direction = direction.normalized;
			kb_powMult = power;
			kb_eventTime = Time.time;
			kb_endTime = Time.time + recoveryTime;
			rigidbody2D.AddForce( -direction * 2.0f, ForceMode2D.Impulse );
		}
		public Vector2 kb_direction;
		public float kb_powMult;
		public float kb_eventTime;
		public float kb_endTime;

		public virtual void KnockbackUpdate()
		{
			rigidbody2D.drag = ((float)stats.RES / 4.0f) + recoveryCurve.Evaluate(VectorExtras.ReverseLerp( Time.time, kb_eventTime, kb_endTime ));
		}


		#endregion


		public virtual void Update()
		{
			KnockbackUpdate();
			rigidbody2D.AddForce( inputVelocity * 2.5f, ForceMode2D.Force );
		}


		public virtual void OnCollisionEnter2D( Collision2D col )
		{
			Debug.Log ("Collision");
			Unit otherUnit = col.collider.GetComponent< Unit >();
			if( otherUnit != null )
			{
				//TODO - Test to make sure that the other unit isnt 'anchored'.
				otherUnit.Knockback( col.relativeVelocity.normalized, col.relativeVelocity.magnitude, 2.5f );
			}
			else
			{
				this.Knockback( -velocity.normalized, velocity.magnitude, 1.25f * velocity.magnitude );
			}
		}






























		/* OLD Non-physical method
		#region Knockback
		public AnimationCurve recoveryCurve;
		public float recoveryTime;
		public bool inControl
		{ get{ return recoveryTime <= 0.0f; } }
		public void Knockback( Vector2 direction, float power, float baseRecoverTime )
		{
			recoveryTime = baseRecoverTime / ((float)stats.RES + 0.5f);
			kb_direction = direction.normalized;
			kb_powMult = power;
			kb_eventTime = Time.time;
			kb_endTime = Time.time + recoveryTime;
		}
		public Vector2 kb_direction;
		public float kb_powMult;
		public float kb_eventTime;
		public float kb_endTime;
		private void KnockbackUpdate()
		{
			recoveryTime -= Time.deltaTime;

			if( inControl == false )
			{
				//Returns 0 - 1 based on when the kockback happened and the duration of the knockback.
				//VectorExtras.ReverseLerp( Time.time, kb_eventTime, kb_endTime );
				float durationMult = kb_powMult * recoveryCurve.Evaluate( VectorExtras.ReverseLerp( Time.time, kb_eventTime, kb_endTime ) );
				velocity = kb_direction * durationMult;
			}
		}
		#endregion

		#region Movement
		public Vector2 velocity;

		public virtual void MoveUpdate()
		{
			transform.Translate( velocity.x / 30.0f, velocity.y / 30.0f, 0.0f );
		}
		#endregion

		
		// Update is called once per frame
		public virtual void Update () 
		{
			KnockbackUpdate();
			MoveUpdate();

		}





		*/


























	}
}