using UnityEngine;
using System.Collections;

namespace Excelsion.Characters
{
	public class BasicEnemy : CharacterBehaviour 
	{
		public float speed = 3.0f;

		void Start () 
		{
		
		}


		protected override void FixedUpdate () 
		{
			base.FixedUpdate();

			Vector2 moveVec;

			if( isStunned == true || rigidbody2D.velocity.magnitude > speed * 2.1f )
			{


				moveVec = -rigidbody2D.velocity.normalized * 2.5f; //Attempt to slow down!
			}
			else
			{
				moveVec = VectorExtras.Direction( this.Position, Player.Get().Position );
			}
			rigidbody2D.AddForce( moveVec * speed );
		}
	}
}