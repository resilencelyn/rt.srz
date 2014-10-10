<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Step5.ascx.cs" Inherits="rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps.Step5" %>

<script src="<%= ResolveUrl("~/Scripts/webcam.js")%>" type="text/javascript"></script>
<script type="text/javascript">
  //Обрабочик окончания работы Ajax запроса
  var prm = Sys.WebForms.PageRequestManager.getInstance().add_endRequest(pageRequestManager_endRequest);

  //Добавление функционала обрезки загруженной подписи после вызова AJAX
  function pageRequestManager_endRequest(sender, args) {
    addCropSupportForSignature();
    addCropSupportForPhoto();
  }

  //Добавление функционала обрезки загруженной подписи в момент инициализации страницы
  $(document).ready(function () {
    addCropSupportForSignature();
    addCropSupportForPhoto();
  });

  //Камера
  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  //Инициализация камеры
  webcam.set_api_url('<%= ResolveUrl("~/HttpHandlers/UploadPhotoHandler.ashx")%>');
  webcam.set_swf_url('<%= ResolveUrl("~/Scripts/webcam.swf")%>');
  webcam.set_quality(100);
  webcam.set_shutter_sound(true, '<%= ResolveUrl("~/Scripts/shutter.mp3")%>');
  webcam.set_hook('onComplete', 'my_completion_handler');

  //Обработчик загрузки фотографии на сервер
  function uploadPhoto() {
    webcam.snap();
  }

  //обработчик окончания загрузки фотографии на сервер выполненого веб камерой
  function my_completion_handler(msg) {
    // показ JPEG на странице
    if (msg.match(/(http\:\/\/\S+)/)) {
      var image_url = RegExp.$1;
      $get('<%=image.ClientID%>').innerHTML = '<img src="' + image_url + '">';
      $get('<%=hfImageUrl.ClientID%>').value = image_url;
    }

    webcam.reset();
  }
  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  //Фотография
  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  //Добавление функционала обрезки загруженной подписи
  function addCropSupportForPhoto() {
    $('.imPhoto').Jcrop({
      minSize: [320, 400],  //требования к фото
      aspectRatio: 0.8,     //требования к фото
      onSelect: storePhotoCoords
    });
  }

  //Сохранение координат подписи после выбора пользователем
  function storePhotoCoords(c) {
    $get('<%=hfPhotoX.ClientID%>').value = c.x;
    $get('<%=hfPhotoY.ClientID%>').value = c.y;
    $get('<%=hfPhotoWidth.ClientID%>').value = c.w;
    $get('<%=hfPhotoHeigth.ClientID%>').value = c.h;
    if ($get('<%=btnCropPhoto.ClientID%>') != null) {
      $get('<%=btnCropPhoto.ClientID%>').disabled = false;
    }
  };

  //обработчик окончания загрузки фотографии на сервер
  function uploadPhotoCompleteHandler(sender) {

    //раздизабливаем конпки визарда
    SetWizardButtonsEnable(true);

    //Очистка поля ввода
    var asyncFileUpload = $get("<%=fuPhotoUploader.ClientID%>");
    var txts = asyncFileUpload.getElementsByTagName("input");
    var postedFilePath;
    for (var i = 0; i < txts.length; i++) {
      if (txts[i].type == "file") {
        postedFilePath = txts[i].value;
      }
    }

    //Принудительный Postback
    __doPostBack('<%=upPhoto.ClientID%>', postedFilePath);

    //блокировка кнпоки обрезать
    if ($get('<%=btnCropPhoto.ClientID%>') != null) {
      $get('<%=btnCropPhoto.ClientID%>').disabled = false;
    }
  }
  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  //Подпись
  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  //обработчик окончания загрузки файла подписи на сервер
  function uploadSignatureCompleteHandler(sender) {
    //раздизабливаем конпки визарда
    SetWizardButtonsEnable(true);

    //Очистка поля ввода
    var asyncFileUpload = $get("<%=fuSignatureUploader.ClientID%>");
    var txts = asyncFileUpload.getElementsByTagName("input");
    var postedFilePath;
    for (var i = 0; i < txts.length; i++) {
      if (txts[i].type == "file") {
        postedFilePath = txts[i].value;
      }
    }

    //Принудительный Postback
    __doPostBack('<%=upSignature.ClientID%>', postedFilePath);

    //блокировка кнпоки обрезать
    if ($get('<%=btnCropSignature.ClientID%>') != null) {
      $get('<%=btnCropSignature.ClientID%>').disabled = false;
    }
  }

  //Добавление функционала обрезки загруженной подписи
  function addCropSupportForSignature() {
    $('.imSignature').Jcrop({
      minSize: [736, 160],  //требования к подписи
      aspectRatio: 4.6,     //требования к подписи
      onSelect: storeSignatureCoords
    });
  }

  //Сохранение координат подписи после выбора пользователем
  function storeSignatureCoords(c) {
    $get('<%=hfSignatureX.ClientID%>').value = c.x;
    $get('<%=hfSignatureY.ClientID%>').value = c.y;
    $get('<%=hfSignatureWidth.ClientID%>').value = c.w;
    $get('<%=hfSignatureHeigth.ClientID%>').value = c.h;
    if ($get('<%=btnCropSignature.ClientID%>') != null) {
      $get('<%=btnCropSignature.ClientID%>').disabled = false;
    }
  };
  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  //Файл
  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  //обработчик окончания загрузки прикрепляемого файла
  //function uploadFileCompleteHandler(sender) {
    //раздизабливаем конпки визарда
    //SetWizardButtonsEnable(true);

    //Очистка поля ввода
    //var asyncFileUpload = $get("'fuFileUploader.ClientID%>");
    //var txts = asyncFileUpload.getElementsByTagName("input");
    //for (var i = 0; i < txts.length; i++) {
    //  if (txts[i].type == "file") {
    //}
    //}

    //Принудительный Postback
    //__doPostBack('upAttachedFiles.ClientID%>', '');

  //}

  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  function uploadStarted() {
    SetWizardButtonsEnable(false);
  }

  function SetWizardButtonsEnable(enable) {
    //вызов функции из StatementSelectionWizardControl
    Disable5StepButtonsDuringLoadingFile(!enable);
  }

  function onUploadError(e) {
    SetWizardButtonsEnable(true);
    alert('Во время загрузки файла произошла ошибка. Вероятная причина - размер файла превышает 50 мегабайт.');
  }
