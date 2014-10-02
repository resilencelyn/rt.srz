// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepresentativeManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface RepresentativeManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.dto;
using rt.srz.model.srz;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface RepresentativeManager.
  /// </summary>
  public partial interface IRepresentativeManager
  {
    /// <summary>
    /// ѕолучает данные по номеру и серии документа
    /// </summary>
    /// <param name="number"></param>
    /// <param name="series"></param>
    /// <returns></returns>
    Representative GetRepresentativeContactInfoByUdl(string number, string series);
  }
}