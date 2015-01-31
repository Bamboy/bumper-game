using UnityEngine;
using System.Collections;

namespace Excelsion.Unit
{
	public abstract class UnitDamagable : Unit
	{
		int hp;
		public int Health
		{
			get{ return hp; }
			set{ hp = value; EvaluateHealth(); }
		}
		public virtual void OnDamage( int val ) { Health -= val; }

		internal virtual void EvaluateHealth()
		{
			hp = Mathf.Clamp( hp, 0, 100 );
			if( hp == 0 )
				Kill();
		}
		public virtual void Kill()
		{
			Destroy( this.gameObject );
		}
	}
}
