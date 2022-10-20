using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GenshinCalc_V2.SeperatedPanels
{
	/// <summary>
	/// EnemyConfiguration.xaml 的交互逻辑
	/// </summary>
	public partial class EnemyConfiguration : Window
	{
		public EnemyConfiguration()
		{
			InitializeComponent();
		}
		private void SliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			Slider slider = sender as Slider;
			switch (slider.Name)
			{
				case "EnemyRes_Hydro_Slider":EnemyRes_Hydro_Label.Content = string.Format(@"  水\t{0}%",SliderValueInput(slider));break;
				case "EnemyRes_Pyro_Slider": EnemyRes_Pyro_Label.Content = string.Format(@"  火\t{0}%", SliderValueInput(slider)); break;
				case "EnemyRes_Cyro_Slider": EnemyRes_Cyro_Label.Content = string.Format(@"  冰\t{0}%", SliderValueInput(slider)); break;
				case "EnemyRes_Electro_Slider": EnemyRes_Electro_Label.Content = string.Format(@"  雷\t{0}%", SliderValueInput(slider)); break;
				case "EnemyRes_Anemo_Slider": EnemyRes_Anemo_Label.Content = string.Format(@"  风\t{0}%", SliderValueInput(slider)); break;
				case "EnemyRes_Geo_Slider": EnemyRes_Geo_Label.Content = string.Format(@"  岩\t{0}%", SliderValueInput(slider)); break;
				case "EnemyRes_Dendro_Slider": EnemyRes_Dendro_Label.Content = string.Format(@"  草\t{0}%", SliderValueInput(slider)); break;
				case "EnemyRes_Phys_Slider": EnemyRes_Phys_Label.Content = string.Format(@"  物理\t{0}%",SliderValueInput(slider));break;
			}
		}
		private double SliderValueInput(Slider slider)
		{
			if (slider.Value == slider.Maximum)
			{
				//抗性为无穷
				return double.PositiveInfinity;
			}
			else
			{
				return slider.Value;
			}
		}
	}
}
