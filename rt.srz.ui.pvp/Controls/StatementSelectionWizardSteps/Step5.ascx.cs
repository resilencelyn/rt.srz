// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Step5.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.model.srz.concepts;
using rt.srz.services;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;
using Content = rt.srz.model.srz.Content;
using Image = System.Drawing.Image;
using System.Web.UI.HtmlControls;

#endregion

namespace rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps
{
  /// <summary>
  ///   The step 5.
  /// </summary>
  public partial class Step5 : WizardStep
  {
    private IStatementService _service;

    private int PolicyType
    {
      get { return CurrentStatement.FormManufacturing != null ? CurrentStatement.FormManufacturing.Id : -1; }
    }

    /// <summary>
    ///   Gets the photo control type.
    /// </summary>
    private PhotoControlType PhotoControlType
    {
      get
      {
        // Получение типа компонента
        var configValue = ObjectFactory.GetInstance<IStatementService>().GetSettingCurrentUser("PhotoControlType");
        return (PhotoControlType)Enum.Parse(typeof(PhotoControlType), configValue);
      }
    }

    public void ClearSessionData()
    {
      Session[SessionConsts.CAttachedPhotoId] = null;
      Session[SessionConsts.CAttachedSignatureId] = null;
      //Session[SessionConsts.CAttachedFileKey] = null;
    }

    /// <summary>
    /// The move data from object 2 gui.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    public override void MoveDataFromObject2GUI(Statement statement)
    {
      // Загрузка ссылки на подпись и фото
      if (statement.InsuredPersonData != null && statement.InsuredPersonData.Contents != null)
      {
        // фото
        if (statement.InsuredPersonData.Contents.Any(x => x.ContentType.Id == TypeContent.Foto))
        {
          var attachedPhotoId = statement.InsuredPersonData.Contents.Where(x => x.ContentType.Id == TypeContent.Foto).
            Select(x => x.Id).First();

          Session[SessionConsts.CAttachedPhotoId] = attachedPhotoId;
        }

        // подпись
        if (statement.InsuredPersonData.Contents.Any(x => x.ContentType.Id == TypeContent.Signature))
        {
          var attachedSignatureId =
            statement.InsuredPersonData.Contents.Where(x => x.ContentType.Id == TypeContent.Signature).
              Select(x => x.Id).First();

          Session[SessionConsts.CAttachedSignatureId] = attachedSignatureId;
        }
      }

      // Загрузка ссылок на файлы
      //if (statement.InsuredPersonData != null && statement.InsuredPersonData.Contents != null &&
      //    statement.InsuredPersonData.Contents.Any(x => x.ContentType.Id == TypeContent.TypeContent20))
      //{
      //  var attachedFilesIdList = statement
      //    .InsuredPersonData.Contents.Where(x => x.ContentType.Id == TypeContent.TypeContent20)
      //    .Select(x => x.Id).ToArray();
      //  Session[SessionConsts.CAttachedFileKey] = attachedFilesIdList;
      //}
    }

