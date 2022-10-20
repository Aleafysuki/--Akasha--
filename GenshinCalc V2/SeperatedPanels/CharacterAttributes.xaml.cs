using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Accessibility;

namespace GenshinCalc_V2
{
	/// <summary>
	/// CharacterAttributes.xaml 的交互逻辑
	/// </summary>
	public partial class CharacterAttributes : UserControl
	{
		SeperatedPanels.Components component = new SeperatedPanels.Components();
		public float Val
		{
			get => Enumerable.Sum(Value);
			//set => Enumerable.Sum(Value);
		}

		/// <summary>
		/// 设定这一种属性是否以百分比形式展示
		/// </summary>
		public bool IsPercentage = true;
		public CharacterAttributes()
		{
			InitializeComponent();
			if ("生命值攻击力防御力元素精通".Contains(AttributeNameLabel.Content.ToString()))
			{
				IsPercentage = false;
			}
			else IsPercentage = true;
			CharacterAttributes_Loaded();
		}
		private void CharacterAttributes_Loaded()
		{
			CharacterBasic.TabIndex = TabIndex * 10 + 1;
			WeaponBasic.TabIndex = TabIndex * 10 + 2;
			CharacterAscention.TabIndex = TabIndex * 10 + 3;
			WeaponAttribute.TabIndex = TabIndex * 10 + 4;
			WeaponEffect.TabIndex = TabIndex * 10 + 5;
			ArtifactAttribute.TabIndex = TabIndex * 10 + 6;
			ElementalResonance.TabIndex = TabIndex * 10 + 7;
			PartnerEffect.TabIndex = TabIndex * 10 + 8;
			OtherEffect.TabIndex = TabIndex * 10 + 9;
		}
		float[] Value = new float[9];
		public delegate void ValueSettingsDelegate(object sender, EventArgs e);
		/// <summary>
		/// 更改其中数据时引发事件
		/// </summary>
		public event ValueSettingsDelegate ValueChange;
		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox Obj = sender as TextBox;
			if (IsPercentage && !Obj.Text.EndsWith('%'))
			{
				Obj.Text += '%';
				Obj.Select(Obj.Text.Length - 1, 0);
			}
			try
			{
				switch (Obj.Name)
				{
					case "CharacterBasic": Value[0] = PercentExec(Obj.Text.Trim()); break;
					case "WeaponBasic": Value[1] = PercentExec(Obj.Text.Trim()); break;
					case "CharacterAscention": Value[2] = PercentExec(Obj.Text.Trim()); break;
					case "WeaponAttribute": Value[3] = PercentExec(Obj.Text.Trim()); break;
					case "WeaponEffect": Value[4] = PercentExec(Obj.Text.Trim()); break;
					case "ArtifactAttribute": Value[5] = PercentExec(Obj.Text.Trim()); break;
					case "ElementalResonance": Value[6] = PercentExec(Obj.Text.Trim()); break;
					case "PartnerEffect": Value[7] = PercentExec(Obj.Text.Trim()); break;
					case "OtherEffect": Value[8] = PercentExec(Obj.Text.Trim()); break;
					default:; break;
				}
			}
			catch (System.Exception)
			{

			}
			Total.Content = IsPercentage ? (Enumerable.Sum(Value) * 100).ToString("0.0") + '%' : Enumerable.Sum(Value).ToString();
			ValueChange?.Invoke(sender, e);
		}


		/// <summary>
		/// 数值处理 用于计算输入数据
		/// </summary>
		/// <param name="txt">文本框中的文字信息</param>
		/// <returns>若输入为数值 则返回数值；若输入为百分数，则返回计算好百分比后的数据</returns>
		private float PercentExec(string txt)
		{
			if (!IsPercentage)
			{
				if (txt.Contains('%'))
				{
					return (Value[0] + Value[1]) * float.Parse(txt.TrimEnd('%')) / 100;
				}
				else return float.Parse(txt);
			}
			else
			{
				return float.Parse(txt.TrimEnd('%')) / 100;
			}
		}

