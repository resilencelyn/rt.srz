using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.ui.pvp.Controllers;

namespace rt.srz.ui.pvp.Pages.Administrations
{
	public partial class Test : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Обслуживает Intellisense для ввода имени
		/// </summary>
		/// <param name="prefixText"></param>
		/// <param name="count"></param>
		/// <param name="contextKey"></param>
		/// <returns></returns>
		[System.Web.Services.WebMethod]
		public static List<string> GetSFirstNameAutoComplete(string prefixText, int count)
		{
			return IntellisenseController.GetFirstNameAutoComplete(prefixText, count);
		}

		/// <summary>
		/// Обслуживает Intellisense для ввода отчества
		/// </summary>
		/// <param name="prefixText"></param>
		/// <param name="count"></param>
		/// <param name="contextKey"></param>
		/// <returns></returns>
		[System.Web.Services.WebMethod]
		public static List<string> GetSMiddleNameAutoComplete(string prefixText, int count, string contextKey)
		{
			return IntellisenseController.GetMiddleNameAutoComplete(prefixText, count, contextKey);
		}

	}
}