    /// <summary>
    /// Переносит данные из элементов на форме в объект
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// </summary>
    /// <param name="setCurrentStatement">
    /// Обновлять ли свойство CurrentStatement после присвоения заявлению данных из дизайна 
    /// </param>
    public override void MoveDataFromGui2Object(ref Statement statement, bool setCurrentStatement = true)
    {
      // создание списка контента
      if (statement.InsuredPersonData != null && statement.InsuredPersonData.Contents == null)
      {
        statement.InsuredPersonData.Contents = new List<Content>();
      }
      statement.InsuredPersonData.Contents.Clear();

      var _statementService = ObjectFactory.GetInstance<IStatementService>();

      // Сохраняем фото и подпись только для электронного полиса 
      if (PolicyType == PolisType.Э)
      {
        switch (PhotoControlType)
        {
          case PhotoControlType.Webcam:
            {
              // через веб камеру
              if (!string.IsNullOrEmpty(hfImageUrl.Value))
              {
                var queryString = new Uri(hfImageUrl.Value).Query;
                var queryDictionary = HttpUtility.ParseQueryString(queryString);
                if (queryDictionary["imageID"] != null)
                {
                  var photoId = new Guid(queryDictionary["imageId"]);
                  MovePhotoFromGui2Object(statement, photoId);
                }
              }
            }

            break;
          case PhotoControlType.Upload:
            {
              // через загрузку фотографии из файла
              if (Session[SessionConsts.CAttachedPhotoId] != null)
              {
                MovePhotoFromGui2Object(statement, (Guid)Session[SessionConsts.CAttachedPhotoId]);
              }
            }

            break;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Добавление ссылки на подпись
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (Session[SessionConsts.CAttachedSignatureId] != null)
        {
          // получение ранее сохраненой подписи из таблицы Content
          var content = _statementService.GetContentRecord((Guid)Session[SessionConsts.CAttachedSignatureId]);

          // добавление ссылки на фотографию в заявление  
          if (!statement.InsuredPersonData.Contents.Contains(content))
          {
            statement.InsuredPersonData.Contents.Add(content);
          }
        }
      }

      // Добавление ссылок на файлы
      //if (Session[SessionConsts.CAttachedFileKey] != null)
      //{
      //  var attachedFilesIdList = Session[SessionConsts.CAttachedFileKey] as Guid[];
      //  if (attachedFilesIdList != null)
      //  {
      //    foreach (var attachedFileId in attachedFilesIdList)
      //    {
      //      // получение ранее сохраненой записи из таблицы Content
      //      var content = _statementService.GetContentRecord(attachedFileId);

      //      // добавление ссылки на файл в заявление
      //      if (statement.InsuredPersonData != null && !statement.InsuredPersonData.Contents.Contains(content))
      //      {
      //        statement.InsuredPersonData.Contents.Add(content);
      //      }
      //    }
      //  }
      //}

      //сохранение изменений в сессию
      if (setCurrentStatement)
      {
        CurrentStatement = statement;
      }
    }

    /// <summary>
    ///   Проверка доступности элементов редактирования для ввода
    /// </summary>
    public override void CheckIsRightToEdit()
    {
      var propertyList = GetPropertyListForCheckIsRightToEdit();

      // Блокировка загрузичков фото, подписи и файлов
      var _statementService = ObjectFactory.GetInstance<IStatementService>();
      fuPhotoUploader.Enabled =
        fuSignatureUploader.Enabled =
        //fuFileUploader.Enabled =
        btnLoadEmptyPhoto.Enabled =
        btnLoadEmptySign.Enabled =
        _statementService.IsRightToEdit(propertyList, Utils.GetExpressionNode(x => x.InsuredPersonData.Contents));
    }

    /// <summary>
    ///   The show or hide electronic policy part.
    /// </summary>
    public void ShowOrHideElectronicPolicyPart()
    {
      // Возможность сделать фото и ввести подпись предоставляется только для электронного типа полиса
      if (CurrentStatement.AbsentPrevPolicy.HasValue && CurrentStatement.AbsentPrevPolicy.Value /*NeedNewPolicy*/ 
        && CurrentStatement.FormManufacturing != null && CurrentStatement.FormManufacturing.Id == PolisType.Э)
      {
        SetVisible(divElectronicPolicy);

        // Выбор способа задания фотографии в зависимости от настроек(веб-камера либо загрузка файла)
        switch (PhotoControlType)
        {
          case PhotoControlType.Upload:
            {
              // через загрузку фотографии из файла
              SetVisible(divPhotoUpload);

              // Скрытие либо отображение фотографии
              if (Session[SessionConsts.CAttachedPhotoId] != null)
              {
                SetVisible(divImagePhoto);
              }
              else
              {
                SetNonVisible(divImagePhoto);
              }
              SetNonVisible(divPhotoWebCam);
            }

            break;
          case PhotoControlType.Webcam:
            {
              // через веб камеру
              SetVisible(divPhotoWebCam);
              SetNonVisible(divPhotoUpload);
            }

            break;
        }

        // скрытие либо отображение подписи
        if (Session[SessionConsts.CAttachedSignatureId] != null)
        {
          SetVisible(divImageSignature);
        }
        else
        {
          SetNonVisible(divImageSignature);
        }
      }
      else
      {
        SetNonVisible(divElectronicPolicy);
      }
    }

    public void SetNonVisible(HtmlGenericControl control)
    {
      control.Style.Add("display", "none");
    }

    public void SetVisible(HtmlGenericControl control)
    {
      control.Style.Remove("display");
      control.Style.Add("display", "block");
    }

    /// <summary>
    /// Установка фокуса на контрол при смене шага
    /// </summary>
    public override void SetDefaultFocus()
    {
      if (CurrentStatement.AbsentPrevPolicy.Value && PolicyType == PolisType.Э)
      {
        fuPhotoUploader.Focus();
      }
      else
      {
        //fuFileUploader.Focus();
      }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<IStatementService>();
    }

    /// <summary>
    /// The page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void Page_Load(object sender, EventArgs e)
    {
      // Восстановление значения после PostBack
      if (!IsPostBack)
      {
        // Блокировка кнопки обрезать фотографию. Будет разблокирована на клиенте после выбора новой области для обрезки
        btnCropPhoto.Enabled = false;

        // Блокировка кнопки обрезать подпись. Будет разблокирована на клиенте после выбора новой области для обрезки
        btnCropSignature.Enabled = false;

      }
      else
      {
        ////после загрузки файлов делается принудительный постбек и значение из контролов исчезает о загруженном файле. 
        ////Чтобы этого не было при постбеке передаём путь загружаемого файла и на лоад формы его вручную проставляем
        ////это работает если аплоадеры со стилем Modern

        ////контролы которые запросили постбек
        ////string controlsRequestedPostback = Request.Params["__EVENTTARGET"];
        ////if (!string.IsNullOrEmpty(controlsRequestedPostback))
        ////{
        ////  if (controlsRequestedPostback == upPhotoUpload.ClientID)
        ////  {
        ////    fuPhotoUploader.LoadedEarlierText = Request["__EVENTARGUMENT"];
        ////  }
        ////  else if (controlsRequestedPostback == upSignatureUpload.ClientID)
        ////  {
        ////    fuSignatureUploader.LoadedEarlierText = Request["__EVENTARGUMENT"];
        ////  }
        ////  else if (controlsRequestedPostback == upFileUpload.ClientID)
        ////  {
        ////    fuFileUploader.LoadedEarlierText = Request["__EVENTARGUMENT"];
        ////  }
        ////}

        // загрузка фотографии
        BindPhoto();

        // загрузка подписи
        BindSignature();

        // загрузка прикрепленных данных в грид
        //BindData2AttachedFileGridView();

        var st = CurrentStatement;
        MoveDataFromGui2Object(ref st);
      }

      ShowOrHideElectronicPolicyPart();

      if (PolicyType == PolisType.П || PolicyType == PolisType.К)
      {
        Label1.Text = "";
        //Label1.Text = "Добавьте копии документов застрахованного лица";
        //divFileUploadTitle.Style.Add("display", "none");
      }
      else if (PolicyType == PolisType.Э)
      {
        Label1.Text = "Добавьте фотографию и образец подписи застрахованного лица";
        //divFileUploadTitle.Style.Add("display", "block");
      }
    }

    /// <summary>
    /// Обработчик загрузки фотографии
    /// </summary>
    /// <param name="sender">
    /// </param>
    /// <param name="e">
    /// </param>
    protected void fuPhotoUploader_PhotoUploadComplete(object sender, EventArgs e)
    {
      var _statementService = ObjectFactory.GetInstance<IStatementService>();

      // запись присланной фотографии в базу
      if (fuPhotoUploader.HasFile)
      {
        var cont = _statementService.ConvertToGrayScale(fuPhotoUploader.FileBytes);

        // запись фотографии в базу
        var content = _statementService.SaveContentRecord(TypeContent.Foto, cont);

        // пишем Id созданной записи в Session
        Session[SessionConsts.CAttachedPhotoId] = content.Id;

        // загрузка подписи
        BindPhoto();
      }
    }

    /// <summary>
    /// Обработчик операции обрезания фотографии
    /// </summary>
    /// <param name="sender">
    /// </param>
    /// <param name="e">
    /// </param>
    protected void btnCropPhoto_Click(object sender, EventArgs e)
    {
      if (Session[SessionConsts.CAttachedPhotoId] == null)
      {
        return;
      }

      var _statementService = ObjectFactory.GetInstance<IStatementService>();

      // Загрузка старой фотографии из базы
      var originalPhotoContent = _statementService.GetContentRecord((Guid)Session[SessionConsts.CAttachedPhotoId]);
      if (originalPhotoContent == null)
      {
        return;
      }

      // Получение размеров 
      var x = Convert.ToInt32(Convert.ToDecimal(hfPhotoX.Value));
      var y = Convert.ToInt32(Convert.ToDecimal(hfPhotoY.Value));
      var width = Convert.ToInt32(Convert.ToDecimal(hfPhotoWidth.Value));
      var height = Convert.ToInt32(Convert.ToDecimal(hfPhotoHeigth.Value));

      // Обрезка фотографии
      var croppedPhoto = CropImage(originalPhotoContent.DocumentContent, x, y, width, height);
      if (croppedPhoto == null)
      {
        return;
      }

      // Запись обрезанной фотографии в базу
      var croppedPhotoContent = _statementService.SaveContentRecord(TypeContent.Foto, croppedPhoto);

      // Удаление старой подписи из базы
      _statementService.ContentRemove(originalPhotoContent);

      // пишем Id созданной записи в Session
      Session[SessionConsts.CAttachedPhotoId] = croppedPhotoContent.Id;

      BindPhoto();

      // блокировка кнопки обрезать. Будет разблокирована на клиенте после выбора новой области для обрезки
      btnCropPhoto.Enabled = false;
    }

    /// <summary>
    /// Обработчик загрузки подписи
    /// </summary>
    /// <param name="sender">
    /// </param>
    /// <param name="e">
    /// </param>
    protected void fuSignatureUploader_SignatureUploadComplete(object sender, EventArgs e)
    {
      var _statementService = ObjectFactory.GetInstance<IStatementService>();

      var cont = _statementService.ConvertToGrayScale(fuSignatureUploader.FileBytes);

      using (var originalImageStream = new MemoryStream(cont))
      {
        using (var originalImage = Image.FromStream(originalImageStream))
        {
          if (originalImage.Width < 736 || originalImage.Height < 160)
          {
            cont = CropImage(cont, 0, 0, Math.Max(736, originalImage.Width), Math.Max(160, originalImage.Height));
          }
        }
      }

      // запись присланного файла в базу
      if (fuSignatureUploader.HasFile)
      {
        // запись файла с подписью в базу
        var content = _statementService.SaveContentRecord(TypeContent.Signature, cont);

        // пишем Id созданной записи в Session
        Session[SessionConsts.CAttachedSignatureId] = content.Id;

        // загрузка подписи
        BindSignature();
      }
    }

    protected void btnLoadEmptySign_Click(object sender, EventArgs e)
    {
      var service = ObjectFactory.GetInstance<IStatementService>();
      var cont = service.CreateEmptySign();
      var content = service.SaveContentRecord(TypeContent.Signature, cont);
      Session[SessionConsts.CAttachedSignatureId] = content.Id;
      SetVisible(divImageSignature);
      BindSignature();
    }

    protected void btnLoadEmptyPhoto_Click(object sender, EventArgs e)
    {
      var service = ObjectFactory.GetInstance<IStatementService>();
      var cont = service.CreateEmptyPhoto();
      var content = service.SaveContentRecord(TypeContent.Foto, cont);
      Session[SessionConsts.CAttachedPhotoId] = content.Id;
      SetVisible(divImagePhoto);
      BindPhoto();
    }

    /// <summary>
    /// Обработчик операции обрезания подписи
    /// </summary>
    /// <param name="sender">
    /// </param>
    /// <param name="e">
    /// </param>
    protected void btnCropSignature_Click(object sender, EventArgs e)
    {
      if (Session[SessionConsts.CAttachedSignatureId] == null)
      {
        return;
      }

      var _statementService = ObjectFactory.GetInstance<IStatementService>();

      // Загрузка старой подписи из базы
      var originalSignatureContent = _statementService.GetContentRecord((Guid)Session[SessionConsts.CAttachedSignatureId]);
      if (originalSignatureContent == null)
      {
        return;
      }

      // Получение размеров 
      var x = Convert.ToInt32(Convert.ToDecimal(hfSignatureX.Value));
      var y = Convert.ToInt32(Convert.ToDecimal(hfSignatureY.Value));
      var width = Convert.ToInt32(Convert.ToDecimal(hfSignatureWidth.Value));
      var height = Convert.ToInt32(Convert.ToDecimal(hfSignatureHeigth.Value));

      // Обрезка подписи
      var croppedSignature = CropImage(originalSignatureContent.DocumentContent, x, y, width, height);
      if (croppedSignature == null)
        return;

      // Запись обрезанной подписи в базу
      var croppedSignatureContent = _statementService.SaveContentRecord(TypeContent.Signature, croppedSignature);

      // Удаление старой подписи из базы
      _statementService.ContentRemove(originalSignatureContent);

      // пишем Id созданной записи в Session
      Session[SessionConsts.CAttachedSignatureId] = croppedSignatureContent.Id;

      BindSignature();

      // блокировка кнопки обрезать. Будет разблокирована на клиенте после выбора новой области для обрезки
      btnCropSignature.Enabled = false;
    }

    /// <summary>
    /// Обработчик удаления прикрепленного файла
    /// </summary>
    /// <param name="sender">
    /// </param>
    /// <param name="e">
    /// </param>
    //protected void gvAttachedFiles_DeleteFile(object sender, EventArgs e)
    //{
    //  var _statementService = ObjectFactory.GetInstance<IStatementService>();

    //  // Загрузка записи из базы
    //  var linkButton = sender as LinkButton;
    //  if (linkButton == null)
    //  {
    //    return;
    //  }

    //  var attachedFileId = new Guid(linkButton.CommandArgument);
    //  var content = _statementService.GetContentRecord(attachedFileId);
    //  if (content == null)
    //  {
    //    return;
    //  }

    //  // Удаление записи из базы
    //  _statementService.ContentRemove(content);

    //  // Удаление из сессии
    //  if (Session[SessionConsts.CAttachedFileKey] != null)
    //  {
    //    var attachedFilesIdList = Session[SessionConsts.CAttachedFileKey] as Guid[];
    //    if (attachedFilesIdList != null)
    //    {
    //      Session[SessionConsts.CAttachedFileKey] = attachedFilesIdList.Where(x => x != content.Id).ToArray();
    //    }
    //  }

    //  // Добавление данных в грид
    //  BindData2AttachedFileGridView();
    //}

    /// <summary>
    /// The move photo from gui 2 object.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    /// <param name="photoId">
    /// The photo id. 
    /// </param>
    private void MovePhotoFromGui2Object(Statement statement, Guid photoId)
    {
      var _statementService = ObjectFactory.GetInstance<IStatementService>();

      // получение ранее сохраненой подписи из таблицы Content
      var content = _statementService.GetContentRecord(photoId);

      // добавление ссылки на фотографию в заявление  
      if (!statement.InsuredPersonData.Contents.Contains(content))
      {
        statement.InsuredPersonData.Contents.Add(content);
      }
    }

    /// <summary>
    ///   Биндинг фотографии
    /// </summary>
    private void BindPhoto()
    {
      // загрузка ссылки на фотографию
      var imageUri = hfImageUrl.Value;

      if (string.IsNullOrEmpty(imageUri) && Session[SessionConsts.CAttachedPhotoId] != null)
      {
        // Получение Id файла подписи из сессии
        var id = (Guid)Session[SessionConsts.CAttachedPhotoId];

        // Построение ссылки на файл с подписью
        imageUri = GetImageUri(id);
      }

      if (!string.IsNullOrEmpty(imageUri))
      {
        // Выбор способа задания фотографии в зависимости от настроек(веб-камера либо загрузка файла)
        switch (PhotoControlType)
        {
          // через загрузку фотографии из файла
          case PhotoControlType.Upload:
            {
              imagePhoto.ImageUrl = imageUri;
              upPhoto.Update();
            }

            break;

          // через веб камеру
          case PhotoControlType.Webcam:
            {
              image.InnerHtml = string.Format("<img src=\"{0}\">", imageUri);
              hfImageUrl.Value = imageUri;
            }

            break;
        }
      }
    }

    protected string GetImageUri(Guid id)
    {
      string imageUri;
      if (System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath == "/") //Тестовый сервер
      {
        imageUri = string.Format(
          @"{0}://{1}:{2}/HttpHandlers/GetPhotoHandler.ashx?imageId={3}",
          Request.Url.Scheme,
          Request.Url.Host,
          Request.Url.Port,
          id);
      }
      else //IIS
      {
        imageUri = string.Format(
          @"{0}://{1}:{2}{4}/HttpHandlers/GetPhotoHandler.ashx?imageId={3}",
          Request.Url.Scheme,
          Request.Url.Host,
          Request.Url.Port,
          id,
          System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
      }
      return imageUri;
    }

    protected string GetFileUri(string id)
    {
      string imageUri;
      if (System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath == "/") //Тестовый сервер
      {
        imageUri = string.Format(
          @"{0}://{1}:{2}/HttpHandlers/GetFileHandler.ashx?fileId={3}",
          Request.Url.Scheme,
          Request.Url.Host,
          Request.Url.Port,
          id);
      }
      else //IIS
      {
        imageUri = string.Format(
          @"{0}://{1}:{2}{4}/HttpHandlers/GetFileHandler.ashx?fileId={3}",
          Request.Url.Scheme,
          Request.Url.Host,
          Request.Url.Port,
          id,
          System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
      }
      return imageUri;
    }

    /// <summary>
    ///   Биндинг подписи
    /// </summary>
    private void BindSignature()
    {
      if (Session[SessionConsts.CAttachedSignatureId] != null)
      {
        // Получение Id файла подписи из сессии
        var Id = (Guid)Session[SessionConsts.CAttachedSignatureId];

        // Построение ссылки на файл с подписью
        var imageUri = GetImageUri(Id);

        imageSignature.ImageUrl = imageUri;

        upSignature.Update();
      }
    }

    /// <summary>
    ///   Биндинг прикрепленных файлов на грид
    /// </summary>
    //private void BindData2AttachedFileGridView()
    //{
    //  var _statementService = ObjectFactory.GetInstance<IStatementService>();

    //  // Добавление файлов в грид
    //  if (Session[SessionConsts.CAttachedFileKey] != null)
    //  {
    //    var attachedFilesIdList = Session[SessionConsts.CAttachedFileKey] as Guid[];
    //    var fileList =
    //      attachedFilesIdList.Select(x => new ListItem(_statementService.GetContentRecord(x).FileName, x.ToString())).
    //        ToArray();
    //    gvAttachedFiles.DataSource = fileList;
    //    gvAttachedFiles.DataBind();
    //  }
    //}

    /// <summary>
    /// Обрезает изображение по указаному размеру
    /// </summary>
    /// <param name="image">
    /// The image. 
    /// </param>
    /// <param name="x">
    /// </param>
    /// <param name="y">
    /// </param>
    /// <param name="width">
    /// </param>
    /// <param name="height">
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/> . 
    /// </returns>
    private byte[] CropImage(byte[] image, int x, int y, int width, int height)
    {
      using (var originalImageStream = new MemoryStream(image))
      {
        using (var originalImage = Image.FromStream(originalImageStream))
        {
          using (var croppedSignature = new Bitmap(width, height))
          {
            croppedSignature.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);

            using (var graphic = Graphics.FromImage(croppedSignature))
            {
              graphic.SmoothingMode = SmoothingMode.AntiAlias;
              graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
              graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

              var destRect = new Rectangle(0, 0, width, height);
              graphic.FillRectangle(new SolidBrush(Color.White), destRect);
              graphic.DrawImage(originalImage, destRect, x, y, width, height, GraphicsUnit.Pixel);

              using (var croppedImageStream = new MemoryStream())
              {
                croppedSignature.Save(croppedImageStream, originalImage.RawFormat);
                return croppedImageStream.GetBuffer();
              }
            }
          }
        }
      }
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
      string error = _service.TryCheckProperty1(CurrentStatement, Utils.GetExpressionNode(x => x.InsuredPersonData.Contents));
      args.IsValid = string.IsNullOrEmpty(error);
      CustomValidator1.ErrorMessage = error;
    }

    /// <summary>
    /// Обработчик загрузки файлов
    /// </summary>
    /// <param name="sender">
    /// </param>
    /// <param name="e">
    /// </param>
    //protected void fuFileUploader_FileUploadComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    //{
    //  var _statementService = ObjectFactory.GetInstance<IStatementService>();

    //  // запись присланного файла в базу
    //  if (fuFileUploader.HasFile)
    //  {
    //    var content = _statementService.SaveContentRecord(TypeContent.TypeContent20, fuFileUploader.FileBytes,
    //                                                     fuFileUploader.FileName);

    //    // пишем Id созданной записи в Session
    //    var attachedFilesIdList = new Guid[0];
    //    if (Session[SessionConsts.CAttachedFileKey] != null)
    //    {
    //      attachedFilesIdList = Session[SessionConsts.CAttachedFileKey] as Guid[];
    //    }

    //    Session[SessionConsts.CAttachedFileKey] = attachedFilesIdList.Concat(new[] { content.Id }).ToArray();
    //  }
    //}

  }
}