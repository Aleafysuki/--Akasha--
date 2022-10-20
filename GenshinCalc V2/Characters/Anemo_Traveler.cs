using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinCalc_V2.Characters
{
	internal class Anemo_Traveler
	{
		float[] Attack_DMG = new float[6];
		float[] Skill_DMG = new float[6];
		float[] Burst_DMG = new float[6];
		Calculator character = new Calculator();
		public void Calc()
		{
			//调用计算项目的示例
			character.Calculate(MainWindow.res, "ATK*CRITRATE*CRITDMG*1", Calculator.CalculateMode.Crited);
		}
	}
}
