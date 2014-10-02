//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rt.core.model
{
  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  public interface IBusinessBase<T>
  {
    T Id { get; }
  }

  /// <summary>
  /// Base for all business objects.
  /// 
  /// For an explanation of why Equals and GetHashCode are overriden, read the following...
  /// http://devlicio.us/blogs/billy_mccafferty/archive/2007/04/25/using-equals-gethashcode-effectively.aspx
  /// </summary>
  /// <typeparam name="T">DataType of the primary key.</typeparam>
  [DataContract]
  [Serializable]
  public abstract class BusinessBase<T> : Business, IBusinessBase<T>
  {
    private T _id = default(T);

    [XmlElement(Order = 1)]
    [DataMember(Order = 1)]
    public virtual T Id
    {
      get { return _id; }
      set { _id = value; }
    }
  }
}
