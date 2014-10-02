// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionConsts.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp
{
  /// <summary>
  /// The session consts.
  /// </summary>
  public static class SessionConsts
  {
    #region Constants

    /// <summary>
    /// Текущее заявление между шагами
    /// </summary>
    public const string CCurrentStatement = "CurrentStatement";

    public const string CExampleStatement = "ExampleStatement";

    public const string CPreviosStatementId = "PreviosStatementId";

    
    public const string CTemplateVsForPrint = "TemplateVsForPrint";

    public const string CJobsSettings = "JobsSettings";

    public const string CAssignUecCertificates = "AssignUecCertificates";

    #region 5 step

    /// <summary>
    ///   The attached photo id.
    /// </summary>
    public const string CAttachedPhotoId = "AttachedPhoto";

    /// <summary>
    ///   The attached file key.
    /// </summary>
    public const string CAttachedFileKey = "AttachedFiles";

    /// <summary>
    ///   The attached signature id.
    /// </summary>
    public const string CAttachedSignatureId = "AttachedSignature";

    #endregion


    /// <summary>
    /// The c admin permission.
    /// </summary>
    public const string CAdminPermission = "adminPermission";

    /// <summary>
    /// The c display admin menu.
    /// </summary>
    public const string CDisplayAdminMenu = "DisplayAdminMenu";

    /// <summary>
    /// The c polis end date.
    /// </summary>
    public const string CPolisEndDate = "PolisEndDate";

    ///// <summary>
    ///// The c step.
    ///// </summary>
    public const string CStep = "CurentStep";

    /// <summary>
    /// The c guid statement id.
    /// </summary>
    public const string CGuidStatementId = "GuidStatementId";

    /// <summary>
    /// The c in statement editing.
    /// </summary>
    public const string CInStatementEditing = "InStatementEditing";

    /// <summary>
    /// The c insured id.
    /// </summary>
    public const string CInsuredId = "InsuredId";


    public const string CStatementsToSeparate = "StatementsToSeparate";

    public const string CStatementsHistory = "StatementsToHistory";

    /// <summary>
    /// The c main insured id.
    /// </summary>
    public const string CMainInsuredId = "MainInsuredId";

    ///// <summary>
    ///// The c need new polis.
    ///// </summary>
    //public const string CNeedNewPolis = "NeedNewPolicy";

    ///// <summary>
    ///// The c polis number.
    ///// </summary>
    //public const string CPolisNumber = "PolisNumber";

    /// <summary>
    /// The c oko 1 gost content.
    /// </summary>
    public const string COko1GostContent = "OKO1GOSTContent";

    /// <summary>
    /// The c oko 1 rsa content.
    /// </summary>
    public const string COko1RsaContent = "OKO1RSAContent";

    /// <summary>
    /// The c operation.
    /// </summary>
    public const string COperation = "Operation";

    /// <summary>
    /// The c pdp id.
    /// </summary>
    public const string CPdpId = "pdpId";

    /// <summary>
    /// The c pdp list.
    /// </summary>
    public const string CPdpList = "pdpList";

    /// <summary>
    /// The c policy type.
    /// </summary>
    //public const string CPolicyType = "PolicyType";

    /// <summary>
    /// The c private terminal gost content.
    /// </summary>
    public const string CPrivateTerminalGostContent = "PrivateTerminalGOSTContent";

    /////// <summary>
    /////// The c statement id.
    /////// </summary>
    ////public const string CStatementId = "StatementId";

    /// <summary>
    /// The c statement status.
    /// </summary>
    public const string CStatementStatus = "StatementStatus";

    /// <summary>
    /// The c terminal gost content.
    /// </summary>
    public const string CTerminalGostContent = "TerminalGOSTContent";

    /// <summary>
    /// The c terminal rsa content.
    /// </summary>
    public const string CTerminalRsaContent = "TerminalRSAContent";

    /// <summary>
    /// The c uc 1 gost content.
    /// </summary>
    public const string CUc1GostContent = "UC1GOSTContent";

    /// <summary>
    /// The c uc 1 rsa content.
    /// </summary>
    public const string CUc1RsaContent = "UC1RSAContent";

    /// <summary>
    /// The c workstation dict.
    /// </summary>
    public const string CWorkstationDict = "workstationDict";


    /*******************************************************************************************/
    /* константы для отображения пунктов меню на основании разрешений текущего пользователя */
    /*!!!Названия не изменять (они используются на мастер форме форме в качестве значений пунктов меню)!!!*/
    /*******************************************************************************************/
    public const string CMaian = "Main";
    public const string CExportSmoBatches = "ExportSmoBatches";
    public const string CTwins = "Twins";
    public const string CSearchKeyTypes = "SearchKeyTypes";
    public const string CPfrStatistic = "PfrStatistic";
    public const string CErrorSynchronizationView = "ErrorSynchronizationView";
    public const string CTfoms = "Tfoms";
    public const string CSmos = "Smos";
    public const string CMos = "Mos";
    public const string CConcepts = "Concepts";
    public const string CFirstMiddleNames = "FirstMiddleNames";
    public const string CRangeNumbers = "RangeNumbers";
    public const string CTemplatesVs = "TemplatesVs";
    public const string CManageChecks = "ManageChecks";
    public const string CUsers = "Users";
    public const string CRoles = "Roles";
    public const string CPermissions = "Permissions";
    public const string CSchedulerTask = "SchedulerTask";
    public const string CJobsProgress = "JobsProgress";
    public const string CInstallation = "Installation";

    /**** форма поиска ****/
    public const string CReinsuranse = "Reinsuranse";
    public const string CReneval = "Reneval";
    public const string CIssue = "Issue";
    public const string CEdit = "Edit";
    public const string CDelete = "Delete";
    public const string CReadUEC = "ReadUEC";
    public const string CWriteUEC = "WriteUEC";
    public const string CReadSmartCard = "ReadSmardCard";
    public const string CSeparate = "Separate";
    public const string CInsuranceHistory = "InsuranceHistory";
    public const string CDeleteDeathInfo = "DeleteDeathInfo";

    #endregion
  }
}