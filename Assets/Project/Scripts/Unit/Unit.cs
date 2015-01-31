using UnityEngine;
using System.Collections;

namespace Excelsion.Unit
{
	[RequireComponent( typeof(Collider2D), typeof(Rigidbody2D) )]
	public abstract class Unit : MonoBehaviour 
	{
		public Stats stats;
		public static AnimationCurve recoveryCurve;
		public float recoveryTime;
		public Vector2 inputVelocity;
		public float inputControlScale;

		internal float defaultDrag;
		private Vector2 _lastVelocity;

		//public bool inFullControl { get{ return recoveryTime <= 0.0f; } }

		public virtual void Start () 
		{
			stats = new Stats();
			defaultDrag = rigidbody2D.drag;
		}
		//Called by Level when this unit goes off of the playing field. (When the unit falls into a pit.)
		public virtual void OnOutOfBounds()
		{
			Destroy( this.gameObject );
		}

		#region Knockback
		/* Knockback works like so:
		 * 
		 * Drag is reduced on collision and the unit is knocked back.
		 * Drag then 'recovers' over time, thus regaining control, and slowing us down.
		 * Input reduction works in the same manner as drag.
		 */

		private float kb_eventTime, kb_endTime;
		public void Knockback( Vector2 direction, float baseRecoverTime, int POW ) //This is called when we are hit.
		{ //TODO - Force needs to be considered with the recovery time

			//recoveryTime = baseRecoverTime / ((float)stats.RES + 0.5f); //baserecovertime, resistance, power
			recoveryTime = ( (((float)POW / 1.25f) / ((float)stats.RES + 0.5f)) / baseRecoverTime ) + 0.5f; //TODO - this formula needs to be played with.

			kb_eventTime = Time.time;
			kb_endTime = Time.time + recoveryTime;
			StartCoroutine( "KB_Push", direction );
		}
		//We yield because we need to make sure that drag was reduced. Otherwise our knockback will be less effective.
		private IEnumerator KB_Push( Vector2 force ) 
		{ yield return new WaitForFixedUpdate(); rigidbody2D.AddForce( force * 2.5f, ForceMode2D.Impulse ); }

		public virtual void KnockbackUpdate() //TODO - make stats.RES tie into this drag somehow
		{
			float curveValue = recoveryCurve.Evaluate(VectorExtras.ReverseLerp( Time.time, kb_eventTime, kb_endTime ));
			rigidbody2D.drag = curveValue * defaultDrag;
			inputControlScale = Mathf.Clamp01(curveValue);
		}

		#endregion

		public virtual void Update()
		{
			KnockbackUpdate();
			rigidbody2D.AddForce( inputVelocity * inputControlScale * (1.5f + (float)stats.SPD) * rigidbody2D.mass, ForceMode2D.Force );
		}
		public void LateUpdate()
		{
			_lastVelocity = rigidbody2D.velocity;
		}


		public virtual void OnCollisionEnter2D( Collision2D col )
		{
			Debug.Log ("Collision!", this);
			Unit otherUnit = col.collider.GetComponent< Unit >();
			if( otherUnit != null )
			{
				//TODO - Test to make sure that the other unit isn't 'anchored'.
				Vector2 velocityChange = (rigidbody2D.velocity - _lastVelocity).normalized;
				otherUnit.Knockback( velocityChange, 3.0f, stats.POW );

				//Debug.Log("This Vel: "+ rigidbody2D.velocity + ", Last Vel: "+ _lastVelocity + ", Difference: "+ velocityChange +" By: "+ this.name);
				Debug.DrawRay( transform.position, new Vector3( velocityChange.x, velocityChange.y, 0.0f ), Color.cyan, 1.5f );
			}
		}





















































	}
}