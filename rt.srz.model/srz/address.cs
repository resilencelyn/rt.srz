// --------------------------------------------------------------------------------------------------------------------
// <copyright file="address.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The address.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System.Text;

  using rt.core.model.interfaces;

  /// <summary>
  ///   The address.
  /// </summary>
  public partial class address
  {
    #region Public Methods and Operators

    /// <summary>
    /// The compare to.
    /// </summary>
    /// <param name="addr">
    /// The addr.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public virtual bool CompareTo(address addr)
    {
      if (Postcode != addr.Postcode)
      {
        return false;
      }

      if (Subject != addr.Subject)
      {
        return false;
      }

      if (Area != addr.Area)
      {
        return false;
      }

      if (City != addr.City)
      {
        return false;
      }

      if (Town != addr.Town)
      {
        return false;
      }

      if (Street != addr.Street)
      {
        return false;
      }

      if (House != addr.House)
      {
        return false;
      }

      if (Housing != addr.Housing)
      {
        return false;
      }

      if (Room != addr.Room)
      {
        return false;
      }

      return true;
    }

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public override string ToString()
    {
      var sb = new StringBuilder();
      sb.Append(Postcode);
      AppendItemToStrAddress(sb, Subject);
      AppendItemToStrAddress(sb, Area);
      AppendItemToStrAddress(sb, City);
      AppendItemToStrAddress(sb, Town);
      AppendItemToStrAddress(sb, Street);
      AppendItemToStrAddress(sb, House);
      AppendItemToStrAddress(sb, Housing);
      AppendItemToStrAddress(sb, Room.HasValue ? Room.Value.ToString() : null);
      return sb.ToString();
    }

    #endregion

    #region Methods

    /// <summary>
    /// The append item to str address.
    /// </summary>
    /// <param name="sb">
    /// The sb.
    /// </param>
    /// <param name="item">
    /// The item.
    /// </param>
    private void AppendItemToStrAddress(StringBuilder sb, string item)
    {
      if (!string.IsNullOrEmpty(item))
      {
        sb.Append(", ").Append(item);
      }
    }

    #endregion
  }
}