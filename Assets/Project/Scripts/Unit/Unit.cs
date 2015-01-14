using UnityEngine;
using System.Collections;

namespace Excelsion.Unit
{
	[RequireComponent( typeof(Collider2D), typeof(Rigidbody2D) )]
	public abstract class Unit : MonoBehaviour 
	{
		public Stats stats;

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

		// Use this for initialization
		public virtual void Start () 
		{
			stats = new Stats();
		}
		
		// Update is called once per frame
		public virtual void Update () 
		{
			KnockbackUpdate();
			MoveUpdate();

		}

		//Called by Level when this unit goes off of the playing field. (When the unit falls into a pit.)
		public virtual void OnOutOfBounds()
		{
			Destroy( this.gameObject );
		}

		public virtual void OnCollisionEnter2D( Collision2D col )
		{
			Debug.Log ("Collision");
			Unit otherUnit = col.collider.GetComponent< Unit >();
			if( otherUnit != null )
			{
				otherUnit.Knockback( col.relativeVelocity.normalized, col.relativeVelocity.magnitude, 2.5f );
			}
			else
			{
				this.Knockback( -velocity.normalized, velocity.magnitude, 1.25f * velocity.magnitude );
			}
		}


























	}
}