// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PolicySearchFields.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The policy search fields.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.srz.database.business.standard
{
  using System;
  using System.Collections.Generic;

  // [CLSCompliant(false)]
  /// <summary>
  /// The policy search fields.
  /// </summary>
  public sealed class PolicySearchFields
  {
    // --------------------------------------------------------

    /// <summary>
    /// The field name resolver.
    /// </summary>
    public readonly PolicySearchFieldNameResolver FieldNameResolver; // !! может быть null

    /// <summary>
    /// The field names.
    /// </summary>
    private HashSet<string> fieldNames;

    /// <summary>
    /// The fields.
    /// </summary>
    private HashSet<FieldTypes> fields;

    // --------------------------------------------------------
    // public statics

    // public static FieldTypes IndexToField(int index)
    // {
    // unchecked
    // {
    // return (FieldTypes)index;
    // }
    // }

    // --------------------------------------------------------

    // public static int FieldToIndex(FieldTypes field)
    // {
    // unchecked
    // {
    // return (int)field;
    // }
    // }

    // --------------------------------------------------------

    // public static FieldTypes TryResolveFieldName(string fieldName, PolicySearchFieldNameResolver resolver = null)
    // {
    // FieldTypes field;
    // if (resolver != null)
    // {
    // field = resolver(fieldName);
    // if (field != FieldTypes.Undefined)
    // return field;
    // }
    // try
    // {
    // return (FieldTypes)Enum.Parse(typeof(FieldTypes), fieldName, ignoreCase: true);
    // }
    // catch { }
    // return FieldTypes.Undefined;
    // }

    // --------------------------------------------------------
    // properties

    // --------------------------------------------------------

    // public IEnumerable<FieldTypes> Fields // !! может вернуть null
    // {
    // get
    // {
    // CheckWhetherResolved();
    // return fields;
    // }
    // }

    // --------------------------------------------------------
    // publics

    /// <summary>
    /// Initializes a new instance of the <see cref="PolicySearchFields"/> class.
    /// </summary>
    /// <param name="FieldNameResolver">
    /// The field name resolver.
    /// </param>
    public PolicySearchFields(PolicySearchFieldNameResolver FieldNameResolver = null)
    {
      this.FieldNameResolver = FieldNameResolver;
    }

    // --------------------------------------------------------

    /// <summary>
    /// The add field.
    /// </summary>
    /// <param name="fieldName">
    /// The field name.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool AddField(string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        return false;
      if (FieldNameResolver != null)
      {
        var field = FieldNameResolver(fieldName);
        if (field != FieldTypes.Undefined)
          return AddField(field);
      }

      if (fieldNames == null)
        fieldNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
      return fieldNames.Add(fieldName);
    }

    // --------------------------------------------------------

    /// <summary>
    /// The add field.
    /// </summary>
    /// <param name="field">
    /// The field.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool AddField(FieldTypes field)
    {
      if (field != FieldTypes.Undefined)
      {
        if (fields == null)
          fields = new HashSet<FieldTypes>();
        if (fields.Add(field))
        {
          switch (field)
          {
            case FieldTypes.IdCardType_IdCardNumber:
              fields.Add(FieldTypes.IdCardType);
              fields.Add(FieldTypes.IdCardNumber);
              break;
            case FieldTypes.PolicyType_PolicyNumber:
              fields.Add(FieldTypes.PolicyType);
              fields.Add(FieldTypes.PolicyNumber);
              break;
            case FieldTypes.InsuranceCompanyCode_InsuranceCompanyCoding:
              fields.Add(FieldTypes.InsuranceCompanyCode);
              fields.Add(FieldTypes.InsuranceCompanyCoding);
              break;
          }

          return true;
        }
      }

      return false;
    }

    // --------------------------------------------------------

    /// <summary>
    /// The add fields.
    /// </summary>
    /// <param name="fieldsList">
    /// The fields list.
    /// </param>
    /// <param name="separator">
    /// The separator.
    /// </param>
    public void AddFields(string fieldsList, params char[] separator)
    {
      if (!string.IsNullOrEmpty(fieldsList))
      {
        var fields = fieldsList.Split(separator);
        if (fields != null)
        {
          foreach (var field in fields)
            AddField(field.Trim());
        }
      }
    }

    // --------------------------------------------------------

    /// <summary>
    /// The add fields.
    /// </summary>
    /// <param name="fieldsList">
    /// The fields list.
    /// </param>
    public void AddFields(string fieldsList)
    {
      AddFields(fieldsList, ',');
    }

    // --------------------------------------------------------

    // public void AddFields(IEnumerable<FieldTypes> fields)
    // {
    // if (fields != null)
    // {
    // foreach (var field in fields)
    // AddField(field);
    // }
    // }

    // --------------------------------------------------------

    // public void AddFields(params FieldTypes[] fields)
    // {
    // AddFields((IEnumerable<FieldTypes>)fields);
    // }

    // --------------------------------------------------------

    // public void ResetFields(string fieldsList, params char[] separator)
    // {
    // ClearFields();
    // AddFields(fieldsList, separator);
    // }

    // --------------------------------------------------------

    // public void ResetFields(string fieldsList)
    // {
    // ResetFields(fieldsList, ',');
    // }

    // --------------------------------------------------------

    // public void ResetFields(IEnumerable<FieldTypes> fields)
    // {
    // ClearFields();
    // AddFields(fields);
    // }

    // --------------------------------------------------------

    // public void ResetFields(params FieldTypes[] fields)
    // {
    // ResetFields((IEnumerable<FieldTypes>)fields);
    // }

    // --------------------------------------------------------

    // public bool ResolveFields(PolicySearchFieldNameResolver resolver)
    // {
    // if (fieldNames != null)
    // {
    // HashSet<string> unresolvedNames = null;
    // foreach (var fieldName in fieldNames)
    // {
    // FieldTypes field = TryResolveFieldName(fieldName, resolver);
    // if (field != FieldTypes.Undefined)
    // {
    // AddField(field);
    // }
    // else
    // {
    // if (unresolvedNames == null)
    // unresolvedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    // unresolvedNames.Add(fieldName);
    // }
    // }
    // fieldNames = unresolvedNames;
    // }
    // return (fieldNames == null);
    // }

    // --------------------------------------------------------

    // public bool ResolveFields()
    // {
    // return ResolveFields(FieldNameResolver);
    // }

    // --------------------------------------------------------

    // public void ClearFields()
    // {
    // if (fieldNames != null)
    // fieldNames.Clear();
    // if (fields != null)
    // fields.Clear();
    // }

    // --------------------------------------------------------

    // public bool HasFields()
    // {
    // return (fields != null && fields.Count > 0) || (fieldNames != null && fieldNames.Count > 0);
    // }

    // --------------------------------------------------------

    // public bool ContainsField(FieldTypes field)
    // {
    // CheckWhetherResolved();
    // if (fields != null)
    // return fields.Contains(field);
    // return false;
    // }

    // --------------------------------------------------------

    /// <summary>
    /// The empty or contains field.
    /// </summary>
    /// <param name="field">
    /// The field.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool EmptyOrContainsField(FieldTypes field)
    {
      CheckWhetherResolved();
      if (fields != null && fields.Count > 0)
        return fields.Contains(field);
      return true;
    }

    // --------------------------------------------------------
    // privates

    /// <summary>
    /// The check whether resolved.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// </exception>
    private void CheckWhetherResolved()
    {
      if (fieldNames != null && fieldNames.Count > 0)
        throw new InvalidOperationException("имена полей не разрешены в их идексы");
    }

    // --------------------------------------------------------
  }
}