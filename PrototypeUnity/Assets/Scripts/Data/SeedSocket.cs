namespace Assets.Scripts.Data
{
	public class SeedSocket
	{
		public int plantID = -1;
		public int plantGrowStage = 0;
		public int daysSinceBeenPlanted = 0;
		public int hpLost = 0;

		public void AddPlant(int ID){
			plantID = ID;
			plantGrowStage = 0;
			daysSinceBeenPlanted = 0;
			hpLost = 0;
		}

		public void Harvest(){
			GameManager.Run(CropsManager.GetCropType(plantID).cropStages[plantGrowStage].OnHarvest);
			plantID = -1;
		}

		public void NewDay(){
			if (plantID != -1) {
				daysSinceBeenPlanted++;
				CropType.CropStage cropStage = CropsManager.GetCropType(plantID).cropStages[plantGrowStage];
				if (daysSinceBeenPlanted >= cropStage.minDaysToGrow
					&& cropStage.growthCondition.Fulfill(GameManager.instance.gameData))
					{
						GameManager.Run(cropStage.OnEnd);
						plantGrowStage += 1;
					if (plantGrowStage >= CropsManager.GetCropType(plantID).cropStages.Length)
						plantID = -1;
					else
						GameManager.Run(CropsManager.GetCropType(plantID).cropStages[plantGrowStage].OnStart);
					}
				}
		}

		public void Damage()
		{
			hpLost++;
			if (hpLost >= CropsManager.GetCropType(plantID).cropStages[plantGrowStage].HP)
			{
				plantID = -1;
			}
		}

		public int GetHealth(){
			return CropsManager.GetCropType(plantID).cropStages[plantGrowStage].HP - hpLost;
		}
	}
}