// --------------------------------------------------------------------------------------------------------------------
// <copyright file="House.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.model.fias
{
  using System;
  using System.Text;

  using rt.core.model.interfaces;

  /// <summary>
  ///   The House.
  /// </summary>
  public partial class House : IAddress
  {
    #region Public Properties

    /// <summary>
    ///   Gets the code.
    /// </summary>
    public virtual string Code
    {
      get
      {
        return Buildnum;
      }
    }

    /// <summary>
    /// Gets the index.
    /// </summary>
    public virtual int? Index
    {
      get
      {
        if (Postalcode != null)
        {
          return int.Parse(Postalcode);
        }

        return null;
      }
    }

    /// <summary>
    /// Gets the level.
    /// </summary>
    public virtual int? Level
    {
      get
      {
        return 8;
      }
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public virtual string Name
    {
      get
      {
        var str = new StringBuilder();
        str.Append(string.Format("{0} д.", Housenum));
        if (Buildnum != null)
        {
          str.Append(string.Format(" {0} корп.", Buildnum));
        }

        if (Strucnum != null)
        {
          str.Append(string.Format(" {0} стр.", Strucnum));
        }

        return str.ToString();
      }
    }

    /// <summary>
    /// Gets the parent id.
    /// </summary>
    public virtual Guid? ParentId
    {
      get
      {
        return Aoguid;
      }
    }

    /// <summary>
    /// Gets the socr.
    /// </summary>
    public virtual string Socr
    {
      get
      {
        return string.Empty;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The get address.
    /// </summary>
    /// <returns>
    /// The <see cref="Address"/>.
    /// </returns>
    public virtual Address GetAddress()
    {
      return new Address
             {
               Id = Id, 
               Code = Code, 
               Index = Index, 
               Level = Level, 
               Name = Name, 
               Okato = Okato, 
               Socr = Socr, 
               ParentId = ParentId
             };
    }

    #endregion
  }
}