using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace GenshinCalc_V2.Functions
{
	internal class CharacterInfo
	{
		string character = string.Empty;
		string[] characterdatajson = new string[2] { @"meta\character\{0}\data.json", @"meta\character\{0}\detail.json" };
		bool IsTraveler = false;
		//File characterdata=File.ReadAllBytes(characterdatajson[0]);
		public void SetCharacter(string charactername)
		{
			if (charactername.Contains("旅行者"))
			{
				//先不管旅行者的属性
				character = "空";
				IsTraveler = true;
			}
			else 
			{
				IsTraveler = false;
				character = charactername;
			}
		}


	}
}
