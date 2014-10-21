// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AObject.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.model.fias
{
  using System;
  using System.Runtime.Serialization;

  using ProtoBuf;

  using rt.core.model.interfaces;

  /// <summary>
  ///   The AObject.
  /// </summary>
  [KnownType(typeof(AObject))]
  [ProtoInclude(10, typeof(AObject))]
  public partial class AObject : IAddress
  {
    #region Public Properties

    /// <summary>
    ///   Gets the index.
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
    ///   Gets the level.
    /// </summary>
    public virtual int? Level
    {
      get
      {
        return Aolevel;
      }
    }

    /// <summary>
    ///   Gets the name.
    /// </summary>
    public virtual string Name
    {
      get
      {
        return Offname;
      }
    }

    /// <summary>
    ///   Gets the parent.
    /// </summary>
    public virtual Guid? ParentId
    {
      get
      {
        return Parentguid;
      }
    }

    /// <summary>
    ///   Gets the socr.
    /// </summary>
    public virtual string Socr
    {
      get
      {
        return Shortname;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The get address.
    /// </summary>
    /// <returns>
    ///   The <see cref="Address" />.
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