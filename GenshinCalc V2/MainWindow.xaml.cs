using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static GenshinCalc_V2.ColorExtension;

namespace GenshinCalc_V2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
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
		}
		private void CharacterAttributes_Loaded()
		{
			
		}
	}
}
