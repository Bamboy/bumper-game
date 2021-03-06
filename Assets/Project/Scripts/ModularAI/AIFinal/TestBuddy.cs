﻿using UnityEngine;
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

			senses = new EntitySenses( el, 3.0f );

			tasks = new EntityTaskManager();
			tasks.AddTask( 1, new TaskRunAroundLikeCrazy( el ));
			tasks.AddTask( 2, new TaskChaseTarget( el ));

			targeting = new EntityTaskManager();
			targeting.AddTask( 1, new TaskAITargetNearest(el, true, false, "Player", 50));



			AddType( "Hostile" );
			AddType( "TestBuddy" );
		}

		public override void Update () 
		{
			base.Update();
			//Debug.Log("Target: "+ GetTarget().name +", Entity Count: "+ Entity.allEntities.Count );
		}

		public override void OnOutOfBounds()
		{
			transform.position = Vector3.zero;
			rigidbody2D.velocity = Vector2.zero;
		}
	}
}