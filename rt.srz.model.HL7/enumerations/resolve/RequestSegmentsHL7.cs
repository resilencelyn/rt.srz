// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestSegmentsHl7.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The request segments h l 7.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.enumerations.resolve
{
  /// <summary>
  ///   The request segments h l 7.
  /// </summary>
  public static class RequestSegmentsHl7
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The first segment.
    /// </summary>
    /// <returns>
    ///   The <see cref="RequestSegmentHl7" />.
    /// </returns>
    public static RequestSegmentHl7 FirstSegment()
    {
      return RequestSegmentHl7.ZP1;
    }

    /// <summary>
    /// The next segment.
    /// </summary>
    /// <param name="currentSegment">
    /// The current segment.
    /// </param>
    /// <returns>
    /// The <see cref="RequestSegmentHl7"/>.
    /// </returns>
    public static RequestSegmentHl7 NextSegment(RequestSegmentHl7 currentSegment)
    {
      var thl = currentSegment;
      if (thl <= RequestSegmentHl7.A03)
      {
        switch (thl)
        {
          case RequestSegmentHl7.ZP1:
            return RequestSegmentHl7.ZP2;

          case RequestSegmentHl7.ZP2:
            return RequestSegmentHl7.ZP4;

          case (RequestSegmentHl7)0x67:
          case (RequestSegmentHl7)0xca:
            goto Label_0081;

          case RequestSegmentHl7.ZP4:
            return RequestSegmentHl7.A01;

          case RequestSegmentHl7.A01:
            return RequestSegmentHl7.A03;

          case RequestSegmentHl7.A03:
            return RequestSegmentHl7.A24;
        }
      }
      else
      {
        switch (thl)
        {
          case RequestSegmentHl7.A24:
            return RequestSegmentHl7.A37;

          case RequestSegmentHl7.A37:
            return RequestSegmentHl7.ZA1;

          case RequestSegmentHl7.ZA1:
            return RequestSegmentHl7.ZA7;
        }
      }

      Label_0081:
      return RequestSegmentHl7.Undefined;
    }

    #endregion

    ///// <summary>
    ///// The try resolve segment.
    ///// </summary>
    ///// <param name="person">
    ///// The person.
    ///// </param>
    ///// <param name="segment">
    ///// The segment.
    ///// </param>
    ///// <param name="segmentIndex">
    ///// The segment index.
    ///// </param>
    ///// <param name="za7">
    ///// The za 7.
    ///// </param>
    ///// <param name="za1">
    ///// The za 1.
    ///// </param>
    ///// <param name="msh">
    ///// The msh.
    ///// </param>
    ///// <param name="evn">
    ///// The evn.
    ///// </param>
    ///// <returns>
    ///// The <see cref="bool"/>.
    ///// </returns>
    // public static bool TryResolveSegment(
    // BasePersonTemplate person, 
    // RequestSegmentHl7 segment, 
    // int segmentIndex, 
    // out ZPI_ZA7 za7, 
    // out ZPI_ZA1 za1, 
    // out MSH msh, 
    // out Evn evn)
    // {
    // za7 = null;
    // za1 = null;
    // msh = null;
    // evn = null;
    // var thl = segment;
    // if (thl != RequestSegmentHl7.ZA1)
    // {
    // if ((thl == RequestSegmentHl7.ZA7) && ((person.Zpi_za7 != null) && (segmentIndex < person.Zpi_za7.Count)))
    // {
    // msh = person.Zpi_za7[segmentIndex].Msh;
    // za7 = person.Zpi_za7[segmentIndex];
    // return true;
    // }
    // }
    // else if ((person.Zpi_za1 != null) && (segmentIndex < person.Zpi_za1.Count))
    // {
    // msh = person.Zpi_za1[segmentIndex].Msh;
    // za1 = person.Zpi_za1[segmentIndex];
    // evn = person.Zpi_za1[segmentIndex].Evn;
    // return true;
    // }

    // return false;
    // }

    ///// <summary>
    ///// The try resolve segment.
    ///// </summary>
    ///// <param name="person">
    ///// The person.
    ///// </param>
    ///// <param name="segment">
    ///// The segment.
    ///// </param>
    ///// <param name="segmentIndex">
    ///// The segment index.
    ///// </param>
    ///// <param name="msh">
    ///// The msh.
    ///// </param>
    ///// <param name="evn">
    ///// The evn.
    ///// </param>
    ///// <param name="pid">
    ///// The pid.
    ///// </param>
    ///// <param name="pidList">
    ///// The pid list.
    ///// </param>
    ///// <param name="insuranceList">
    ///// The insurance list.
    ///// </param>
    ///// <param name="qpd">
    ///// The qpd.
    ///// </param>
    ///// <param name="qpdZp1">
    ///// The qpd zp 1.
    ///// </param>
    ///// <returns>
    ///// The <see cref="bool"/>.
    ///// </returns>
    // public static bool TryResolveSegment(
    // BasePersonTemplate person, 
    // RequestSegmentHl7 segment, 
    // int segmentIndex, 
    // out MSH msh, 
    // out Evn evn, 
    // out MessagePid pid, 
    // out IList<MessagePid> pidList, 
    // out IList<ADT_A01_INSURANCE> insuranceList, 
    // out Qpd qpd, 
    // out QPD_ZP1 qpdZp1)
    // {
    // msh = null;
    // evn = null;
    // pid = null;
    // pidList = null;
    // insuranceList = null;
    // qpd = null;
    // qpdZp1 = null;
    // var thl = segment;
    // if (thl <= RequestSegmentHl7.A03)
    // {
    // switch (thl)
    // {
    // case RequestSegmentHl7.ZP1:
    // if ((person.Qbp_Zp1 == null) || (segmentIndex >= person.Qbp_Zp1.Count))
    // {
    // goto Label_02B7;
    // }

    // msh = person.Qbp_Zp1[segmentIndex].Msh;
    // qpdZp1 = person.Qbp_Zp1[segmentIndex].Qpd;
    // return true;

    // case RequestSegmentHl7.ZP2:
    // if ((person.Qbp_Zp2 == null) || (segmentIndex >= person.Qbp_Zp2.Count))
    // {
    // goto Label_02B7;
    // }

    // msh = person.Qbp_Zp2[segmentIndex].Msh;
    // qpd = person.Qbp_Zp2[segmentIndex].Qpd;
    // return true;

    // case (RequestSegmentHl7)0x67:
    // case (RequestSegmentHl7)0xca:
    // goto Label_02B7;

    // case RequestSegmentHl7.ZP4:
    // if ((person.Qbp_Zp4 == null) || (segmentIndex >= person.Qbp_Zp4.Count))
    // {
    // goto Label_02B7;
    // }

    // msh = person.Qbp_Zp4[segmentIndex].Msh;
    // qpd = person.Qbp_Zp4[segmentIndex].Qpd;
    // return true;

    // case RequestSegmentHl7.A01:
    // if ((person.Adt_A01 == null) || (segmentIndex >= person.Adt_A01.Count))
    // {
    // goto Label_02B7;
    // }

    // msh = person.Adt_A01[segmentIndex].Msh;
    // evn = person.Adt_A01[segmentIndex].Evn;
    // pid = person.Adt_A01[segmentIndex].Pid;
    // insuranceList = person.Adt_A01[segmentIndex].InsuranceList;
    // return true;

    // case RequestSegmentHl7.A03:
    // if ((person.Adt_A03 == null) || (segmentIndex >= person.Adt_A03.Count))
    // {
    // goto Label_02B7;
    // }

    // msh = person.Adt_A03[segmentIndex].Msh;
    // evn = person.Adt_A03[segmentIndex].Evn;
    // pid = person.Adt_A03[segmentIndex].Pid;
    // return true;
    // }
    // }
    // else
    // {
    // switch (thl)
    // {
    // case RequestSegmentHl7.A24:
    // if ((person.Adt_A24 != null) && (segmentIndex < person.Adt_A24.Count))
    // {
    // msh = person.Adt_A24[segmentIndex].Msh;
    // evn = person.Adt_A24[segmentIndex].Evn;
    // pidList = person.Adt_A24[segmentIndex].PidList;
    // return true;
    // }

    // break;

    // case RequestSegmentHl7.A37:
    // if ((person.Adt_A37 != null) && (segmentIndex < person.Adt_A37.Count))
    // {
    // msh = person.Adt_A37[segmentIndex].Msh;
    // evn = person.Adt_A37[segmentIndex].Evn;
    // pidList = person.Adt_A37[segmentIndex].PidList;
    // return true;
    // }

    // break;
    // }
    // }

    // Label_02B7:
    // return false;
    // }
  }
}