		/// <summary>
		/// 设定不可使用的属性（例如武器无法提供生命值白字等）
		///"CharacterBasic"		角色基础值	//
		///"WeaponBasic"		武器基础值	//
		///"CharacterAscention"	角色突破加成	//
		///"WeaponAttribute"	武器的副属性	//
		///"WeaponEffect"		武器的特效	//
		///"ArtifactAttribute"	圣遗物对属性的加成	//
		///"ElementalResonance"	元素共鸣造成的加成	//
		///"PartnerEffect"		队友的辅助效果	//
		///"OtherEffect"		其他（如吃伤害加成食品等）
		/// </summary>
		/// <param name="s">文本框中的文字信息 以半角逗号分隔
		/// </param>
		public void SetUnavaliableItems(string s)
		{
			if (s.ToLower().Contains("all"))
				s = "CharacterBasic,WeaponBasic,CharacterAscention," +
					"WeaponAttribute,WeaponEffect,ArtifactAttribute," +
					"ElementalResonance,PartnerEffect,OtherEffect";
			string[] items = s.Split('\u002C');
			foreach (string item in items)
			{
				switch (item)
				{
					case "CharacterBasic":
						{
							CharacterBasic.Clear();
							Value[0] = 0;
							CharacterBasic.IsEnabled = false;
							CharacterBasic.Visibility = Visibility.Hidden;
							break;
						}
					case "WeaponBasic":
						{
							WeaponBasic.Clear();
							Value[1] = 0;
							WeaponBasic.IsEnabled = false;
							WeaponBasic.Visibility = Visibility.Hidden;
							break;
						}
					case "CharacterAscention":
						{
							CharacterAscention.Clear();
							Value[2] = 0;
							CharacterAscention.IsEnabled = false;
							CharacterAscention.Visibility = Visibility.Hidden;
							break;
						}
					case "WeaponAttribute":
						{
							WeaponAttribute.Clear();
							Value[3] = 0;
							WeaponAttribute.IsEnabled = false;
							WeaponAttribute.Visibility = Visibility.Hidden;
							break;
						}
					case "WeaponEffect":
						{
							WeaponEffect.Clear();
							Value[4] = 0;
							WeaponEffect.IsEnabled = false;
							WeaponEffect.Visibility = Visibility.Hidden;
							break;
						}
					case "ArtifactAttribute":
						{
							ArtifactAttribute.Clear();
							Value[5] = 0;
							ArtifactAttribute.IsEnabled = false;
							ArtifactAttribute.Visibility = Visibility.Hidden;
							break;
						}
					case "ElementalResonance":
						{
							ElementalResonance.Clear();
							Value[6] = 0;
							ElementalResonance.IsEnabled = false;
							ElementalResonance.Visibility = Visibility.Hidden;
							break;
						}
					case "PartnerEffect":
						{
							PartnerEffect.Clear();
							Value[7] = 0;
							PartnerEffect.IsEnabled = false;
							PartnerEffect.Visibility = Visibility.Hidden;
							break;
						}
					case "OtherEffect":
						{
							OtherEffect.Clear();
							Value[8] = 0;
							OtherEffect.IsEnabled = false;
							OtherEffect.Visibility = Visibility.Hidden;
							break;
						}
					default:; break;
				}
			}
		}
		/// <summary>
		/// 设定锁定的属性（例如角色永远自带100%元素充能效率等）
		///"CharacterBasic"		角色基础值	//
		///"WeaponBasic"		武器基础值	//
		///"CharacterAscention"	角色突破加成	//
		///"WeaponAttribute"	武器的副属性	//
		///"WeaponEffect"		武器的特效	//
		///"ArtifactAttribute"	圣遗物对属性的加成	//
		///"ElementalResonance"	元素共鸣造成的加成	//
		///"PartnerEffect"		队友的辅助效果	//
		///"OtherEffect"		其他（如吃伤害加成食品等）
		/// </summary>
		/// <param name="s">文本框中的文字信息 以半角逗号分隔
		/// <param name="s">文本框中的文字信息 以半角逗号分隔
		/// </param>
		public void SetLockedItems(string s, string n)
		{
			string[] items = s.Split('\u002C');
			string[] vals = n.Split('\u002C');
			for (int i = 0; i < items.Length; i++)
			{
				switch (items[i])
				{
					case "CharacterBasic":
						{
							CharacterBasic.Text = vals[i];
							Value[0] = PercentExec(vals[i]);
							CharacterBasic.IsEnabled = false;
							break;
						}
					case "WeaponBasic":
						{
							WeaponBasic.Text = vals[i];
							Value[1] = PercentExec(vals[i]);
							WeaponBasic.IsEnabled = false;
							break;
						}
					case "CharacterAscention":
						{
							CharacterAscention.Text = vals[i];
							Value[2] = PercentExec(vals[i]);
							CharacterAscention.IsEnabled = false;
							break;
						}
					case "WeaponAttribute":
						{
							WeaponAttribute.Text = vals[i];
							Value[3] = PercentExec(vals[i]);
							WeaponAttribute.IsEnabled = false;
							break;
						}
					case "WeaponEffect":
						{
							WeaponEffect.Text = vals[i];
							Value[4] = PercentExec(vals[i]);
							WeaponEffect.IsEnabled = false;
							break;
						}
					case "ArtifactAttribute":
						{
							ArtifactAttribute.Text = vals[i];
							Value[5] = PercentExec(vals[i]);
							ArtifactAttribute.IsEnabled = false;
							break;
						}
					case "ElementalResonance":
						{
							ElementalResonance.Text = vals[i];
							Value[6] = PercentExec(vals[i]);
							ElementalResonance.IsEnabled = false;
							break;
						}
					case "PartnerEffect":
						{
							PartnerEffect.Text = vals[i];
							Value[7] = PercentExec(vals[i]);
							PartnerEffect.IsEnabled = false;
							break;
						}
					case "OtherEffect":
						{
							OtherEffect.Text = vals[i];
							Value[8] = PercentExec(vals[i]);
							OtherEffect.IsEnabled = false;
							break;
						}
					default:; break;
				}
			}
		}
		/// <summary>
		/// 设定一列中可用的属性（例如角色突破只增加攻击力、武器副属性只增加暴击率等等）
		/// 示例输入：SetEnabledStat("ATK,EM","CharacterAscention,WeaponAttribute");
		/// 字符串s与t的逗号数量要保持一致，除非s中包含子串"all"
		/// </summary>
		/// <param name="t">输入的控制用字符串，以半角逗号分隔，指定是哪一列</param>
		public void SetEnabledItems(string t, bool enable)
		{
			string[] columns = t.Split('\u002C');
			foreach (string column in columns)
			{
				switch (column)
				{
					case "CharacterBasic":
						{
							CharacterBasic.Visibility = enable ? Visibility.Visible : Visibility.Hidden;
							CharacterBasic.IsEnabled = enable;
							break;
						}
					case "WeaponBasic":
						{
							WeaponBasic.Visibility = enable ? Visibility.Visible : Visibility.Hidden;
							WeaponBasic.IsEnabled = enable;
							break;
						}
					case "CharacterAscention":
						{
							CharacterAscention.Visibility = enable ? Visibility.Visible : Visibility.Hidden;
							CharacterAscention.IsEnabled = enable;
							break;
						}
					case "WeaponAttribute":
						{
							WeaponAttribute.Visibility = enable ? Visibility.Visible : Visibility.Hidden;
							WeaponAttribute.IsEnabled = enable;
							break;
						}
					case "WeaponEffect":
						{
							WeaponEffect.Visibility = enable ? Visibility.Visible : Visibility.Hidden;
							WeaponEffect.IsEnabled = enable;
							break;
						}
					case "ArtifactAttribute":
						{
							ArtifactAttribute.Visibility = enable ? Visibility.Visible : Visibility.Hidden;
							ArtifactAttribute.IsEnabled = enable;
							break;
						}
					case "ElementalResonance":
						{
							ElementalResonance.Visibility = enable ? Visibility.Visible : Visibility.Hidden;
							ElementalResonance.IsEnabled = enable;
							break;
						}
					case "PartnerEffect":
						{
							PartnerEffect.Visibility = enable ? Visibility.Visible : Visibility.Hidden;
							PartnerEffect.IsEnabled = enable;
							break;
						}
					case "OtherEffect":
						{
							OtherEffect.Visibility = enable ? Visibility.Visible : Visibility.Hidden;
							OtherEffect.IsEnabled = enable;
							break;
						}
					default:; break;
				}
			}
		}
		/// <summary>
		/// 使所有属性回到可用状态
		/// </summary>
		/// <param name="c">控件归类名(例如一个控件叫做攻击力)</param>
		public void UnlockAllTextBoxes(UIElementCollection c)
		{
			foreach (UIElement element in c)
			{
				if (element is TextBox textBox)
				{
					textBox.IsReadOnly = false;
				}
			}
		}
		public void SetUpperLabels(UIElementCollection c, bool HideOrNot)
		{
			foreach (UIElement element in c)
			{
				if (element is Label label)
				{
					label.Visibility = HideOrNot ? Visibility.Hidden : Visibility.Visible;
				}
			}
		}
		#region 对输入数据进行简单处理
		/// <summary>
		/// 对表达式进行简单的四则运算
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FastCalculate(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Enter)
			{
				TextBox textBox = sender as TextBox;
				textBox.Text = component.StringCalc(textBox.Text.Trim('%')).ToString();
			}
		}
		#endregion

		#region 标签与文本框的可见性同步
		/// <summary>
		/// 同步CharacterBasic与其上方标签文字（“角色基础值”）可见性
		/// </summary>
		private void TextBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			VisibilitySync();
		}
		private void TextBox_IsVisibleChanged(object sender, RoutedEventArgs e)
		{
			VisibilitySync();
		}
		private void VisibilitySync()
		{
			CharacterBasic_Label.Visibility = CharacterBasic.Visibility;
			WeaponBasic_Label.Visibility = WeaponBasic.Visibility;
			CharacterAscention_Label.Visibility = CharacterAscention.Visibility;
			WeaponAttribute_Label.Visibility = WeaponAttribute.Visibility;
			WeaponEffect_Label.Visibility = WeaponEffect.Visibility;
			ArtifactAttribute_Label.Visibility = ArtifactAttribute.Visibility;
			ElementalResonance_Label.Visibility = ElementalResonance.Visibility;
			PartnerEffect_Label.Visibility = PartnerEffect.Visibility;
			OtherEffect_Label.Visibility = OtherEffect.Visibility;
		}
		#endregion

		

	}
}
