// --------------------------------------------------------------------------------------------------------------------
// <copyright file="address.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The address.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System.Text;
namespace rt.srz.model.srz
{
  /// <summary>
  ///   The address.
  /// </summary>
  public partial class address
  {
    public virtual bool CompareTo(address addr)
    {
      if (Postcode != addr.Postcode)
        return false;

      if (Subject != addr.Subject)
        return false;

      if (Area != addr.Area)
        return false;

      if (City != addr.City)
        return false;

      if (Town != addr.Town)
        return false;

      if (Street != addr.Street)
        return false;

      if (House != addr.House)
        return false;

      if (Housing != addr.Housing)
        return false;

      if (Room != addr.Room)
        return false;

      return true;
    }

    private void AppendItemToStrAddress(StringBuilder sb, string item)
    {
      if (!string.IsNullOrEmpty(item))
      {
        sb.Append(", ").Append(item);
      }
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
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


  }
}