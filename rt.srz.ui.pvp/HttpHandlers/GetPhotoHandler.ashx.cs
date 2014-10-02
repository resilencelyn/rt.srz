using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;

namespace rt.srz.ui.pvp.HttpHandlers
{
    /// <summary>
    /// Summary description for GetPhotoHandler
    /// </summary>
    public class GetPhotoHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            IStatementService _statementService = ObjectFactory.GetInstance<IStatementService>();

            Guid imageId = Guid.Empty;
            if (context.Request.QueryString["imageId"] != null)
                imageId = new Guid(context.Request.QueryString["imageId"]);
            else
                throw new ArgumentException("Не указан параметр");

            Content content = _statementService.GetContentRecord(imageId);
            if (content == null)
                throw new ArgumentException("Не существующий ID");

            context.Response.ContentType = "image/jpeg";
            context.Response.BinaryWrite(content.ContentInterior/*content.DocumentContent*/);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}