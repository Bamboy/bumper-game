using UnityEngine;
using System.Collections;

namespace Excelsion.ModularAI
{
	public abstract class EntityLivingBase : Entity
	{
		
		public override void Start()
		{
			base.Start();
		}
		public override void Update()
		{
			base.Update();
		}

		public virtual void OnDeath()
		{

		}





	}
}