using UnityEngine;
using System.Collections;

namespace Excelsion.Unit
{
	public class Player : UnitDamagable
	{
		public static Player instance;
		#region Skills
		//Our player has some additional stats we wish to track.
		static int _lives;
		static int _skill;
		public static int LIVES //This number is reduced by one every time we die.
		{
			get{ return _lives; } set{ _lives = Mathf.Max( 0, value ); }
		}
		public static int SKILL //Every wave, stat point(s) are given. Every two points of this adds an additional stat point.
		{
			get{ return _skill; } set{ _skill = Mathf.Max( 0, value ); }
		}
		#endregion


		public override void Start () 
		{
			stats = new Stats( 1, 1, 2, 1, 3 ); //The default player stats.
			LIVES = 3;
			SKILL = 0;
			instance = this;
			defaultDrag = rigidbody2D.drag;
			Health = stats.VIT;
		}

		public override void Update ()
		{
			base.Update();


			inputVelocity = new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );
		}

		public override void OnOutOfBounds()
		{
			/*if( (LIVES - 1) <= 0 )
			{
				Main.instance.Lose ();
			}
			else
			{
				Respawn();
			} */

			Respawn();
		}
		public void Respawn()
		{
			transform.position = new Vector3(0,0,0);
			LIVES -= 1;
			Health = stats.VIT;
		}



























	}
}