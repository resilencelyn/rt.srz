namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  using System;
  using System.Collections.Generic;

  using rt.srz.database.business.standard.keyscompiler.Fields;

  //[CLSCompliant(false)]
    public sealed class PolicySearchFields
    {
        // --------------------------------------------------------

        HashSet<string> fieldNames = null;
        HashSet<FieldTypes> fields = null;

        // --------------------------------------------------------
        // public statics

        //public static FieldTypes IndexToField(int index)
        //{
        //    unchecked
        //    {
        //        return (FieldTypes)index;
        //    }
        //}

        // --------------------------------------------------------

        //public static int FieldToIndex(FieldTypes field)
        //{
        //    unchecked
        //    {
        //        return (int)field;
        //    }
        //}

        // --------------------------------------------------------

        //public static FieldTypes TryResolveFieldName(string fieldName, PolicySearchFieldNameResolver resolver = null)
        //{
        //    FieldTypes field;
        //    if (resolver != null)
        //    {
        //        field = resolver(fieldName);
        //        if (field != FieldTypes.Undefined)
        //            return field;
        //    }
        //    try
        //    {
        //        return (FieldTypes)Enum.Parse(typeof(FieldTypes), fieldName, ignoreCase: true);
        //    }
        //    catch { }
        //    return FieldTypes.Undefined;
        //}

        // --------------------------------------------------------
        // properties

        public readonly SearchFieldNameResolver FieldNameResolver; // !! может быть null

        // --------------------------------------------------------

        //public IEnumerable<FieldTypes> Fields // !! может вернуть null
        //{
        //    get
        //    {
        //        CheckWhetherResolved();
        //        return fields;
        //    }
        //}

        // --------------------------------------------------------
        // publics

        public PolicySearchFields(SearchFieldNameResolver FieldNameResolver = null)
        {
            this.FieldNameResolver = FieldNameResolver;
        }

        // --------------------------------------------------------

        public bool AddField(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
                return false;
            if (FieldNameResolver != null)
            {
                FieldTypes field = FieldNameResolver(fieldName);
                if (field != FieldTypes.Undefined)
                    return AddField(field);
            }
            if (fieldNames == null)
                fieldNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            return fieldNames.Add(fieldName);
        }

        // --------------------------------------------------------

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

        public void AddFields(string fieldsList, params char[] separator)
        {
            if (!string.IsNullOrEmpty(fieldsList))
            {
                string[] fields = fieldsList.Split(separator);
                if (fields != null)
                {
                    foreach (var field in fields)
                        AddField(field.Trim());
                }
            }
        }

        // --------------------------------------------------------

        public void AddFields(string fieldsList)
        {
            AddFields(fieldsList, ',');
        }

        // --------------------------------------------------------

        //public void AddFields(IEnumerable<FieldTypes> fields)
        //{
        //    if (fields != null)
        //    {
        //        foreach (var field in fields)
        //            AddField(field);
        //    }
        //}

        // --------------------------------------------------------

        //public void AddFields(params FieldTypes[] fields)
        //{
        //    AddFields((IEnumerable<FieldTypes>)fields);
        //}

        // --------------------------------------------------------

        //public void ResetFields(string fieldsList, params char[] separator)
        //{
        //    ClearFields();
        //    AddFields(fieldsList, separator);
        //}

        // --------------------------------------------------------

        //public void ResetFields(string fieldsList)
        //{
        //    ResetFields(fieldsList, ',');
        //}

        // --------------------------------------------------------

        //public void ResetFields(IEnumerable<FieldTypes> fields)
        //{
        //    ClearFields();
        //    AddFields(fields);
        //}

        // --------------------------------------------------------

        //public void ResetFields(params FieldTypes[] fields)
        //{
        //    ResetFields((IEnumerable<FieldTypes>)fields);
        //}

        // --------------------------------------------------------

        //public bool ResolveFields(PolicySearchFieldNameResolver resolver)
        //{
        //    if (fieldNames != null)
        //    {
        //        HashSet<string> unresolvedNames = null;
        //        foreach (var fieldName in fieldNames)
        //        {
        //            FieldTypes field = TryResolveFieldName(fieldName, resolver);
        //            if (field != FieldTypes.Undefined)
        //            {
        //                AddField(field);
        //            }
        //            else
        //            {
        //                if (unresolvedNames == null)
        //                    unresolvedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //                unresolvedNames.Add(fieldName);
        //            }
        //        }
        //        fieldNames = unresolvedNames;
        //    }
        //    return (fieldNames == null);
        //}

        // --------------------------------------------------------

        //public bool ResolveFields()
        //{
        //    return ResolveFields(FieldNameResolver);
        //}

        // --------------------------------------------------------

        //public void ClearFields()
        //{
        //    if (fieldNames != null)
        //        fieldNames.Clear();
        //    if (fields != null)
        //        fields.Clear();
        //}

        // --------------------------------------------------------

        //public bool HasFields()
        //{
        //    return (fields != null && fields.Count > 0) || (fieldNames != null && fieldNames.Count > 0);
        //}

        // --------------------------------------------------------

        //public bool ContainsField(FieldTypes field)
        //{
        //    CheckWhetherResolved();
        //    if (fields != null)
        //        return fields.Contains(field);
        //    return false;
        //}

        // --------------------------------------------------------

        public bool EmptyOrContainsField(FieldTypes field)
        {
            CheckWhetherResolved();
            if (fields != null && fields.Count > 0)
                return fields.Contains(field);
            return true;
        }

        // --------------------------------------------------------
        // privates

        void CheckWhetherResolved()
        {
            if (fieldNames != null && fieldNames.Count > 0)
                throw new InvalidOperationException("имена полей не разрешены в их идексы");
        }

        // --------------------------------------------------------
    }
}
