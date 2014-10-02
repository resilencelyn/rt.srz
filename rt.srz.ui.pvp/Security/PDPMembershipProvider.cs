// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PDPMembershipProvider.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Security
{
  using System;
  using System.Configuration;
  using System.Configuration.Provider;
  using System.Security.Authentication;
  using System.Web.Configuration;
  using System.Web.Security;

  using rt.core.model.security;
  using rt.srz.model.interfaces.service;

  using StructureMap;

  /// <summary>
  /// The pdp membership provider.
  /// </summary>
  public class PDPMembershipProvider : MembershipProvider
  {
    #region Constants

    #endregion

    #region Fields

    /// <summary>
    /// The _provider settings.
    /// </summary>
    private readonly ProviderSettings providerSettings;

    /// <summary>
    /// The _security service.
    /// </summary>
    private readonly ISecurityService securityService;

    /// <summary>
    /// The auth service.
    /// </summary>
    private readonly IAuthService authService;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="PDPMembershipProvider"/> class.
    /// </summary>
    public PDPMembershipProvider()
    {
      securityService = ObjectFactory.GetInstance<ISecurityService>();
      authService = ObjectFactory.GetInstance<IAuthService>();

      var membershipSection = (MembershipSection)WebConfigurationManager.GetSection("system.web/membership");
      providerSettings = membershipSection.Providers[membershipSection.DefaultProvider];
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the application name.
    /// </summary>
    public override string ApplicationName
    {
      get
      {
        return providerSettings.Parameters["applicationName"];
      }

      set
      {
        providerSettings.Parameters["applicationName"] = value;
      }
    }

    /// <summary>
    /// Gets a value indicating whether enable password reset.
    /// </summary>
    public override bool EnablePasswordReset
    {
      get
      {
        return bool.Parse(providerSettings.Parameters["enablePasswordReset"]);
      }
    }

    /// <summary>
    /// Gets a value indicating whether enable password retrieval.
    /// </summary>
    public override bool EnablePasswordRetrieval
    {
      get
      {
        return bool.Parse(providerSettings.Parameters["enablePasswordRetrieval"]);
      }
    }

    /// <summary>
    /// Gets the max invalid password attempts.
    /// </summary>
    public override int MaxInvalidPasswordAttempts
    {
      get
      {
        return int.Parse(providerSettings.Parameters["maxInvalidPasswordAttempts"]);
      }
    }

    /// <summary>
    /// Gets the min required non alphanumeric characters.
    /// </summary>
    public override int MinRequiredNonAlphanumericCharacters
    {
      get
      {
        return int.Parse(providerSettings.Parameters["minRequiredNonalphanumericCharacters"]);
      }
    }

    /// <summary>
    /// Gets the min required password length.
    /// </summary>
    public override int MinRequiredPasswordLength
    {
      get
      {
        return int.Parse(providerSettings.Parameters["minRequiredPasswordLength"]);
      }
    }

    /// <summary>
    /// Gets the password attempt window.
    /// </summary>
    public override int PasswordAttemptWindow
    {
      get
      {
        return int.Parse(providerSettings.Parameters["passwordAttemptWindow"]);
      }
    }

    /// <summary>
    /// Gets the password format.
    /// </summary>
    public override MembershipPasswordFormat PasswordFormat
    {
      get
      {
        return MembershipPasswordFormat.Clear;
      }
    }

    /// <summary>
    /// Gets the password strength regular expression.
    /// </summary>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override string PasswordStrengthRegularExpression
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    /// Gets a value indicating whether requires question and answer.
    /// </summary>
    public override bool RequiresQuestionAndAnswer
    {
      get
      {
        return bool.Parse(providerSettings.Parameters["requiresQuestionAndAnswer"]);
      }
    }

    /// <summary>
    /// Gets a value indicating whether requires unique email.
    /// </summary>
    public override bool RequiresUniqueEmail
    {
      get
      {
        return bool.Parse(providerSettings.Parameters["requiresUniqueEmail"]);
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The change password.
    /// </summary>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="oldPassword">
    /// The old password.
    /// </param>
    /// <param name="newPassword">
    /// The new password.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    /// <exception cref="Exception">
    /// </exception>
    /// <exception cref="MembershipPasswordException">
    /// </exception>
    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
      if (newPassword.Length < MinRequiredPasswordLength)
      {
        return false;
      }

      if (!ValidateUser(username, oldPassword))
      {
        return false;
      }

      var args = new ValidatePasswordEventArgs(username, newPassword, true);

      OnValidatingPassword(args);

      if (args.Cancel)
      {
        if (args.FailureInformation != null)
        {
          throw args.FailureInformation;
        }
        else
        {
          throw new MembershipPasswordException("Изменение пароля отменено из-за ошибки проверки пароля.");
        }
      }

      if (securityService.UpdatePassword(username, newPassword) != null)
      {
        return true;
      }

      return false;
    }

    /// <summary>
    /// The change password question and answer.
    /// </summary>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="password">
    /// The password.
    /// </param>
    /// <param name="newPasswordQuestion">
    /// The new password question.
    /// </param>
    /// <param name="newPasswordAnswer">
    /// The new password answer.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override bool ChangePasswordQuestionAndAnswer(
      string username, 
      string password, 
      string newPasswordQuestion, 
      string newPasswordAnswer)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// The create user.
    /// </summary>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="password">
    /// The password.
    /// </param>
    /// <param name="email">
    /// The email.
    /// </param>
    /// <param name="passwordQuestion">
    /// The password question.
    /// </param>
    /// <param name="passwordAnswer">
    /// The password answer.
    /// </param>
    /// <param name="isApproved">
    /// The is approved.
    /// </param>
    /// <param name="providerUserKey">
    /// The provider user key.
    /// </param>
    /// <param name="status">
    /// The status.
    /// </param>
    /// <returns>
    /// The <see cref="MembershipUser"/>.
    /// </returns>
    public override MembershipUser CreateUser(
      string username, 
      string password, 
      string email, 
      string passwordQuestion, 
      string passwordAnswer, 
      bool isApproved, 
      object providerUserKey, 
      out MembershipCreateStatus status)
    {
      status = MembershipCreateStatus.ProviderError;
      return null;
    }

    /// <summary>
    /// The delete user.
    /// </summary>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="deleteAllRelatedData">
    /// The delete all related data.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// The find users by email.
    /// </summary>
    /// <param name="emailToMatch">
    /// The email to match.
    /// </param>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <param name="totalRecords">
    /// The total records.
    /// </param>
    /// <returns>
    /// The <see cref="MembershipUserCollection"/>.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override MembershipUserCollection FindUsersByEmail(
      string emailToMatch, 
      int pageIndex, 
      int pageSize, 
      out int totalRecords)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// The find users by name.
    /// </summary>
    /// <param name="usernameToMatch">
    /// The username to match.
    /// </param>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <param name="totalRecords">
    /// The total records.
    /// </param>
    /// <returns>
    /// The <see cref="MembershipUserCollection"/>.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override MembershipUserCollection FindUsersByName(
      string usernameToMatch, 
      int pageIndex, 
      int pageSize, 
      out int totalRecords)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// The get all users.
    /// </summary>
    /// <param name="pageIndex">
    /// The page index.
    /// </param>
    /// <param name="pageSize">
    /// The page size.
    /// </param>
    /// <param name="totalRecords">
    /// The total records.
    /// </param>
    /// <returns>
    /// The <see cref="MembershipUserCollection"/>.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// The get number of users online.
    /// </summary>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override int GetNumberOfUsersOnline()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// The get password.
    /// </summary>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="answer">
    /// The answer.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override string GetPassword(string username, string answer)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// The get user.
    /// </summary>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="userIsOnline">
    /// The user is online.
    /// </param>
    /// <returns>
    /// The <see cref="MembershipUser"/>.
    /// </returns>
    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
      var user = securityService.GetUserByName(username);

      if (user == null)
      {
        return null;
      }

      return new MembershipUser(
        Name, 
        user.Login, 
        null, 
        user.Email, 
        string.Empty, 
        string.Empty, 
        true, 
        false, 
        user.CreationDate, 
        user.LastLoginDate, 
        DateTime.MinValue, 
        DateTime.MinValue, 
        DateTime.MinValue);
    }

    /// <summary>
    /// The get user.
    /// </summary>
    /// <param name="providerUserKey">
    /// The provider user key.
    /// </param>
    /// <param name="userIsOnline">
    /// The user is online.
    /// </param>
    /// <returns>
    /// The <see cref="MembershipUser"/>.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// The get user name by email.
    /// </summary>
    /// <param name="email">
    /// The email.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    /// <exception cref="ProviderException">
    /// </exception>
    public override string GetUserNameByEmail(string email)
    {
      try
      {
        return securityService.GetUserNameByEmail(email);
      }
      catch (Exception e)
      {
        throw new ProviderException(e.Message);
      }
    }

    /// <summary>
    /// The reset password.
    /// </summary>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="answer">
    /// The answer.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override string ResetPassword(string username, string answer)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// The unlock user.
    /// </summary>
    /// <param name="userName">
    /// The user name.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override bool UnlockUser(string userName)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// The update user.
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override void UpdateUser(MembershipUser user)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// The validate user.
    /// </summary>
    /// <param name="username">
    /// The username.
    /// </param>
    /// <param name="password">
    /// The password.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool ValidateUser(string username, string password)
    {
      try
      {
        authService.Authenticate(username, password);

        return true;
      }
      catch (AuthenticationException)
      {
        return false;
      }
    }

    #endregion
  }
}