// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Fio.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fio.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The fio.
  /// </summary>
  [Serializable]
  public class Fio
  {
    #region Fields

    /// <summary>
    ///   The name.
    /// </summary>
    [XmlElement(ElementName = "XPN.2", Order = 2)]
    public string Name;

    /// <summary>
    ///   The otchestvo.
    /// </summary>
    [XmlElement(ElementName = "XPN.3", Order = 3)]
    public string Otchestvo;

    /// <summary>
    ///   The surname.
    /// </summary>
    [XmlElement(ElementName = "XPN.1", Order = 1)]
    public Surname Surname;

    /// <summary>
    ///   The type code fio.
    /// </summary>
    [XmlElement(ElementName = "XPN.7", Order = 7)]
    public string TypeCodeFio;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="Fio" /> class.
    /// </summary>
    public Fio()
    {
      Surname = new Surname();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Fio"/> class.
    /// </summary>
    /// <param name="Surname">
    /// The surname.
    /// </param>
    /// <param name="Name">
    /// The name.
    /// </param>
    /// <param name="Otchestvo">
    /// The otchestvo.
    /// </param>
    /// <param name="TypeCodeFio">
    /// The type code fio.
    /// </param>
    public Fio(Surname Surname, string Name, string Otchestvo, string TypeCodeFio)
    {
      this.Surname = Surname;
      this.Name = Name;
      this.Otchestvo = Otchestvo;
      this.TypeCodeFio = TypeCodeFio;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the surname 1.
    /// </summary>
    [XmlIgnore]
    public string Surname1
    {
      get
      {
        return Surname.surname;
      }

      set
      {
        Surname.surname = value;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The ==.
    /// </summary>
    /// <param name="left">
    ///   The left.
    /// </param>
    /// <param name="right">
    ///   The right.
    /// </param>
    /// <returns>
    /// </returns>
    public static bool operator ==(Fio left, Fio right)
    {
      return Equals(left, right);
    }

    /// <summary>
    ///   The !=.
    /// </summary>
    /// <param name="left">
    ///   The left.
    /// </param>
    /// <param name="right">
    ///   The right.
    /// </param>
    /// <returns>
    /// </returns>
    public static bool operator !=(Fio left, Fio right)
    {
      return !Equals(left, right);
    }

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj))
      {
        return false;
      }

      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      if (obj.GetType() != typeof(Fio))
      {
        return false;
      }

      return Equals((Fio)obj);
    }

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="other">
    /// The other.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool Equals(Fio other)
    {
      if (ReferenceEquals(null, other))
      {
        return false;
      }

      if (ReferenceEquals(this, other))
      {
        return true;
      }

      return Equals(other.Surname.surname, Surname.surname) && Equals(other.Name, Name)
             && Equals(other.Otchestvo, Otchestvo) && Equals(other.TypeCodeFio, TypeCodeFio);
    }

    /// <summary>
    ///   The get hash code.
    /// </summary>
    /// <returns>
    ///   The <see cref="int" />.
    /// </returns>
    public override int GetHashCode()
    {
      unchecked
      {
        var result = Name != null ? Name.GetHashCode() : 0;
        result = (result * 397) ^ (Otchestvo != null ? Otchestvo.GetHashCode() : 0);
        result = (result * 397) ^ (TypeCodeFio != null ? TypeCodeFio.GetHashCode() : 0);
        result = (result * 397) ^ (Surname.surname != null ? Surname.surname.GetHashCode() : 0);
        return result;
      }
    }

    #endregion
  }
}