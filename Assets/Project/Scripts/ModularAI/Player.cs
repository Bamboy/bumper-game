using UnityEngine;
using System.Collections;

namespace Excelsion.ModularAI
{
	public class Player : EntityLivingBase 
	{
		
		public override void Start () 
		{
			base.Start();
			stats = new Stats( 1, 1, 2, 1, 3 ); //The default player stats.
		}

		public override void Update ()
		{
			base.Update();
			inputVelocity = new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );
		}

		public override void OnOutOfBounds()
		{
			transform.position = Vector3.zero;
			rigidbody2D.velocity = Vector2.zero;
		}
	}
}