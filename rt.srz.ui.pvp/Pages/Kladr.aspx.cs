using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.core.model.interfaces;
using rt.srz.ui.pvp.Controllers;
using StructureMap;

namespace rt.srz.ui.pvp.Pages
{
    public partial class Kladr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Обслуживает Intellisense КЛАДР
        /// </summary>
        /// <param name="prefixText">
        /// The prefix Text.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <param name="contextKey">
        /// The context Key.
        /// </param>
        /// <returns>
        /// The
        ///   <see>
        ///     <cref>List</cref>
        ///   </see>
        ///   .
        /// </returns>
        [WebMethod]
        public static List<string> GetKladrList(string prefixText, int count, string contextKey)
        {
            return IntellisenseController.GetKladrList(prefixText, count, contextKey);
        }

        /// <summary>
        /// Обслуживает Intellisense для ввода отчества
        /// </summary>
        /// <param name="prefixText">
        /// The prefix Text.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <param name="contextKey">
        /// The context Key.
        /// </param>
        /// <returns>
        /// The
        ///   <see>
        ///     <cref>List</cref>
        ///   </see>
        ///   .
        /// </returns>
        [WebMethod]
        public static List<string> GetMiddleNameAutoComplete(string prefixText, int count, string contextKey)
        {
            return IntellisenseController.GetMiddleNameAutoComplete(prefixText, count, contextKey);
        }

        /// <summary>
        /// Возвращает контактную информацию по документу удл, найденному по номеру и серии
        /// </summary>
        /// <param name="kladrId">
        /// The kladr Id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [WebMethod]
        public static string GetPostcodeByKladrId(Guid kladrId)
        {
            var kladrService = ObjectFactory.GetInstance<IAddressService>();
            var kladr = kladrService.GetAddress(kladrId);
            if (kladr != null && kladr.Index != null)
            {
                return kladr.Index.ToString();
            }

            return string.Empty;
        }
    }
}