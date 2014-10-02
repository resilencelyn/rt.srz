using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.model.srz.concepts;
using rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps;

namespace rt.srz.ui.pvp.HttpHandlers
{
  /// <summary>
  /// Summary description for UploadPhotoHandler
  /// </summary>
  public class UploadPhotoHandler : IHttpHandler
  {
    public void ProcessRequest(HttpContext context)
    {
      IStatementService _statementService = ObjectFactory.GetInstance<IStatementService>();

      //чтение присланного изображения
      byte[] imageDump = _statementService.ConvertToGrayScale(context.Request.BinaryRead(context.Request.ContentLength));

      //запись присланного изображения в базу
      Content content = null;
      if (imageDump.Length > 0)
        content = _statementService.SaveContentRecord(TypeContent.Foto, imageDump);

      //Возврат ссылки на сохраненное изображение
      context.Response.Clear();
      string imageUri = string.Format(@"{0}://{1}:{2}/HttpHandlers/GetPhotoHandler.ashx?imageId={3}", context.Request.Url.Scheme,
          context.Request.Url.Host, context.Request.Url.Port, content.Id);
      context.Response.Write(imageUri);
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