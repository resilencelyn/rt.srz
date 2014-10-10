// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FatalErrorTypes.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fatal error types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons.Enumerations.Resolve
{
  /// <summary>
  ///   The fatal error types.
  /// </summary>
  public static class FatalErrorTypes
  {
    #region Public Methods and Operators

    /// <summary>
    /// The fatal error as string.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FatalErrorAsString(FatalErrorType type)
    {
      switch (type)
      {
        case FatalErrorType.Unknown:
          return "����������";

        case FatalErrorType.DatabaseError:
          return "������ ��� ��������� � ��";
      }

      return type.ToString();
    }

    #endregion
  }
}