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

			tasks = new EntityTaskManager();
			tasks.AddTask( 2, new TaskRunAroundLikeCrazy( this.gameObject ));
			tasks.AddTask( 1, new TaskChasePlayer( this.gameObject ));
		}

		public override void Update () 
		{
			base.Update();
		}
	}
}