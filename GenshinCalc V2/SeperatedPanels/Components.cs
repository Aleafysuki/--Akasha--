using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace GenshinCalc_V2.SeperatedPanels
{
	internal class Components
	{
		System.Data.DataTable table = new System.Data.DataTable();
		/// <summary>
		/// 数值处理 用于计算输入数据
		/// </summary>
		/// <param name="txt">文本框中的文字信息</param>
		/// <returns>若输入为数值 则返回数值；若输入为百分数，则返回计算好百分比后的数据</returns>
		public string StringCalc(string txt)
		{
			return table.Compute(txt.Trim('%'), "false").ToString();
		}

	}
}
