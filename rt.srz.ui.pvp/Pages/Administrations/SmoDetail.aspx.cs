using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Controllers;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;

namespace rt.srz.ui.pvp.Pages.Administrations
{
	public partial class SmoDetail : System.Web.UI.Page
	{
		private BaseAddCancelPage MasterPage
		{
			get { return (BaseAddCancelPage)this.Master; }
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			MasterPage.Save += MasterPage_Save;
			MasterPage.Cancel += MasterPage_Cancel;
		}

		private Guid SaveChanges()
		{
			return smoDetailControl.SaveChanges();
		}

		//private string ParamCurrentPage
		//{
		//	get { return smoDetailControl.CurrentPageIndex > 0 ? string.Format("CurrentPageIndex={0}", smoDetailControl.CurrentPageIndex) : null; }
		//}

		void MasterPage_Cancel()
		{
			RedirectUtils.RedirectToAdministrationSmos(Response);//, ParamCurrentPage);
		}

		void MasterPage_Save()
		{
			SaveChanges();
			RedirectUtils.RedirectToAdministrationSmos(Response);//, ParamCurrentPage);
		}

		/// <summary>
		/// Обслуживает Intellisense для ввода имени
		/// </summary>
		/// <param name="prefixText"></param>
		/// <param name="count"></param>
		/// <param name="contextKey"></param>
		/// <returns></returns>
		[System.Web.Services.WebMethod]
		public static List<string> GetFirstNameAutoComplete(string prefixText, int count)
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
		public static List<string> GetMiddleNameAutoComplete(string prefixText, int count, string contextKey)
		{
			return IntellisenseController.GetMiddleNameAutoComplete(prefixText, count, contextKey);
		}

	}
}