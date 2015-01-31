using UnityEngine;
using System.Collections;

namespace Excelsion.Unit
{
	[System.Serializable]
	public class Stats : System.Object
	{
		public int _pow = 1;
		public int _dmg = 1;
		public int _res = 1;
		public int _spd = 1;
		public int _vit = 1;

		public Stats() { }
		public Stats( int power, int damage, int resistance, int speed, int vitality )
		{
			POW = Mathf.Max( 1, power );
			DMG = Mathf.Max( 1, damage );
			RES = Mathf.Max( 1, resistance );
			SPD = Mathf.Max( 1, speed );
			VIT = Mathf.Max( 1, vitality );
		}

		public int POW // How forceful attacks are.
		{
			get{ return _pow; }
			set{ _pow = Mathf.Max( 1, value ); }
		}
		public int DMG // How damaging attacks are.
		{
			get{ return _dmg; }
			set{ _dmg = Mathf.Max( 1, value ); }
		}
		public int RES // How quickly the unit is able to recover from being pushed.
		{
			get{ return _res; }
			set{ _res = Mathf.Max( 1, value ); }
		}
		public int SPD //How fast we move.
		{
			get{ return _spd; }
			set{ _spd = Mathf.Max( 1, value ); }
		}
		public int VIT // How much health the unit has.
		{
			get{ return _vit; }
			set{ _vit = Mathf.Max( 1, value ); }
		}
	}
}
