using UnityEngine;
using System.Collections;

namespace Excelsion.ModularAI
{
	[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
	public abstract class Entity : MonoBehaviour 
	{
		public Stats stats;
		//public bool onGround;

		//public bool isDead;
		//protected void Kill(){ this.SetDead(); }
		//public void SetDead(){ isDead = true; }
		//public bool isAlive(){ return !isDead; }
		private bool invulnerable;
		//public bool IsInvulnerable(){ return invulnerable; }

		protected Damage lastDamage;
		protected virtual void SetBeenAttacked(){  }
		public virtual bool AttackEntityFrom( Damage dmg )
		{
			if( invulnerable )
				return false;

			lastDamage = dmg;
			return true;
		}

		#region Physics
		public float recoveryTime;
		protected float defaultDrag;
		protected float defaultMass;
		protected Vector2 _lastVelocity;

		internal Vector2 inputVelocity; //Direction in which the player or the AI is trying to move.
		public Vector2 InputVelocity{ get{ return inputVelocity; } set{ inputVelocity = value; } }
		protected float inputControlScale; //Percentage that climbs back to 1 over time, after taking knockback.
		#region Knockback
		/* Knockback works like so:
		 * 
		 * Drag is reduced on collision and the unit is knocked back.
		 * Drag then 'recovers' over time, thus regaining control, and slowing us down.
		 * Input reduction works in the same manner as drag.
		 */
		
		private float kb_eventTime, kb_endTime;
		public virtual void Knockback( Vector2 direction, float baseRecoverTime, int POW ) //This is called when we are hit.
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
		
		private void KnockbackUpdate() //TODO - make stats.RES tie into this drag somehow
		{
			float curveValue = Main.instance.unitRecovery.Evaluate(VectorExtras.ReverseLerp( Time.time, kb_eventTime, kb_endTime ));
			rigidbody2D.drag = curveValue * defaultDrag;
			inputControlScale = Mathf.Clamp01(curveValue);
		}

		public virtual void FixedUpdate()
		{
			//rigidbody2D.AddForce( inputVelocity * inputControlScale * (1.5f + (float)stats.SPD) * rigidbody2D.mass, ForceMode2D.Force );
			//inputVelocity = Vector2.zero;
			if( rigidbody2D.velocity.magnitude > Main.maxVelocity )
			{
				Debug.Log("Velocity exceeded! "+ rigidbody2D.velocity.magnitude, this);
				rigidbody2D.velocity = rigidbody2D.velocity.normalized * Main.maxVelocity;
			}
		}
		#endregion Knockback

		public virtual void OnCollisionEnter2D( Collision2D col ) //TODO make this work through our Damage class
		{
			Debug.Log ("Collision!", this);
			Entity otherEnt = col.collider.GetComponent< Entity >();
			if( otherEnt != null )
			{
				if( Time.time < kb_endTime )
					return;
				Vector2 velocityChange = (rigidbody2D.velocity - _lastVelocity).normalized;//TODO - Test to make sure that the other unit isn't 'anchored'.
				otherEnt.Knockback( velocityChange, 3.0f, stats.POW );
				
				//Debug.Log("This Vel: "+ rigidbody2D.velocity + ", Last Vel: "+ _lastVelocity + ", Difference: "+ velocityChange +" By: "+ this.name);
				Debug.DrawRay( transform.position, new Vector3( velocityChange.x, velocityChange.y, 0.0f ), Color.cyan, 1.5f );
			}
		}

		#endregion Physics

		public virtual void Start()
		{
			stats = new Stats();
			defaultDrag = rigidbody2D.drag;
			defaultMass = rigidbody2D.mass;
		}

		public virtual void Update()
		{
			KnockbackUpdate();
			rigidbody2D.AddForce( inputVelocity * inputControlScale * (1.5f + (float)stats.SPD) * rigidbody2D.mass, ForceMode2D.Force );
			inputVelocity = Vector2.zero;
		}



		//Called by Level when this unit goes off of the playing field. (When the unit falls into a pit.)
		public virtual void OnOutOfBounds(){ return; }
	}
}