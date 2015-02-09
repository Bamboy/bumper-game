using UnityEngine;
using System.Collections;
using Excelsion.ModularAI.Tasks;
using Excelsion.ModularAI;

namespace Excelsion.AI
{
	public class TestIdler : EntityCreature 
	{
		public override void Start () 
		{
			base.Start();
			EntityLiving el = this as EntityLiving;

			senses = new EntitySenses( el, 3.0f );

			tasks = new EntityTaskManager();
			//tasks.AddTask( 1, new TaskRunAroundLikeCrazy( el ));
			//tasks.AddTask( 2, new TaskChasePlayer( el ));
			targeting = new EntityTaskManager();



			AddType( "Passive" );
			AddType( "TestIdler" );
		}
		
		public override void Update () 
		{
			base.Update();
		}
		
		public override void OnOutOfBounds()
		{
			GameObject.Destroy( this.gameObject );
		}
	}
}