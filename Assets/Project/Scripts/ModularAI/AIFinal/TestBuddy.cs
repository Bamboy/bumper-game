using UnityEngine;
using System.Collections;
using Excelsion.ModularAI.Tasks;
using Excelsion.ModularAI;

namespace Excelsion.AI
{
	public class TestBuddy : EntityCreature 
	{
		public override void Start () 
		{
			base.Start();

			EntityLiving el = this as EntityLiving;
			tasks = new EntityTaskManager();
			tasks.AddTask( 1, new TaskRunAroundLikeCrazy( el ));
			tasks.AddTask( 2, new TaskChasePlayer( el ));
		}

		public override void Update () 
		{
			base.Update();
		}

		public override void OnOutOfBounds()
		{
			transform.position = Vector3.zero;
			rigidbody2D.velocity = Vector2.zero;
		}
	}
}