</script>

<div class="wizardDiv">

  <div>

    <asp:HiddenField ID="hfImageUrl" runat="server" />
    <asp:HiddenField ID="hfPhotoX" runat="server" />
    <asp:HiddenField ID="hfPhotoY" runat="server" />
    <asp:HiddenField ID="hfPhotoWidth" runat="server" />
    <asp:HiddenField ID="hfPhotoHeigth" runat="server" />
    <asp:HiddenField ID="hfSignatureX" runat="server" />
    <asp:HiddenField ID="hfSignatureY" runat="server" />
    <asp:HiddenField ID="hfSignatureWidth" runat="server" />
    <asp:HiddenField ID="hfSignatureHeigth" runat="server" />
    <br />
    <div class="headerTitles">
      <asp:Label ID="Label1" runat="server" Text="Добавьте копии документов или фотографию и образец подписи застрахованного лица"></asp:Label>
    </div>
    <div id="divElectronicPolicy" runat="server">
      <div class="headerSubTitles">
        <asp:Label ID="Label2" runat="server" Text="Фотография" />
      </div>
      <div id="divPhotoWebCam" style="display: inline-block" runat="server">
        <table>
          <tr>
            <td></td>
          </tr>
          <tr>
            <td style="width: 320px; height: 400px; border: 1px;">
              <script type="text/javascript">
                document.write(webcam.get_html(320, 400));
              </script>
            </td>
            <td>
              <input type="button" value="Сделать снимок" onclick="uploadPhoto()" />
            </td>
            <td style="width: 320px; height: 400px; border: 1px;">
              <div id="image" runat="server" style="width: 320px; height: 400px; border: 1px groove #000000;">
              </div>
            </td>
          </tr>
        </table>
      </div>
      <div id="divPhotoUpload" runat="server">
        <asp:UpdatePanel ID="upPhotoUpload" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

            <div style="margin-right: auto; margin: auto; display: table">
              <div style="clear: both">
                <div style="float: left;">
                  <div style="padding: 5px">
                    <ajaxToolkit:AsyncFileUpload runat="server" ID="fuPhotoUploader" Width="400px" ThrobberID="photoUploadLoader"
                      CompleteBackColor="White" UploadingBackColor="#CCFFFF" OnUploadedComplete="fuPhotoUploader_PhotoUploadComplete" OnClientUploadStarted="uploadStarted"
                      OnClientUploadComplete="uploadPhotoCompleteHandler" OnClientUploadError="onUploadError" CssClass="controlBoxes" />
                  </div>
                </div>
                <div style="float: left;">
                  <div style="padding: 5px">
                    <asp:Button ID="btnLoadEmptyPhoto" runat="server" Text="Загрузить пустую фотографию" OnClick="btnLoadEmptyPhoto_Click" Width="200px" UseSubmitBehavior="false" CausesValidation="false" CssClass="buttons" />
                  </div>
                </div>
              </div>
              <div style="clear: both">
                <asp:Image ID="photoUploadLoader" runat="server" ImageUrl="../../Resources/ajax-loader.gif" />
              </div>
            </div>
          </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="upPhoto" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
            <div id="divImagePhoto" runat="server">

              <div style="padding: 5px; margin-right: auto; margin: auto; display: table">
                <div style="clear: both">
                  <div style="padding: 5px; text-align: left">
                    <asp:Image ID="imagePhoto" runat="server" CssClass="imPhoto" />
                  </div>
                </div>
              </div>

              <div style="padding: 5px; margin-right: auto; margin: auto; display: table">
                <div style="clear: both">
                  <div style="float: left; width: 100%; text-align: left">
                    <div style="padding: 5px">
                      <asp:Button ID="btnCropPhoto" runat="server" Text="Обрезать фотографию" OnClick="btnCropPhoto_Click" CssClass="buttons" />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>

      <hr />

      <div>
        <div class="headerSubTitles">
          <asp:Label ID="Label3" runat="server" Text="Прикрепление подписи" />
        </div>

        <asp:UpdatePanel ID="upSignatureUpload" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

            <div style="margin-right: auto; margin: auto; display: table">
              <div style="clear: both">
                <div style="float: left;">
                  <div style="padding: 5px">
                    <ajaxToolkit:AsyncFileUpload runat="server" ID="fuSignatureUploader" Width="400px" ThrobberID="imageSignatureUploadLoader"
                      CompleteBackColor="White" UploadingBackColor="#CCFFFF" OnUploadedComplete="fuSignatureUploader_SignatureUploadComplete" OnClientUploadStarted="uploadStarted"
                      OnClientUploadComplete="uploadSignatureCompleteHandler" OnClientUploadError="onUploadError" CssClass="controlBoxes" />
                  </div>
                </div>
                <div style="float: left;">
                  <div style="padding: 5px">
                    <asp:Button ID="btnLoadEmptySign" runat="server" Text="Загрузить пустую подпись" OnClick="btnLoadEmptySign_Click" Width="200px" UseSubmitBehavior="false" CausesValidation="false" CssClass="buttons" />
                  </div>
                </div>
              </div>
              <div style="clear: both">
                <asp:Image ID="imageSignatureUploadLoader" runat="server" ImageUrl="../../Resources/ajax-loader.gif" />
              </div>
            </div>
          </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="upSignature" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

            <div id="divImageSignature" runat="server">

              <div style="padding: 5px; margin-right: auto; margin: auto; display: table">
                <div style="padding: 5px; text-align: left">
                  <asp:Image ID="imageSignature" runat="server" CssClass="imSignature" />
                </div>
              </div>

              <div style="padding: 5px; margin-right: auto; margin: auto; display: table">
                <div style="clear: both">
                  <div style="float: left; width: 100%; text-align: left">
                    <div style="padding: 5px">
                      <asp:Button ID="btnCropSignature" runat="server" Text="Обрезать подпись" OnClick="btnCropSignature_Click" CssClass="buttons" />
                    </div>
                  </div>
                </div>
              </div>

            </div>

          </ContentTemplate>
        </asp:UpdatePanel>
      </div>

      <hr />
    </div>


    <%--**********************************************************закоментированы документы списком*************************************************************--%>


