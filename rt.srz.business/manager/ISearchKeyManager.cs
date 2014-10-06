// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISearchKeyManager.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface SearchKeyManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System.Collections.Generic;

  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The interface SearchKeyManager.
  /// </summary>
  public partial interface ISearchKeyManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The calculate keys.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<SearchKey> CalculateStandardKeys(Statement statement);

    /// <summary>
    /// The calculate keys.
    /// </summary>
    /// <param name="keyTypeList">
    /// The key Type List.
    /// </param>
    /// <param name="pd">
    /// The pd.
    /// </param>
    /// <param name="doc">
    /// The doc.
    /// </param>
    /// <param name="addr1">
    /// The addr 1.
    /// </param>
    /// <param name="addr2">
    /// The addr 2.
    /// </param>
    /// <param name="medicalInsurances">
    /// The medical Insurances.
    /// </param>
    /// <param name="okato">
    /// The okato.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<SearchKey> CalculateUserKeys(
      IList<SearchKeyType> keyTypeList, 
      InsuredPersonDatum pd, 
      Document doc, 
      address addr1, 
      address addr2, 
      IList<MedicalInsurance> medicalInsurances, 
      string okato);

    /// <summary>
    /// The save search keys.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="keys">
    /// The keys.
    /// </param>
    void SaveSearchKeys(Statement statement, IEnumerable<SearchKey> keys);

    #endregion
  }
}