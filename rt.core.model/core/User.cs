//-------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

namespace rt.core.model.core
{
  /// <summary>
  /// The User.
  /// </summary>
  public partial class User 
  {
    public virtual bool IsAdmin
    {
      get { return Login.ToLower() == "admin"; }
    }
  }
}