using UnityEngine;
using System.Collections;

namespace Excelsion.Characters
{
	public class Player : CharacterBehaviour 
	{
		private static Player plyInstance;
		public static Player Get()
		{ return plyInstance; }

		private Vector3 respawnPos;
		public bool inputEnabled;
		public float speed = 3.0f;

		void Awake () 
		{
			plyInstance = this;
			respawnPos = transform.position;
			inputEnabled = true;
		}


		protected override void FixedUpdate () 
		{
			base.FixedUpdate();

			//TODO - change the input to be in its own script!
			Vector2 moveVec = new Vector2( Input.GetAxis( "Horizontal" ) * speed, Input.GetAxis( "Vertical" ) * speed );
			if( inputEnabled == false )
				moveVec = Vector2.zero;
			rigidbody2D.AddForce( moveVec );
		}

		public override void Kill()
		{
			OnKilled();
		}
		protected override void OnKilled()
		{
			Debug.Log("Player was killed!");
			inputEnabled = false;
			StartCoroutine( "Respawn", 3.0f );
		}
		private IEnumerator Respawn( float time )
		{
			Debug.Log("Respawning...");
			yield return new WaitForSeconds( time );
			transform.position = respawnPos;
			inputEnabled = true;
			rigidbody2D.isKinematic = false;
			zSpeed = 0.0f;
			Debug.Log("Respawned!");
		}
	}
}