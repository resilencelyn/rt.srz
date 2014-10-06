// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataEventTypes.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The data event types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.enumerations.resolve
{
  #region references

  using System;

  using rt.srz.model.HL7.dotNetX;

  #endregion

  /// <summary>
  ///   The data event types.
  /// </summary>
  [CLSCompliant(false)]
  public static class DataEventTypes
  {
    #region Public Methods and Operators

    /// <summary>
    /// The retrieve data event type.
    /// </summary>
    /// <param name="transactionCode">
    /// The transaction code.
    /// </param>
    /// <param name="transcauseCode">
    /// The transcause code.
    /// </param>
    /// <returns>
    /// The <see cref="DataEventType"/>.
    /// </returns>
    public static DataEventType RetrieveDataEventType(string transactionCode, string transcauseCode)
    {
      switch (TStringHelper.StringToEmpty(transactionCode))
      {
        case "ZA1":
          string str2;
          if (((str2 = TStringHelper.StringToEmpty(transcauseCode)) == null) || !(str2 == "301"))
          {
            break;
          }

          return DataEventType.IssueOrReissuancePolicyAnnounce;

        case "ZA7":
          return DataEventType.NotificationOfReceiveAnnounce;

        case "A04":
          string str3;
          if (((str3 = TStringHelper.StringToEmpty(transcauseCode)) == null) || !(str3 == "П01"))
          {
            break;
          }

          return DataEventType.IssuePolicy;

        case "A08":
          switch (TStringHelper.StringToEmpty(transcauseCode))
          {
            case "П01":
              return DataEventType.IssuePolicy;

            case "П02":
              return DataEventType.DeregisterPolicy;

            case "П03":
              return DataEventType.RegisterPolicy;

            case "П04":
              return DataEventType.ChangePolicyPartial;

            case "П05":
              return DataEventType.ChangePolicyEmployment;

            case "П06":
              return DataEventType.ChangePolicyComplete;
          }

          break;

        case "A03":
          string str5;
          if (((str5 = TStringHelper.StringToEmpty(transcauseCode)) == null) || !(str5 == "П07"))
          {
            break;
          }

          return DataEventType.DecommitPolicyENP;

        case "A13":
          string str6;
          if (((str6 = TStringHelper.StringToEmpty(transcauseCode)) == null) || !(str6 == "П09"))
          {
            break;
          }

          return DataEventType.RecommitPolicyENP;

        case "A24":
          string str7;
          if (((str7 = TStringHelper.StringToEmpty(transcauseCode)) == null) || !(str7 == "П10"))
          {
            break;
          }

          return DataEventType.ResolvePolicyDublicates;

        case "A37":
          string str8;
          if (((str8 = TStringHelper.StringToEmpty(transcauseCode)) == null) || !(str8 == "П11"))
          {
            break;
          }

          return DataEventType.ResolvePolicyCollisions;

        case "ZP1":
          return DataEventType.QueryPersonInsurance;

        case "ZP2":
          return DataEventType.QueryPersonsRegistrating;

        case "ZP3":
          return DataEventType.QueryPersonsDeregistrating;

        case "ZP4":
          return DataEventType.QueryPersonsDeadAbroad;

        case "ZP5":
          return DataEventType.QueryDublicates;
      }

      return DataEventType.Undefined;
    }

    /// <summary>
    /// The retrieve reply code.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveReplyCode(DataEventType type)
    {
      switch (type)
      {
        case DataEventType.DeregisterPolicy:
        case DataEventType.RegisterPolicy:
        case DataEventType.IssuePolicy:
        case DataEventType.ChangePolicyPartial:
        case DataEventType.ChangePolicyEmployment:
        case DataEventType.ChangePolicyComplete:
        case DataEventType.DecommitPolicyENP:
        case DataEventType.RecommitPolicyENP:
        case DataEventType.ResolvePolicyDublicates:
        case DataEventType.ResolvePolicyCollisions:
        case DataEventType.IssueOrReissuancePolicyAnnounce:
        case DataEventType.NotificationOfReceiveAnnounce:
          return "ACK";

        case DataEventType.BatchProcess:
          return "ACK";

        case DataEventType.QueryPersonInsurance:
        case DataEventType.QueryPersonsRegistrating:
        case DataEventType.QueryPersonsDeregistrating:
        case DataEventType.QueryPersonsDeadAbroad:
        case DataEventType.QueryDublicates:
          return "RSP";
      }

      return null;
    }

    /// <summary>
    /// The retrieve reply generals.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="transactionReply">
    /// The transaction reply.
    /// </param>
    /// <param name="replyCode">
    /// The reply code.
    /// </param>
    /// <param name="replyStruct">
    /// The reply struct.
    /// </param>
    public static void RetrieveReplyGenerals(
      DataEventType type, 
      out string transactionReply, 
      out string replyCode, 
      out string replyStruct)
    {
      transactionReply = RetrieveTransactionReply(type);
      replyCode = RetrieveReplyCode(type);
      replyStruct = RetrieveReplyStruct(type);
    }

    /// <summary>
    /// The retrieve reply struct.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveReplyStruct(DataEventType type)
    {
      var delimiter = "_";
      return TStringHelper.CombineStrings(RetrieveReplyCode(type), RetrieveReplyStructCode(type), delimiter);
    }

    /// <summary>
    /// The retrieve reply struct code.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveReplyStructCode(DataEventType type)
    {
      switch (type)
      {
        case DataEventType.QueryPersonsDeregistrating:
        case DataEventType.QueryPersonsRegistrating:
          return "ZK2";

        case DataEventType.QueryPersonsDeadAbroad:
          return "ZK4";

        case DataEventType.QueryDublicates:
          return "ZK5";

        case DataEventType.QueryPersonInsurance:
          return "ZK1";
      }

      return null;
    }

    /// <summary>
    /// The retrieve request code.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveRequestCode(DataEventType type)
    {
      switch (type)
      {
        case DataEventType.RegisterPolicy:
        case DataEventType.ChangePolicyPartial:
        case DataEventType.IssuePolicy:
        case DataEventType.DeregisterPolicy:
        case DataEventType.ChangePolicyEmployment:
        case DataEventType.ChangePolicyComplete:
        case DataEventType.DecommitPolicyENP:
        case DataEventType.RecommitPolicyENP:
        case DataEventType.ResolvePolicyDublicates:
        case DataEventType.ResolvePolicyCollisions:
          return "ADT";

        case DataEventType.QueryPersonInsurance:
        case DataEventType.QueryPersonsRegistrating:
        case DataEventType.QueryPersonsDeregistrating:
        case DataEventType.QueryPersonsDeadAbroad:
        case DataEventType.QueryDublicates:
          return "QBP";

        case DataEventType.IssueOrReissuancePolicyAnnounce:
        case DataEventType.NotificationOfReceiveAnnounce:
          return "ZPI";
      }

      return null;
    }

    /// <summary>
    /// The retrieve request name.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveRequestName(DataEventType type)
    {
      switch (type)
      {
        case DataEventType.QueryPersonInsurance:
          return "СП";

        case DataEventType.QueryPersonsRegistrating:
          return "ВСТ";

        case DataEventType.QueryPersonsDeregistrating:
          return "СНТ";

        case DataEventType.QueryPersonsDeadAbroad:
          return "УМ";

        case DataEventType.QueryDublicates:
          return "КДБ";
      }

      return null;
    }

    /// <summary>
    /// The retrieve request string.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveRequestString(DataEventType type)
    {
      var str = RetrieveTransactionCode(type);
      if (string.IsNullOrEmpty(str))
      {
        return null;
      }

      var delimiter = "/";
      return TStringHelper.CombineStrings(str, RetrieveTranscauseCode(type), delimiter);
    }

    /// <summary>
    /// The retrieve request struct.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveRequestStruct(DataEventType type)
    {
      var delimiter = "_";
      return TStringHelper.CombineStrings(RetrieveRequestCode(type), RetrieveRequestStructCode(type), delimiter);
    }

    /// <summary>
    /// The retrieve request struct code.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveRequestStructCode(DataEventType type)
    {
      switch (type)
      {
        case DataEventType.RegisterPolicy:
        case DataEventType.ChangePolicyPartial:
        case DataEventType.IssuePolicy:
        case DataEventType.DeregisterPolicy:
        case DataEventType.ChangePolicyEmployment:
        case DataEventType.ChangePolicyComplete:
          return "A01";

        case DataEventType.DecommitPolicyENP:
          return "A03";

        case DataEventType.RecommitPolicyENP:
          return "A01";

        case DataEventType.QueryPersonInsurance:
        case DataEventType.QueryPersonsDeadAbroad:
          return RetrieveTransactionCode(type);

        case DataEventType.QueryPersonsRegistrating:
        case DataEventType.QueryPersonsDeregistrating:
        case DataEventType.QueryDublicates:
          return "ZP2";

        case DataEventType.ResolvePolicyDublicates:
          return "A24";

        case DataEventType.ResolvePolicyCollisions:
          return "A37";

        case DataEventType.IssueOrReissuancePolicyAnnounce:
          return "ZA1";

        case DataEventType.NotificationOfReceiveAnnounce:
          return "ZA7";
      }

      return null;
    }

    /// <summary>
    /// The retrieve transaction code.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveTransactionCode(DataEventType type)
    {
      switch (type)
      {
        case DataEventType.DeregisterPolicy:
        case DataEventType.RegisterPolicy:
        case DataEventType.IssuePolicy:
        case DataEventType.ChangePolicyPartial:
        case DataEventType.ChangePolicyEmployment:
        case DataEventType.ChangePolicyComplete:
          return "A08";

        case DataEventType.BatchProcess:
          return "?";

        case DataEventType.DecommitPolicyENP:
          return "A03";

        case DataEventType.RecommitPolicyENP:
          return "A13";

        case DataEventType.QueryPersonInsurance:
          return "ZP1";

        case DataEventType.QueryPersonsRegistrating:
          return "ZP2";

        case DataEventType.ResolvePolicyDublicates:
          return "A24";

        case DataEventType.ResolvePolicyCollisions:
          return "A37";

        case DataEventType.QueryPersonsDeregistrating:
          return "ZP3";

        case DataEventType.QueryPersonsDeadAbroad:
          return "ZP4";

        case DataEventType.QueryDublicates:
          return "ZP5";

        case DataEventType.IssueOrReissuancePolicyAnnounce:
          return "ZA1";

        case DataEventType.NotificationOfReceiveAnnounce:
          return "ZA7";
      }

      return null;
    }

    /// <summary>
    /// The retrieve transaction reply.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveTransactionReply(DataEventType type)
    {
      switch (type)
      {
        case DataEventType.QueryPersonInsurance:
          return "ZK1";

        case DataEventType.QueryPersonsRegistrating:
          return "ZK2";

        case DataEventType.QueryPersonsDeregistrating:
          return "ZK3";

        case DataEventType.QueryPersonsDeadAbroad:
          return "ZK4";

        case DataEventType.QueryDublicates:
          return "ZK5";
      }

      return RetrieveTransactionCode(type);
    }

    /// <summary>
    /// The retrieve transcause code.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveTranscauseCode(DataEventType type)
    {
      switch (type)
      {
        case DataEventType.RegisterPolicy:
          return "П03";

        case DataEventType.ChangePolicyPartial:
          return "П04";

        case DataEventType.ChangePolicyEmployment:
          return "П05";

        case DataEventType.IssuePolicy:
          return "П01";

        case DataEventType.DeregisterPolicy:
          return "П02";

        case DataEventType.ChangePolicyComplete:
          return "П06";

        case DataEventType.DecommitPolicyENP:
          return "П07";

        case DataEventType.RecommitPolicyENP:
          return "П09";

        case DataEventType.ResolvePolicyDublicates:
          return "П10";

        case DataEventType.ResolvePolicyCollisions:
          return "П11";

        case DataEventType.IssueOrReissuancePolicyAnnounce:
          return "301";
      }

      return null;
    }

    /// <summary>
    /// The retrieve transcause reply.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveTranscauseReply(DataEventType type)
    {
      return RetrieveTranscauseCode(type);
    }

    #endregion
  }
}