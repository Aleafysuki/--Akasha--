using System;
using System.Windows;
namespace GenshinCalc_V2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static float[] res = new float[13];
		public MainWindow()
		{
			InitializeComponent();
			OriginalAvalibilitySettings();
		}
		/// <summary>
		/// 初始设定
		/// </summary>
		public void OriginalAvalibilitySettings()
		{

			HP.AttributeNameLabel.Content = "生命值";
			HP.SetUnavaliableItems("WeaponBasic");

			ATK.AttributeNameLabel.Content = "攻击力";

			DEF.AttributeNameLabel.Content = "防御力";
			DEF.SetUnavaliableItems("WeaponBasic");

			EM.AttributeNameLabel.Content = "元素精通";
			EM.SetUnavaliableItems("CharacterBasic,WeaponBasic");

			CRITRATE.AttributeNameLabel.Content = "暴击率";
			CRITRATE.IsPercentage = true;
			CRITRATE.SetUnavaliableItems("WeaponBasic");
			CRITRATE.SetLockedItems("CharacterBasic", "5%");

			CRITDMG.AttributeNameLabel.Content = "暴击伤害";
			CRITDMG.IsPercentage = true;
			CRITDMG.SetUnavaliableItems("WeaponBasic");
			CRITDMG.SetLockedItems("CharacterBasic", "50%");

			RECHARGE.AttributeNameLabel.Content = "元素充能效率";
			RECHARGE.IsPercentage = true;
			RECHARGE.SetUnavaliableItems("WeaponBasic");
			RECHARGE.SetLockedItems("CharacterBasic", "100%");

			ELEMBONUS.AttributeNameLabel.Content = "元素伤害加成";
			ELEMBONUS.IsPercentage = true;
			ELEMBONUS.SetUnavaliableItems("CharacterBasic,WeaponBasic");

			PHYSBONUS.AttributeNameLabel.Content = "物理伤害加成";
			PHYSBONUS.IsPercentage = true;
			PHYSBONUS.SetUnavaliableItems("CharacterBasic,WeaponBasic");

			UNIVERSALBONUS.AttributeNameLabel.Content = "全伤害加成";
			UNIVERSALBONUS.IsPercentage = true;
			UNIVERSALBONUS.SetUnavaliableItems("CharacterBasic,WeaponBasic");

			HEALINGBONUS.AttributeNameLabel.Content = "治疗加成";
			HEALINGBONUS.IsPercentage = true;
			HEALINGBONUS.SetUnavaliableItems("CharacterBasic,WeaponBasic");
			//DMGBONUS.SetLockedItems("CharacterBasic", "100%");

			//EnableItem("ATK,HP", "CharacterAscention,WeaponBasic");
			//DisableItem("ATK", "ElementalResonance");
		}
		public void DisableItem(string s, string t)
		{
			if (s.ToLower().Contains("all"))
				s = "HP,ATK,DEF,EM,CRITRATE,CRITDMG,RECHARGE,ELEMBONUS,PHYSBONUS,UNIVERSALBONUS";
			else s.ToUpper();
			string[] ss = s.Split('\u002C');
			foreach (string row in ss)
			{
				switch (row)
				{
					case "HP": HP.SetEnabledItems(t, false); break;
					case "ATK": ATK.SetEnabledItems(t, false); break;
					case "DEF": DEF.SetEnabledItems(t, false); break;
					case "EM": EM.SetEnabledItems(t, false); break;
					case "CRITRATE": CRITRATE.SetEnabledItems(t, false); break;
					case "CRITDMG": CRITDMG.SetEnabledItems(t, false); break;
					case "RECHARGE": RECHARGE.SetEnabledItems(t, false); break;
					case "ELEMBONUS": ELEMBONUS.SetEnabledItems(t, false); break;
					case "PHYSBONUS": PHYSBONUS.SetEnabledItems(t, false); break;
					case "UNIVERSALBONUS": UNIVERSALBONUS.SetEnabledItems(t, false); break;
					case "HEALINGBONUS": UNIVERSALBONUS.SetEnabledItems(t, false); break;
					default:; break;
				}
			}
		}
		public void EnableItem(string s, string t)
		{
			if (s.ToLower().Contains("all"))
				s = "HP,ATK,DEF,EM,CRITRATE,CRITDMG,RECHARGE,ELEMBONUS,PHYSBONUS,UNIVERSALBONUS,HEALINGBONUS";
			else s.ToUpper();
			string[] ss = s.Split('\u002C');
			foreach (string row in ss)
			{
				switch (row)
				{
					case "HP": HP.SetEnabledItems(t, true); break;
					case "ATK": ATK.SetEnabledItems(t, true); break;
					case "DEF": DEF.SetEnabledItems(t, true); break;
					case "EM": EM.SetEnabledItems(t, true); break;
					case "CRITRATE": CRITRATE.SetEnabledItems(t, true); break;
					case "CRITDMG": CRITDMG.SetEnabledItems(t, true); break;
					case "RECHARGE": RECHARGE.SetEnabledItems(t, true); break;
					case "ELEMBONUS": ELEMBONUS.SetEnabledItems(t, true); break;
					case "PHYSBONUS": PHYSBONUS.SetEnabledItems(t, true); break;
					case "UNIVERSALBONUS": UNIVERSALBONUS.SetEnabledItems(t, true); break;
					case "HEALINGBONUS": UNIVERSALBONUS.SetEnabledItems(t, true); break;
					default:; break;
				}
			}
		}
		//测试用
		public async void CallCalculator()
		{
			res = new float[] { HP.Val, ATK.Val, DEF.Val, EM.Val, CRITRATE.Val, CRITDMG.Val, RECHARGE.Val, ELEMBONUS.Val, PHYSBONUS.Val, UNIVERSALBONUS.Val, HEALINGBONUS.Val, 90f, 90f };
			await Calculator.Calculate(res, "ATK*0.5*(1+ELEMBONUS+PHYSBONUS+UNIVERSALBONUS)*1.486", Calculator.CalculateMode.NonCrited);

		}
		public async void CallCalculator(string formula)
		{
			res = new float[] { HP.Val, ATK.Val, DEF.Val, EM.Val, CRITRATE.Val, CRITDMG.Val, RECHARGE.Val, ELEMBONUS.Val, PHYSBONUS.Val, UNIVERSALBONUS.Val, HEALINGBONUS.Val, 90f, 90f };
			await Calculator.Calculate(res, formula.Replace("CRITRATE", "0"),Calculator.CalculateMode.NonCrited);
			await Calculator.Calculate(res, formula, Calculator.CalculateMode.Crited);

		}
		#region 拖动slider的数据处理
		public string[] LevelLabelText = { "1", "20", "20+", "40", "40+", "50", "50+", "60", "60+", "70", "70+", "80", "80+", "90" };
		private void SliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			System.Windows.Controls.Slider obj = sender as System.Windows.Controls.Slider;
			switch (obj.Name)
			{
				case "CharacterLevel_Slider": CharacterLevelLabel.Content = string.Format(@"等级：{0}级", LevelLabelText[Convert.ToInt32(obj.Value)]); break;
				case "CharacterConstellation_Slider": CharacterConstellationLabel.Content = string.Format(@"命座：{0}", obj.Value); break;
				case "NormalAttackLevel_Slider": CharacterSkillALabel.Content = string.Format(@"普攻：{0}级", obj.Value); break;
				case "ElementalSkillLevel_Slider": CharacterSkillELabel.Content = string.Format(@"战技：{0}级", obj.Value); break;
				case "ElementalBurstLevel_Slider": CharacterSkillQLabel.Content = string.Format(@"爆发：{0}级", obj.Value); break;
				case "WeaponLevel_Slider": WeaponLevelLabel.Content = string.Format(@"等级：{0}", LevelLabelText[Convert.ToInt32(obj.Value)]); break;
				case "WeaponRefinement_Slider": WeaponRefinementLabel.Content = string.Format(@"精炼{0}阶", obj.Value); break;
				default: break;
			}
			CallCalculator();
		}
		#endregion

		private void CallCalculator(object sender, EventArgs e)
		{
			CallCalculator();
		}
	}
}
