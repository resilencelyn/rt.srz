//-------------------------------------------------------------------------------------
// <copyright file="person.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using System.Runtime.Serialization;
namespace rt.atl.model.atl
{
  /// <summary>
  /// The person.
  /// </summary>
  public partial class person 
  {
    [DataMember(Order = 159)]
    public virtual bool IsExported { get; set; }

    [DataMember(Order = 160)]
    public virtual string ExportError { get; set; }
  }
}