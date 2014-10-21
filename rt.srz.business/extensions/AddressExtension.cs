// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressExtension.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.extensions
{
  using rt.core.model.interfaces;
  using rt.fias.model.fias;
  using rt.srz.business.manager;
  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  ///   The address.
  /// </summary>
  public static class AddressExtension
  {
    #region Public Methods and Operators

    /// <summary>
    /// The regulatory.
    /// </summary>
    /// <param name="address">
    /// The address.
    /// </param>
    /// <returns>
    /// The <see cref="IAddress"/>.
    /// </returns>
    public static IAddress Regulatory(this address address)
    {
      if (address.RegulatoryId != null)
      {
        return ObjectFactory.GetInstance<IAddressService>().GetAddress(address.RegulatoryId.Value);
      }

      return null;
    }

    /// <summary>
    /// The set regulatory.
    /// </summary>
    /// <param name="address">
    /// The address.
    /// </param>
    /// <param name="regulatoryAddress">
    /// The regulatory address.
    /// </param>
    public static void SetRegulatory(this address address, IAddress regulatoryAddress)
    {
      address.RegulatoryId = regulatoryAddress.Id;
    }

    #endregion
  }
}