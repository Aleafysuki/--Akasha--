using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GenshinCalc_V2
{
	/// <summary>
	/// 通过具体数值和公式计算伤害值
	/// Calculator.xaml 的交互逻辑
	/// </summary>
	public partial class Calculator : UserControl
	{
		/// <summary>
		/// 对于各种境况的计算规则分支
		/// </summary>
		public enum CalculateMode
		{
			/// <summary>未暴击，直伤</summary>
			NonCrited,
			/// <summary>已暴击，直伤</summary>
			Crited,
			/// <summary>未暴击，1.5倍增幅（火打水蒸发、冰打火融化）</summary>
			NonCrited1,
			/// <summary>已暴击，1.5倍增幅（火打水蒸发、冰打火融化）</summary>
			Crited1,
			/// <summary>未暴击，2.0倍增幅（水打火蒸发、火打冰融化）</summary>
			NonCrited2,
			/// <summary>已暴击，2.0倍增幅（水打火蒸发、火打冰融化）</summary>
			Crited2,

		}
		SeperatedPanels.Components component = new SeperatedPanels.Components();
		public Calculator()
		{
			InitializeComponent();
		}

		private float critrate, critdmg;
		readonly Upheaval upheaval = new Upheaval();
		/// <summary>
		/// 计算未暴击情况下的伤害基础值
		/// </summary>
		/// <param name="res">传入的数据数组
		/// 规定：数组能且仅能按照如下顺序传入
		/// [0]HP
		/// [1]ATK
		/// [2]DEF
		/// [3]EM
		/// [4]CRITRATE
		/// [5]CRITDMG
		/// [6]RECHARGE
		/// [7]ELEMBONUS
		/// [8]PHYSBONUS
		/// [9]UNIVERSALBONUS
		/// [10]HEALINGBONUS
		/// [11]ENEMYRES
		/// [12]ENEMYLEVEL
		/// </param>
		/// <param name="type">传入的运算表达式
		/// 表达式应严格遵守四则运算规则</param>
		public Task<float> NonCritedDMGCalculate(float[] res, string formula)
		{
			//按照符号分隔运算表达式
			//string[] operators=formula.Split(new char[7] { '+','-','*','/','%','^','&'});
			formula = formula.Replace("HP", res[0].ToString());
			formula = formula.Replace("ATK", res[1].ToString());
			formula = formula.Replace("DEF", res[2].ToString());
			formula = formula.Replace("CRITRATE", "0");
			formula = formula.Replace("CRITDMG", "0");
			formula = formula.Replace("RECHARGE", res[6].ToString());
			formula = formula.Replace("ELEMBONUS", res[7].ToString());
			formula = formula.Replace("PHYSBONUS", res[8].ToString());
			formula = formula.Replace("UNIVERSALBONUS", res[9].ToString());
			formula = formula.Replace("HEALINGBONUS", res[10].ToString());
			formula = formula.Replace("ENEMYRES", res[11].ToString());
			formula = formula.Replace("ENEMYLEVEL", res[12].ToString());

			formula = formula.Replace("EM", res[3].ToString());

			NonCritedDMG.Content = component.StringCalc(formula);
			return Task.Run(() =>
			{
				return 0f;
			});
		}
		public Task<float> Calculate(float[] res, string formula, CalculateMode mode)
		{
			//按照符号分隔运算表达式
			//string[] operators=formula.Split(new char[7] { '+','-','*','/','%','^','&'});
			formula = formula.Replace("HP", res[0].ToString());
			formula = formula.Replace("ATK", res[1].ToString());
			formula = formula.Replace("DEF", res[2].ToString());
			formula = formula.Replace("CRITRATE", res[4].ToString());
			formula = formula.Replace("CRITDMG", res[5].ToString());
			formula = formula.Replace("RECHARGE", res[6].ToString());
			formula = formula.Replace("ELEMBONUS", res[7].ToString());
			formula = formula.Replace("EM", res[3].ToString());
			formula = formula.Replace("PHYSBONUS", res[8].ToString());
			formula = formula.Replace("UNIVERSALBONUS", res[9].ToString());
			formula = formula.Replace("HEALINGBONUS", res[10].ToString());
			formula = formula.Replace("ENEMYRES", res[11].ToString());
			formula = formula.Replace("ENEMYLEVEL", res[12].ToString());
			switch (mode)
			{
				case 0: CritedDMG.Content = component.StringCalc(formula);break;
				default:break;
			}

			return Task.Run(() =>
			{
				return 0f;
			});
		}

		private float[] React = new float[4];
		/// <summary>
		/// 计算剧变反应伤害
		/// </summary>
		/// <param name="EM">元素精通</param>
		/// <param name="Level">角色等级（可使用防御力进行推算）</param>
		/// <param name="RES"></param>
		/// <param name="ReactionDMGBuff">反应伤害加成（例如魔女4件套装效果等）</param>
		/// <returns>float数组，分别代表了各种剧变反应下计算出的伤害值
		/// </returns>
		public float[] UpheavalDMGCalculate(float EM, float Level, float RES, float ReactionDMGBuff)
		{
			//计算如下伤害：
			//剧变反应：16.0
			React[0] = (float)(1 + (16.0 / (1 + (2000 / EM))) + ReactionDMGBuff) * RES * upheaval.Upheaval_Damage((int)Level);
			//增幅反应：2.78
			React[1] = (float)(1 + (2.78 / (1 + (1400 / EM))) + ReactionDMGBuff) * RES * upheaval.Upheaval_Damage((int)Level);
			return React;
		}
		public void IsHealerCalculator(bool heal)
		{
			if (heal)
			{
				AvgDMG_Label.Text = "单次治疗";
				CritedDMG_Label.Text = "攻击提升";
				NonCritedDMG_Label.Text = "精通提升";
			}
			else
			{
				AvgDMG_Label.Text = "伤害期望";
				CritedDMG_Label.Text = "暴击前";
				NonCritedDMG_Label.Text = "暴击后";
			}
		}

	}
	#region 剧变反应伤害数值
	//参考：https://bbs.mihoyo.com/ys/article/2215872
	public class Upheaval
	{
		readonly float[] UpheavalBase =					 //1.6版本及以后的剧变反应基准值
			{0,   33,   37,   38,   42,   44,   49,   52,   57,   62,   68, //01~10级
                  73,   81,   89,   97,  107,  118,  128,  139,  150,  161, //11~20级
                 172,  183,  194,  206,  217,  226,  236,  246,  259,  272, //21~30级
                 284,  298,  310,  323,  338,  352,  368,  383,  399,  414, //31~40级
                 430,  448,  467,  487,  511,  537,  562,  590,  618,  647, //41~50级
                 673,  700,  729,  757,  797,  832,  868,  906,  944,  986, //51~60级
                1027, 1078, 1130, 1184, 1248, 1302, 1359, 1416, 1472, 1531, //61~70级
                1589, 1649, 1702, 1754, 1828, 1893, 1958, 2022, 2089, 2154, //71~80级
                2219, 2286, 2352, 2420, 2507, 2578, 2650, 2727, 2810, 2893  //81~90级
            };

		/// <summary>
		/// 根据等级获取反应计算系数
		/// </summary>
		/// <param name="level">角色等级</param>
		/// <param name="Type">是否使用旧反应系数,true=旧反应系数,false=新反应系数</param>
		/// <returns>计算获得的反应系数值</returns>
		public float Upheaval_Damage(int level)
		{
			try
			{
				return UpheavalBase[level];
				// -2E-06 * Math.Pow(level, 5) + 0.0004 * Math.Pow(level, 4) - 0.0246 * Math.Pow(level, 3) + 0.7445 * Math.Pow(level, 2) - 1.6865 * level + 34.394;
			}
			catch (Exception)
			{
				return 0;
			}
		}
	}
	#endregion
}