<%--    <div id="divFileUploadTitle" runat="server" class="headerTitles">
      <asp:Label ID="lbFilesUploadTitle" runat="server" Text="Добавьте копии документов застрахованного лица" />
    </div>

    <div class="headerSubTitles">
      <asp:Label ID="Label4" runat="server" Text="Прикрепление файлов" />
    </div>



    <asp:UpdatePanel ID="upFileUpload" runat="server" UpdateMode="Conditional">
      <ContentTemplate>
        <div style="margin-right: auto; margin: auto; display: table">
          <div style="clear: both">
            <div style="float: left;">
              <div style="padding: 5px">
                <ajaxToolkit:AsyncFileUpload runat="server" ID="fuFileUploader" Width="400px" UploaderStyle="Traditional" ThrobberID="imageFileUploadLoader"
                  CompleteBackColor="White" UploadingBackColor="#CCFFFF" OnUploadedComplete="fuFileUploader_FileUploadComplete" OnClientUploadStarted="uploadStarted"
                  OnClientUploadComplete="uploadFileCompleteHandler" OnClientUploadError="onUploadError" CssClass="controlBoxes" />
              </div>
            </div>
          </div>
          <div style="float: left;">
            <div style="padding: 5px; width: 200px">
            </div>
          </div>
        </div>
        <div style="clear: both">
          <asp:Image ID="imageFileUploadLoader" runat="server" ImageUrl="../../Resources/ajax-loader.gif" />
        </div>
        </div>
      </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="upAttachedFiles" runat="server">
      <ContentTemplate>
        <div style="padding: 5px">
          <asp:GridView Width="100%" ID="gvAttachedFiles" runat="server" AutoGenerateColumns="false" EmptyDataText="Нет прикрепленных файлов">
            <Columns>
              <asp:BoundField DataField="Text" HeaderText="Имя прикрепленного файла" />
              <asp:TemplateField>
                <ItemTemplate>
                  <asp:LinkButton ID="lnkDelete" Text="Удалить" CommandArgument='<%# Eval("Value") %>'
                    runat="server" OnClick="gvAttachedFiles_DeleteFile" />
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
                <ItemTemplate>
                  <asp:HyperLink runat="server" Text="Просмотреть" NavigateUrl='<%# string.Format("~/HttpHandlers/GetFileHandler.ashx?fileid={0}",  Eval("Value"))%>' />
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>
          </asp:GridView>
        </div>
      </ContentTemplate>
    </asp:UpdatePanel>
--%>
    <div class="errorMessage">
      <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
    </div>

  </div>
</div>
