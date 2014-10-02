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
  /// Summary description for GetFileHandler
  /// </summary>
  public class GetFileHandler : IHttpHandler
  {
    public void ProcessRequest(HttpContext context)
    {
      IStatementService _statementService = ObjectFactory.GetInstance<IStatementService>();

      Guid fileId = Guid.Empty;
      if (context.Request.QueryString["fileId"] != null)
        fileId = new Guid(context.Request.QueryString["fileId"]);
      else
        throw new ArgumentException("Не указан параметр");

      Content content = _statementService.GetContentRecord(fileId);
      if (content == null)
        throw new ArgumentException("Не существующий ID");

      context.Response.Clear();
      context.Response.ClearHeaders();
      context.Response.AddHeader("Content-Type", "Application/octet-stream");
      context.Response.AddHeader("Content-Length", content.DocumentContent.Length.ToString());
      context.Response.AddHeader("Content-Disposition", "attachment; filename=" + content.FileName);
      context.Response.BinaryWrite(content.ContentInterior/*content.DocumentContent*/);
      context.Response.Flush();
      context.Response.End();
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