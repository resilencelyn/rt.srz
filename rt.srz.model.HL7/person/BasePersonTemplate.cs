// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasePersonTemplate.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The base person template.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.dotNetX;
  using rt.srz.model.HL7.person.messages;
  using rt.srz.model.HL7.person.requests;
  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   The base person template.
  /// </summary>
  [Serializable]
  public class BasePersonTemplate
  {
    ///// <summary>
    /////   The ack list.
    ///// </summary>
    #region Fields

    /// <summary>
    /// The ack list.
    /// </summary>
    [XmlElement(ElementName = "ACK", Order = 2)]
    public List<Ack> AckList;

    /// <summary>
    ///   The adt_ a 01.
    /// </summary>
    [XmlElement(ElementName = "ADT_A01", Order = 3)]
    public List<ADT_A01> Adt_A01;

    ///// <summary>
    /////   The adt_ a 03.
    ///// </summary>
    // [XmlElement(ElementName = "ADT_A03", Order = 3)]
    // public List<ADT_A03> Adt_A03;

    ///// <summary>
    /////   The adt_ a 24.
    ///// </summary>
    // [XmlElement(ElementName = "ADT_A24", Order = 4)]
    // public List<ADT_A24> Adt_A24;

    ///// <summary>
    /////   The adt_ a 37.
    ///// </summary>
    // [XmlElement(ElementName = "ADT_A37", Order = 5)]
    // public List<ADT_A37> Adt_A37;

    /// <summary>
    ///   The begin NsiMoPacket.
    /// </summary>
    [XmlElement(ElementName = "BHS", Order = 1)]
    public BHS BeginPacket = new BHS();

    /// <summary>
    ///   The end NsiMoPacket.
    /// </summary>
    [XmlElement(ElementName = "BTS", Order = 20)]
    public BTS EndPacket = new BTS();

    #endregion

    ///// <summary>
    /////   The qbp_ zp 1.
    ///// </summary>
    // [XmlElement(ElementName = "QBP_ZP1", Order = 6)]
    // public List<QBP_ZP1> Qbp_Zp1;

    ///// <summary>
    /////   The qbp_ zp 2.
    ///// </summary>
    // [XmlElement(ElementName = "QBP_ZP2", Order = 7)]
    // public List<QBP_ZP2> Qbp_Zp2;

    ///// <summary>
    /////   The qbp_ zp 4.
    ///// </summary>
    // [XmlElement(ElementName = "QBP_ZP4", Order = 8)]
    // public List<QBP_ZP4> Qbp_Zp4;

    ///// <summary>
    /////   The qbp_ zp 6.
    ///// </summary>
    // [XmlElement(ElementName = "QBP_ZP6", Order = 9)]
    // public List<QBP_ZP6> Qbp_Zp6;

    ///// <summary>
    /////   The qbp_ zp 9.
    ///// </summary>
    // [XmlElement(ElementName = "QBP_ZP9", Order = 10)]
    // public List<QBP_ZP9> Qbp_Zp9;

    ///// <summary>
    /////   The rsp_ zk 1.
    ///// </summary>
    // [XmlElement(ElementName = "RSP_ZK1", Order = 11)]
    // public List<RSP_ZK1> Rsp_Zk1;

    ///// <summary>
    /////   The rsp_ zk 2.
    ///// </summary>
    // [XmlElement(ElementName = "RSP_ZK2", Order = 12)]
    // public List<RSP_ZK2> Rsp_Zk2;

    ///// <summary>
    /////   The rsp_ zk 4.
    ///// </summary>
    // [XmlElement(ElementName = "RSP_ZK4", Order = 13)]
    // public List<RSP_ZK4> Rsp_Zk4;

    ///// <summary>
    /////   The rsp_ zk 5.
    ///// </summary>
    // [XmlElement(ElementName = "RSP_ZK5", Order = 14)]
    // public List<RSP_ZK5> Rsp_Zk5;

    ///// <summary>
    /////   The rsp_ zk 6.
    ///// </summary>
    // [XmlElement(ElementName = "RSP_ZK6", Order = 15)]
    // public List<RSP_ZK6> Rsp_Zk6;

    ///// <summary>
    /////   The rsp_ zk 9.
    ///// </summary>
    // [XmlElement(ElementName = "RSP_ZK9", Order = 16)]
    // public List<RSP_ZK9> Rsp_Zk9;

    ///// <summary>
    /////   The zpi zwis.
    ///// </summary>
    // [XmlElement(ElementName = "ZPI_ZWI", Order = 17)]
    // public List<ZpiZwi> ZpiZwis;

    ///// <summary>
    /////   The zpi_za 1.
    ///// </summary>
    // [XmlElement(ElementName = "ZPI_ZA1", Order = 18)]
    // public List<ZPI_ZA1> Zpi_za1;

    ///// <summary>
    /////   The zpi_za 7.
    ///// </summary>
    // [XmlElement(ElementName = "ZPI_ZA7", Order = 19)]
    // public List<ZPI_ZA7> Zpi_za7;
    #region Public Methods and Operators

    /// <summary>
    /// The node type by name.
    /// </summary>
    /// <param name="nodeName">
    /// The node name.
    /// </param>
    /// <returns>
    /// The <see cref="Type"/>.
    /// </returns>
    public static Type NodeTypeByName(string nodeName)
    {
      switch (nodeName)
      {
        case "BHS":
          return typeof(BHS);

        case "BTS":
          return typeof(BTS);

        case "ADT_A01":
          return typeof(ADT_A01);

        case "ADT_A03":
          return typeof(ADT_A03);

        case "ADT_A24":
          return typeof(ADT_A24);

        case "ADT_A37":
          return typeof(ADT_A37);

        case "QBP_ZP1":
          return typeof(QBP_ZP1);

        case "QBP_ZP2":
          return typeof(QBP_ZP2);

        case "QBP_ZP4":
          return typeof(QBP_ZP4);

        case "ZPI_ZA1":
          return typeof(ZPI_ZA1);

        case "ZPI_ZA7":
          return typeof(ZPI_ZA7);
      }

      return null;
    }

    ///// <summary>
    ///// The add node.
    ///// </summary>
    ///// <param name="node">
    ///// The node.
    ///// </param>
    ///// <returns>
    ///// The <see cref="bool"/>.
    ///// </returns>
    // public bool AddNode(object node)
    // {
    // var bhs = node as BHS;
    // if (bhs != null)
    // {
    // BeginPacket = bhs;
    // return true;
    // }

    // var adt_a = node as ADT_A01;
    // if (adt_a != null)
    // {
    // CollectionHelper.ListWriteValue(ref Adt_A01, adt_a);
    // return true;
    // }

    // var adt_a2 = node as ADT_A03;
    // if (adt_a2 != null)
    // {
    // CollectionHelper.ListWriteValue(ref Adt_A03, adt_a2);
    // return true;
    // }

    // var adt_a3 = node as ADT_A24;
    // if (adt_a3 != null)
    // {
    // CollectionHelper.ListWriteValue(ref Adt_A24, adt_a3);
    // return true;
    // }

    // var adt_a4 = node as ADT_A37;
    // if (adt_a4 != null)
    // {
    // CollectionHelper.ListWriteValue(ref Adt_A37, adt_a4);
    // return true;
    // }

    // var qbp_zp = node as QBP_ZP1;
    // if (qbp_zp != null)
    // {
    // CollectionHelper.ListWriteValue(ref Qbp_Zp1, qbp_zp);
    // return true;
    // }

    // var qbp_zp2 = node as QBP_ZP2;
    // if (qbp_zp2 != null)
    // {
    // CollectionHelper.ListWriteValue(ref Qbp_Zp2, qbp_zp2);
    // return true;
    // }

    // var qbp_zp3 = node as QBP_ZP4;
    // if (qbp_zp3 != null)
    // {
    // CollectionHelper.ListWriteValue(ref Qbp_Zp4, qbp_zp3);
    // return true;
    // }

    // var qbp_zp6 = node as QBP_ZP6;
    // if (qbp_zp6 != null)
    // {
    // CollectionHelper.ListWriteValue(ref Qbp_Zp6, qbp_zp6);
    // return true;
    // }

    // var qbp_zp9 = node as QBP_ZP9;
    // if (qbp_zp9 != null)
    // {
    // CollectionHelper.ListWriteValue(ref Qbp_Zp9, qbp_zp9);
    // return true;
    // }

    // var zpi_za = node as ZPI_ZA1;
    // if (zpi_za != null)
    // {
    // CollectionHelper.ListWriteValue(ref Zpi_za1, zpi_za);
    // return true;
    // }

    // var zpi_za2 = node as ZPI_ZA7;
    // if (zpi_za2 != null)
    // {
    // CollectionHelper.ListWriteValue(ref Zpi_za7, zpi_za2);
    // return true;
    // }

    // var rsp_zk = node as RSP_ZK1;
    // if (rsp_zk != null)
    // {
    // CollectionHelper.ListWriteValue(ref Rsp_Zk1, rsp_zk);
    // return true;
    // }

    // var rsp_zk2 = node as RSP_ZK2;
    // if (rsp_zk2 != null)
    // {
    // CollectionHelper.ListWriteValue(ref Rsp_Zk2, rsp_zk2);
    // return true;
    // }

    // var rsp_zk3 = node as RSP_ZK4;
    // if (rsp_zk3 != null)
    // {
    // CollectionHelper.ListWriteValue(ref Rsp_Zk4, rsp_zk3);
    // return true;
    // }

    // var rsp_zk4 = node as RSP_ZK5;
    // if (rsp_zk4 != null)
    // {
    // CollectionHelper.ListWriteValue(ref Rsp_Zk5, rsp_zk4);
    // return true;
    // }

    // var ack = node as Ack;
    // if (ack != null)
    // {
    // CollectionHelper.ListWriteValue(ref AckList, ack);
    // return true;
    // }

    // var zwi = node as ZpiZwi;
    // if (zwi != null)
    // {
    // CollectionHelper.ListWriteValue(ref ZpiZwis, zwi);
    // return true;
    // }

    // var bts = node as BTS;
    // if (bts != null)
    // {
    // EndPacket = bts;
    // return true;
    // }

    // return false;
    // }

    /// <summary>
    ///   The clear answers.
    /// </summary>
    public void ClearAnswers()
    {
      // CollectionHelper.ListClearValues(AckList);
      // CollectionHelper.ListClearValues(Rsp_Zk1);
      // CollectionHelper.ListClearValues(Rsp_Zk2);
      // CollectionHelper.ListClearValues(Rsp_Zk4);
      // CollectionHelper.ListClearValues(Rsp_Zk5);
    }

    /// <summary>
    ///   The clear batch metadata.
    /// </summary>
    public void ClearBatchMetadata()
    {
      BeginPacket = null;
      EndPacket = null;
    }

    /// <summary>
    ///   The clear body.
    /// </summary>
    public void ClearBody()
    {
      ClearMessages();
      ClearAnswers();
    }

    /// <summary>
    ///   The clear messages.
    /// </summary>
    public void ClearMessages()
    {
      CollectionHelper.ListClearValues(Adt_A01);

      // CollectionHelper.ListClearValues(Adt_A03);
      // CollectionHelper.ListClearValues(Adt_A24);
      // CollectionHelper.ListClearValues(Adt_A37);
      // CollectionHelper.ListClearValues(Qbp_Zp1);
      // CollectionHelper.ListClearValues(Qbp_Zp2);
      // CollectionHelper.ListClearValues(Qbp_Zp4);
      // CollectionHelper.ListClearValues(Zpi_za1);
      // CollectionHelper.ListClearValues(Zpi_za7);
    }

    /// <summary>
    ///   The message count.
    /// </summary>
    /// <returns>
    ///   The <see cref="int" />.
    /// </returns>
    public int MessageCount()
    {
      var count = 0;
      count += Adt_A01 != null ? Adt_A01.Count : 0;

      // count += Adt_A03 != null ? Adt_A03.Count : 0;
      // count += Adt_A24 != null ? Adt_A24.Count : 0;
      // count += Adt_A37 != null ? Adt_A37.Count : 0;
      // count += Qbp_Zp1 != null ? Qbp_Zp1.Count : 0;
      // count += Qbp_Zp2 != null ? Qbp_Zp2.Count : 0;
      // count += Qbp_Zp4 != null ? Qbp_Zp4.Count : 0;
      // count += Qbp_Zp9 != null ? Qbp_Zp9.Count : 0;
      // count += ZpiZwis != null ? ZpiZwis.Count : 0;
      // count += Zpi_za1 != null ? Zpi_za1.Count : 0;
      // count += Zpi_za7 != null ? Zpi_za7.Count : 0;
      return count;
    }

    /// <summary>
    ///   The prepare answer.
    /// </summary>
    public void PrepareAnswer()
    {
      // AckList = new List<Ack>();
      // Rsp_Zk1 = new List<RSP_ZK1>();
      // Rsp_Zk2 = new List<RSP_ZK2>();
      // Rsp_Zk4 = new List<RSP_ZK4>();
      // Rsp_Zk5 = new List<RSP_ZK5>();
    }

    /// <summary>
    ///   The prepare card.
    /// </summary>
    public void PrepareCard()
    {
      // Zpi_za1 = new List<ZPI_ZA1>();
      // Zpi_za7 = new List<ZPI_ZA7>();
    }

    /// <summary>
    ///   The prepare erp.
    /// </summary>
    public void PrepareErp()
    {
      Adt_A01 = new List<ADT_A01>();

      // Adt_A03 = new List<ADT_A03>();
      // Adt_A24 = new List<ADT_A24>();
      // Adt_A37 = new List<ADT_A37>();
      // Qbp_Zp1 = new List<QBP_ZP1>();
      // Qbp_Zp2 = new List<QBP_ZP2>();
      // Qbp_Zp4 = new List<QBP_ZP4>();
      // Qbp_Zp9 = new List<QBP_ZP9>();
      // ZpiZwis = new List<ZpiZwi>();
    }

    /// <summary>
    ///   The reply count.
    /// </summary>
    /// <returns>
    ///   The <see cref="long" />.
    /// </returns>
    public long ReplyCount()
    {
      // return (((AckList.Count + Rsp_Zk1.Count) + Rsp_Zk2.Count) + Rsp_Zk4.Count) + Rsp_Zk5.Count;
      return 0;
    }

    /// <summary>
    ///   The retrieve short info.
    /// </summary>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    public string RetrieveShortInfo()
    {
      return TStringHelper.CombineStrings(BeginPacket.RetrieveShortInfo(), EndPacket.RetrieveShortInfo(), ", ");
    }

    #endregion
  }
}