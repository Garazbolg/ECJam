using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Data
{
	[Serializable]
	public class CropType
	{
		[Serializable]
		public struct CropStage
		{
			public int minDaysToGrow;
			public ConditionScript growthCondition;
			public Script OnStart;
			public Script OnEnd;
			public Script OnHarvest;
			public Script OnWerewolfContact;
			public int HP;
		}

		public string name;

		public CropStage[] cropStages;
		public UnityEngine.GameObject viewPrefab;
	}
}
