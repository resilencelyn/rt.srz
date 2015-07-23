// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TReflector.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The t reflector.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.dotNetX
{
  #region references

  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Linq;
  using System.Reflection;
  using System.Reflection.Emit;

  #endregion

  /// <summary>
  ///   The t reflector.
  /// </summary>
  public static class TReflector
  {
    #region Delegates

    /// <summary>
    ///   The reflect object custom handler.
    /// </summary>
    /// <param name="Source">
    ///   The source.
    /// </param>
    /// <param name="Destination">
    ///   The destination.
    /// </param>
    /// <param name="SourceMemberInfo">
    ///   The source member info.
    /// </param>
    /// <param name="DestinationMemberInfo">
    ///   The destination member info.
    /// </param>
    public delegate bool ReflectObjectCustomHandler(
      object Source, 
      object Destination, 
      MemberInfo SourceMemberInfo, 
      MemberInfo DestinationMemberInfo);

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The caller descendent types.
    /// </summary>
    /// <param name="BaseType">
    /// The base type.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public static IList<Type> CallerDescendentTypes(Type BaseType)
    {
      return CallerDescendentTypes(BaseType, true);
    }

    /// <summary>
    /// The caller descendent types.
    /// </summary>
    /// <param name="BaseType">
    /// The base type.
    /// </param>
    /// <param name="bIncludeClassesOnly">
    /// The b include classes only.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public static IList<Type> CallerDescendentTypes(Type BaseType, bool bIncludeClassesOnly)
    {
      return CallerDescendentTypes(BaseType, bIncludeClassesOnly, true);
    }

    /// <summary>
    /// The caller descendent types.
    /// </summary>
    /// <param name="BaseType">
    /// The base type.
    /// </param>
    /// <param name="bIncludeClassesOnly">
    /// The b include classes only.
    /// </param>
    /// <param name="bExcludeAbstractTypes">
    /// The b exclude abstract types.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public static IList<Type> CallerDescendentTypes(Type BaseType, bool bIncludeClassesOnly, bool bExcludeAbstractTypes)
    {
      return GetDescendentTypes(Assembly.GetCallingAssembly(), BaseType, bIncludeClassesOnly, bExcludeAbstractTypes);
    }

    /// <summary>
    /// The change member value.
    /// </summary>
    /// <param name="NewValue">
    /// The new value.
    /// </param>
    /// <param name="ClassInstance">
    /// The class instance.
    /// </param>
    /// <param name="MemberName">
    /// The member name.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool ChangeMemberValue(object NewValue, object ClassInstance, string MemberName)
    {
      return ChangeMemberValue(NewValue, ClassInstance, MemberName, false);
    }

    /// <summary>
    /// The change member value.
    /// </summary>
    /// <param name="NewValue">
    /// The new value.
    /// </param>
    /// <param name="ClassInstance">
    /// The class instance.
    /// </param>
    /// <param name="MemberName">
    /// The member name.
    /// </param>
    /// <param name="bNonPublicAllowed">
    /// The b non public allowed.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool ChangeMemberValue(
      object NewValue, 
      object ClassInstance, 
      string MemberName, 
      bool bNonPublicAllowed)
    {
      return ChangeMemberValue(NewValue, ClassInstance, MemberName, bNonPublicAllowed, false);
    }

    /// <summary>
    /// The change member value.
    /// </summary>
    /// <param name="NewValue">
    /// The new value.
    /// </param>
    /// <param name="ClassInstance">
    /// The class instance.
    /// </param>
    /// <param name="MemberName">
    /// The member name.
    /// </param>
    /// <param name="bNonPublicAllowed">
    /// The b non public allowed.
    /// </param>
    /// <param name="bThrowExceptionIfNotFound">
    /// The b throw exception if not found.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool ChangeMemberValue(
      object NewValue, 
      object ClassInstance, 
      string MemberName, 
      bool bNonPublicAllowed, 
      bool bThrowExceptionIfNotFound)
    {
      bool flag;
      var obj2 = GetMemberValue(ClassInstance, MemberName, bNonPublicAllowed, bThrowExceptionIfNotFound);
      if (NewValue == null)
      {
        if (obj2 == null)
        {
          return false;
        }

        flag = false;
      }
      else
      {
        flag = obj2 != null;
      }

      if (flag)
      {
        var comparable = NewValue as IComparable;
        if (comparable != null)
        {
          if (comparable.CompareTo(obj2) == 0)
          {
            return false;
          }
        }
        else if (NewValue.Equals(obj2))
        {
          return false;
        }
      }

      return SetMemberValue(NewValue, ClassInstance, MemberName, bNonPublicAllowed);
    }

    /// <summary>
    /// The collect descendent types.
    /// </summary>
    /// <param name="TypeDescenders">
    /// The type descenders.
    /// </param>
    /// <param name="Assembly">
    /// The assembly.
    /// </param>
    /// <param name="BaseType">
    /// The base type.
    /// </param>
    public static void CollectDescendentTypes(ref Collection<Type> TypeDescenders, Assembly Assembly, Type BaseType)
    {
      CollectDescendentTypes(ref TypeDescenders, Assembly, BaseType, true);
    }

    /// <summary>
    /// The collect descendent types.
    /// </summary>
    /// <param name="TypeDescenders">
    /// The type descenders.
    /// </param>
    /// <param name="Assembly">
    /// The assembly.
    /// </param>
    /// <param name="BaseType">
    /// The base type.
    /// </param>
    /// <param name="bIncludeClassesOnly">
    /// The b include classes only.
    /// </param>
    public static void CollectDescendentTypes(
      ref Collection<Type> TypeDescenders, 
      Assembly Assembly, 
      Type BaseType, 
      bool bIncludeClassesOnly)
    {
      CollectDescendentTypes(ref TypeDescenders, Assembly, BaseType, bIncludeClassesOnly, true);
    }

    /// <summary>
    /// The collect descendent types.
    /// </summary>
    /// <param name="TypeDescenders">
    /// The type descenders.
    /// </param>
    /// <param name="Assembly">
    /// The assembly.
    /// </param>
    /// <param name="BaseType">
    /// The base type.
    /// </param>
    /// <param name="bIncludeClassesOnly">
    /// The b include classes only.
    /// </param>
    /// <param name="bExcludeAbstractTypes">
    /// The b exclude abstract types.
    /// </param>
    public static void CollectDescendentTypes(
      ref Collection<Type> TypeDescenders, 
      Assembly Assembly, 
      Type BaseType, 
      bool bIncludeClassesOnly, 
      bool bExcludeAbstractTypes)
    {
      var types = Assembly.GetTypes();
      if (types != null)
      {
        foreach (var type in types)
        {
          if (((!bIncludeClassesOnly || type.IsClass) && (!bExcludeAbstractTypes || !type.IsAbstract))
              && BaseType.IsAssignableFrom(type))
          {
            if (TypeDescenders == null)
            {
              TypeDescenders = new Collection<Type>();
            }

            TypeDescenders.Add(type);
          }
        }
      }
    }

    /// <summary>
    /// The create custom attribute from object.
    /// </summary>
    /// <param name="SourceAttribute">
    /// The source attribute.
    /// </param>
    /// <returns>
    /// The <see cref="CustomAttributeBuilder"/>.
    /// </returns>
    public static CustomAttributeBuilder CreateCustomAttributeFromObject(object SourceAttribute)
    {
      if (SourceAttribute == null)
      {
        return null;
      }

      var type = SourceAttribute.GetType();
      var fields = type.GetFields();
      var fieldValues = new object[fields.Length];
      for (var i = 0; i < fields.Length; i++)
      {
        fieldValues[i] = fields[i].GetValue(SourceAttribute);
      }

      var namedProperties = (from prop in type.GetProperties() where prop.CanWrite select prop).ToArray<PropertyInfo>();
      var propertyValues = new object[namedProperties.Length];
      for (var j = 0; j < namedProperties.Length; j++)
      {
        propertyValues[j] = namedProperties[j].GetValue(SourceAttribute, null);
      }

      return new CustomAttributeBuilder(
        type.GetConstructor(Type.EmptyTypes), 
        new object[0], 
        namedProperties, 
        propertyValues, 
        fields, 
        fieldValues);
    }

    /// <summary>
    /// The create method from method info.
    /// </summary>
    /// <param name="CostructedType">
    /// The costructed type.
    /// </param>
    /// <param name="MethodInfo">
    /// The method info.
    /// </param>
    /// <returns>
    /// The <see cref="MethodBuilder"/>.
    /// </returns>
    public static MethodBuilder CreateMethodFromMethodInfo(TypeBuilder CostructedType, MethodInfo MethodInfo)
    {
      return CostructedType.DefineMethod(
                                         MethodInfo.Name, 
                                         MethodInfo.Attributes, 
                                         MethodInfo.CallingConvention, 
                                         MethodInfo.ReturnType, 
                                         GetTypesFromParameters(MethodInfo.GetParameters()));
    }

    /// <summary>
    /// The get all type members.
    /// </summary>
    /// <param name="Type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="ICollection"/>.
    /// </returns>
    public static ICollection<MemberInfo> GetAllTypeMembers(Type Type)
    {
      return GetAllTypeMembers(Type, false);
    }

    /// <summary>
    /// The get all type members.
    /// </summary>
    /// <param name="Type">
    /// The type.
    /// </param>
    /// <param name="bIncludeStatics">
    /// The b include statics.
    /// </param>
    /// <returns>
    /// The <see cref="ICollection"/>.
    /// </returns>
    public static ICollection<MemberInfo> GetAllTypeMembers(Type Type, bool bIncludeStatics)
    {
      if (Type.IsInterface)
      {
        return GetTypeMembersEx(Type);
      }

      var bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance
                         | BindingFlags.DeclaredOnly;
      if (bIncludeStatics)
      {
        bindingFlags |= BindingFlags.Static;
      }

      var result = new HashSet<MemberInfo>();
      CollectAllTypeMembers(Type, bindingFlags, result);
      return result;
    }

    /// <summary>
    /// The get descendent types.
    /// </summary>
    /// <param name="BaseType">
    /// The base type.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public static IList<Type> GetDescendentTypes(Type BaseType)
    {
      return GetDescendentTypes(BaseType, true);
    }

    /// <summary>
    /// The get descendent types.
    /// </summary>
    /// <param name="Assembly">
    /// The assembly.
    /// </param>
    /// <param name="BaseType">
    /// The base type.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public static IList<Type> GetDescendentTypes(Assembly Assembly, Type BaseType)
    {
      return GetDescendentTypes(Assembly, BaseType, true);
    }

    /// <summary>
    /// The get descendent types.
    /// </summary>
    /// <param name="BaseType">
    /// The base type.
    /// </param>
    /// <param name="bIncludeClassesOnly">
    /// The b include classes only.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public static IList<Type> GetDescendentTypes(Type BaseType, bool bIncludeClassesOnly)
    {
      return GetDescendentTypes(BaseType, bIncludeClassesOnly, true);
    }

    /// <summary>
    /// The get descendent types.
    /// </summary>
    /// <param name="Assembly">
    /// The assembly.
    /// </param>
    /// <param name="BaseType">
    /// The base type.
    /// </param>
    /// <param name="bIncludeClassesOnly">
    /// The b include classes only.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public static IList<Type> GetDescendentTypes(Assembly Assembly, Type BaseType, bool bIncludeClassesOnly)
    {
      return GetDescendentTypes(Assembly, BaseType, bIncludeClassesOnly, true);
    }

    /// <summary>
    /// The get descendent types.
    /// </summary>
    /// <param name="BaseType">
    /// The base type.
    /// </param>
    /// <param name="bIncludeClassesOnly">
    /// The b include classes only.
    /// </param>
    /// <param name="bExcludeAbstractTypes">
    /// The b exclude abstract types.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public static IList<Type> GetDescendentTypes(Type BaseType, bool bIncludeClassesOnly, bool bExcludeAbstractTypes)
    {
      Collection<Type> typeDescenders = null;
      var loadedAsseblies = GetLoadedAsseblies();
      if (loadedAsseblies != null)
      {
        foreach (var assembly in loadedAsseblies)
        {
          CollectDescendentTypes(ref typeDescenders, assembly, BaseType, bIncludeClassesOnly, bExcludeAbstractTypes);
        }
      }

      return typeDescenders;
    }

    /// <summary>
    /// The get descendent types.
    /// </summary>
    /// <param name="Assembly">
    /// The assembly.
    /// </param>
    /// <param name="BaseType">
    /// The base type.
    /// </param>
    /// <param name="bIncludeClassesOnly">
    /// The b include classes only.
    /// </param>
    /// <param name="bExcludeAbstractTypes">
    /// The b exclude abstract types.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public static IList<Type> GetDescendentTypes(
      Assembly Assembly, 
      Type BaseType, 
      bool bIncludeClassesOnly, 
      bool bExcludeAbstractTypes)
    {
      Collection<Type> typeDescenders = null;
      CollectDescendentTypes(ref typeDescenders, Assembly, BaseType, bIncludeClassesOnly, bExcludeAbstractTypes);
      return typeDescenders;
    }

    /// <summary>
    ///   The get loaded asseblies.
    /// </summary>
    /// <returns>
    ///   The <see cref="Assembly[]" />.
    /// </returns>
    public static Assembly[] GetLoadedAsseblies()
    {
      return AppDomain.CurrentDomain.GetAssemblies();
    }

    /// <summary>
    /// The get member type.
    /// </summary>
    /// <param name="mi">
    /// The mi.
    /// </param>
    /// <returns>
    /// The <see cref="Type"/>.
    /// </returns>
    public static Type GetMemberType(MemberInfo mi)
    {
      if (mi != null)
      {
        var info = mi as FieldInfo;
        if (info != null)
        {
          return info.FieldType;
        }

        var info2 = mi as PropertyInfo;
        if (info2 != null)
        {
          return info2.PropertyType;
        }
      }

      return null;
    }

    /// <summary>
    /// The get member type.
    /// </summary>
    /// <param name="ClassType">
    /// The class type.
    /// </param>
    /// <param name="MemberName">
    /// The member name.
    /// </param>
    /// <returns>
    /// The <see cref="Type"/>.
    /// </returns>
    public static Type GetMemberType(Type ClassType, string MemberName)
    {
      return GetMemberType(ClassType, MemberName, false);
    }

    /// <summary>
    /// The get member type.
    /// </summary>
    /// <param name="ClassType">
    /// The class type.
    /// </param>
    /// <param name="MemberName">
    /// The member name.
    /// </param>
    /// <param name="bNonPublicAllowed">
    /// The b non public allowed.
    /// </param>
    /// <returns>
    /// The <see cref="Type"/>.
    /// </returns>
    public static Type GetMemberType(Type ClassType, string MemberName, bool bNonPublicAllowed)
    {
      return GetMemberType(ClassType, MemberName, bNonPublicAllowed, false);
    }

    /// <summary>
    /// The get member type.
    /// </summary>
    /// <param name="ClassType">
    /// The class type.
    /// </param>
    /// <param name="MemberName">
    /// The member name.
    /// </param>
    /// <param name="bNonPublicAllowed">
    /// The b non public allowed.
    /// </param>
    /// <param name="bThrowExceptionIfNotFound">
    /// The b throw exception if not found.
    /// </param>
    /// <returns>
    /// The <see cref="Type"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    public static Type GetMemberType(
      Type ClassType, 
      string MemberName, 
      bool bNonPublicAllowed, 
      bool bThrowExceptionIfNotFound)
    {
      if ((ClassType != null) && !string.IsNullOrEmpty(MemberName))
      {
        var bindingAttr = BindingFlags.Public | BindingFlags.Instance;
        if (bNonPublicAllowed)
        {
          bindingAttr |= BindingFlags.NonPublic;
        }

        var member = ClassType.GetMember(MemberName, bindingAttr);
        if ((member != null) && (member.Length > 0))
        {
          var mi = member[0];
          if (mi != null)
          {
            return GetMemberType(mi);
          }
        }
      }

      if (bThrowExceptionIfNotFound)
      {
        throw new ArgumentException("member not found");
      }

      return null;
    }

    /// <summary>
    /// The get member value.
    /// </summary>
    /// <param name="ClassInstance">
    /// The class instance.
    /// </param>
    /// <param name="Member">
    /// The member.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// </exception>
    public static object GetMemberValue(object ClassInstance, MemberInfo Member)
    {
      var info = Member as FieldInfo;
      if (info != null)
      {
        return info.GetValue(ClassInstance);
      }

      var info2 = Member as PropertyInfo;
      if (info2 == null)
      {
        throw new NotSupportedException("unsupported member type: " + Member.GetType().Name);
      }

      return info2.GetValue(ClassInstance, null);
    }

    /// <summary>
    /// The get member value.
    /// </summary>
    /// <param name="ClassInstance">
    /// The class instance.
    /// </param>
    /// <param name="MemberName">
    /// The member name.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    public static object GetMemberValue(object ClassInstance, string MemberName)
    {
      return GetMemberValue(ClassInstance, MemberName, false);
    }

    /// <summary>
    /// The get member value.
    /// </summary>
    /// <param name="ClassInstance">
    /// The class instance.
    /// </param>
    /// <param name="MemberName">
    /// The member name.
    /// </param>
    /// <param name="bNonPublicAllowed">
    /// The b non public allowed.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    public static object GetMemberValue(object ClassInstance, string MemberName, bool bNonPublicAllowed)
    {
      return GetMemberValue(ClassInstance, MemberName, bNonPublicAllowed, false);
    }

    /// <summary>
    /// The get member value.
    /// </summary>
    /// <param name="ClassInstance">
    /// The class instance.
    /// </param>
    /// <param name="MemberName">
    /// The member name.
    /// </param>
    /// <param name="bNonPublicAllowed">
    /// The b non public allowed.
    /// </param>
    /// <param name="bThrowExceptionIfNotFound">
    /// The b throw exception if not found.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    public static object GetMemberValue(
      object ClassInstance, 
      string MemberName, 
      bool bNonPublicAllowed, 
      bool bThrowExceptionIfNotFound)
    {
      if ((ClassInstance != null) && !string.IsNullOrEmpty(MemberName))
      {
        var type = ClassInstance.GetType();
        if (type != null)
        {
          var bindingAttr = BindingFlags.Public | BindingFlags.Instance;
          if (bNonPublicAllowed)
          {
            bindingAttr |= BindingFlags.NonPublic;
          }

          var member = type.GetMember(MemberName, bindingAttr);
          if ((member != null) && (member.Length > 0))
          {
            var info = member[0];
            if (info != null)
            {
              return GetMemberValue(ClassInstance, info);
            }
          }
        }
      }

      if (bThrowExceptionIfNotFound)
      {
        throw new ArgumentException("member not found");
      }

      return null;
    }

    /// <summary>
    /// The get type members ex.
    /// </summary>
    /// <param name="Type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="ICollection"/>.
    /// </returns>
    public static ICollection<MemberInfo> GetTypeMembersEx(Type Type)
    {
      return GetTypeMembersEx(Type, BindingFlags.Default);
    }

    /// <summary>
    /// The get type members ex.
    /// </summary>
    /// <param name="Type">
    /// The type.
    /// </param>
    /// <param name="BindingFlags">
    /// The binding flags.
    /// </param>
    /// <returns>
    /// The <see cref="ICollection"/>.
    /// </returns>
    public static ICollection<MemberInfo> GetTypeMembersEx(Type Type, BindingFlags BindingFlags)
    {
      if (Type.IsInterface)
      {
        var result = new HashSet<MemberInfo>();
        CollectInterfaceMembers(Type, BindingFlags, result);
        return result;
      }

      return Type.GetMembers(BindingFlags);
    }

    /// <summary>
    /// The get types from parameters.
    /// </summary>
    /// <param name="Parameters">
    /// The parameters.
    /// </param>
    /// <returns>
    /// The <see cref="Type[]"/>.
    /// </returns>
    public static Type[] GetTypesFromParameters(ParameterInfo[] Parameters)
    {
      if ((Parameters == null) || (Parameters.Length < 1))
      {
        return Type.EmptyTypes;
      }

      var typeArray = new Type[Parameters.Length];
      var length = Parameters.Length;
      while (length > 0)
      {
        length--;
        typeArray[length] = Parameters[length].ParameterType;
      }

      return typeArray;
    }

    /// <summary>
    /// The reflect object loosely.
    /// </summary>
    /// <param name="Source">
    /// The source.
    /// </param>
    /// <param name="Destination">
    /// The destination.
    /// </param>
    public static void ReflectObjectLoosely(object Source, object Destination)
    {
      ReflectObjectLoosely(Source, Destination, false);
    }

    /// <summary>
    /// The reflect object loosely.
    /// </summary>
    /// <param name="Source">
    /// The source.
    /// </param>
    /// <param name="Destination">
    /// The destination.
    /// </param>
    /// <param name="ReflectObjectCustom">
    /// The reflect object custom.
    /// </param>
    public static void ReflectObjectLoosely(
      object Source, 
      object Destination, 
      ReflectObjectCustomHandler ReflectObjectCustom)
    {
      ReflectObjectLoosely(Source, Destination, ReflectObjectCustom, false);
    }

    /// <summary>
    /// The reflect object loosely.
    /// </summary>
    /// <param name="Source">
    /// The source.
    /// </param>
    /// <param name="Destination">
    /// The destination.
    /// </param>
    /// <param name="bReflectCollectionParams">
    /// The b reflect collection params.
    /// </param>
    public static void ReflectObjectLoosely(object Source, object Destination, bool bReflectCollectionParams)
    {
      ReflectObjectLoosely(Source, Destination, null, bReflectCollectionParams);
    }

    /// <summary>
    /// The reflect object loosely.
    /// </summary>
    /// <param name="Source">
    /// The source.
    /// </param>
    /// <param name="Destination">
    /// The destination.
    /// </param>
    /// <param name="ReflectObjectCustom">
    /// The reflect object custom.
    /// </param>
    /// <param name="bReflectCollectionParams">
    /// The b reflect collection params.
    /// </param>
    public static void ReflectObjectLoosely(
      object Source, 
      object Destination, 
      ReflectObjectCustomHandler ReflectObjectCustom, 
      bool bReflectCollectionParams)
    {
      if ((Destination != null) && (Source != null))
      {
        var allTypeMembers = GetAllTypeMembers(Destination.GetType());
        if ((allTypeMembers != null) && (allTypeMembers.Count > 0))
        {
          var is3 = GetAllTypeMembers(Source.GetType());
          if ((is3 != null) && (is3.Count > 0))
          {
            foreach (var info in allTypeMembers)
            {
              FieldInfo info5;
              PropertyInfo info6;
              Type propertyType;
              object obj2;
              Type[] interfaces;
              Type type3;
              Type type4;
              object obj3;
              IEnumerable enumerable;
              object obj5;
              MemberTypes types2;
              FieldInfo info2 = null;
              PropertyInfo info3 = null;
              var memberType = info.MemberType;
              if (memberType != MemberTypes.Field)
              {
                if (memberType == MemberTypes.Property)
                {
                  goto Label_0091;
                }

                continue;
              }

              info2 = info as FieldInfo;
              if (info2 == null)
              {
                continue;
              }

              var fieldType = info2.FieldType;
              goto Label_00B5;
              Label_0091:
              info3 = info as PropertyInfo;
              if ((info3 == null) || !info3.CanWrite)
              {
                continue;
              }

              fieldType = info3.PropertyType;
              Label_00B5:
              Label_0337:
              foreach (var info4 in is3)
              {
                if (string.CompareOrdinal(info4.Name, info.Name) != 0)
                {
                  goto Label_0337;
                }

                info5 = null;
                info6 = null;
                types2 = info4.MemberType;
                if (types2 != MemberTypes.Field)
                {
                  if (types2 == MemberTypes.Property)
                  {
                    goto Label_011C;
                  }

                  goto Label_0337;
                }

                info5 = info4 as FieldInfo;
                if (info5 == null)
                {
                  goto Label_0337;
                }

                propertyType = info5.FieldType;
                goto Label_0141;
                Label_011C:
                info6 = info4 as PropertyInfo;
                if ((info6 == null) || !info6.CanRead)
                {
                  goto Label_0337;
                }

                propertyType = info6.PropertyType;
                Label_0141:
                if ((ReflectObjectCustom == null) || !ReflectObjectCustom(Source, Destination, info4, info))
                {
                  if (fieldType == propertyType)
                  {
                    if (typeof(Delegate).IsAssignableFrom(fieldType))
                    {
                      goto Label_0337;
                    }

                    if (info5 != null)
                    {
                      obj2 = info5.GetValue(Source);
                    }
                    else
                    {
                      if (info6 == null)
                      {
                        goto Label_0337;
                      }

                      obj2 = info6.GetValue(Source, null);
                    }

                    if (info2 != null)
                    {
                      info2.SetValue(Destination, obj2);
                    }
                    else if (info3 != null)
                    {
                      info3.SetValue(Destination, obj2, null);
                    }

                    continue;
                  }

                  if ((!bReflectCollectionParams || !typeof(IEnumerable).IsAssignableFrom(propertyType))
                      || (fieldType.GetConstructor(Type.EmptyTypes) == null))
                  {
                    goto Label_0337;
                  }

                  interfaces = fieldType.GetInterfaces();
                  if (interfaces == null)
                  {
                    goto Label_0337;
                  }

                  type3 = null;
                  type4 = null;
                  foreach (var type5 in interfaces)
                  {
                    if (type5.IsGenericType && typeof(ICollection<>).IsAssignableFrom(type5.GetGenericTypeDefinition()))
                    {
                      type4 = type5.GetGenericArguments()[0];
                      if (type4.IsValueType || (type4.GetConstructor(Type.EmptyTypes) != null))
                      {
                        type3 = type5;
                        break;
                      }
                    }
                  }

                  if (type3 == null)
                  {
                    goto Label_0337;
                  }

                  obj3 = Activator.CreateInstance(fieldType);
                  if (info5 != null)
                  {
                    enumerable = info5.GetValue(Source) as IEnumerable;
                  }
                  else
                  {
                    if (info6 == null)
                    {
                      goto Label_0337;
                    }

                    enumerable = info6.GetValue(Source, null) as IEnumerable;
                  }

                  if (enumerable != null)
                  {
                    foreach (var obj4 in enumerable)
                    {
                      obj5 = Activator.CreateInstance(type4);
                      ReflectObjectLoosely(obj4, obj5, ReflectObjectCustom, bReflectCollectionParams);
                      type3.InvokeMember("Add", BindingFlags.InvokeMethod, null, obj3, new[] { obj5 });
                    }

                    if (info2 != null)
                    {
                      info2.SetValue(Destination, obj3);
                    }
                    else if (info3 != null)
                    {
                      info3.SetValue(Destination, obj3, null);
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// The search type.
    /// </summary>
    /// <param name="typeFullName">
    /// The type full name.
    /// </param>
    /// <returns>
    /// The <see cref="Type"/>.
    /// </returns>
    public static Type SearchType(string typeFullName)
    {
      IEnumerable<Assembly> loadedAsseblies = GetLoadedAsseblies();
      if (loadedAsseblies != null)
      {
        foreach (var assembly in loadedAsseblies)
        {
          var type = assembly.GetType(typeFullName, false);
          if (type != null)
          {
            return type;
          }
        }
      }

      return null;
    }

    /// <summary>
    /// The set member value.
    /// </summary>
    /// <param name="NewValue">
    /// The new value.
    /// </param>
    /// <param name="ClassInstance">
    /// The class instance.
    /// </param>
    /// <param name="Member">
    /// The member.
    /// </param>
    /// <exception cref="NotSupportedException">
    /// </exception>
    public static void SetMemberValue(object NewValue, object ClassInstance, MemberInfo Member)
    {
      var info = Member as FieldInfo;
      if (info != null)
      {
        info.SetValue(ClassInstance, NewValue);
      }
      else
      {
        var info2 = Member as PropertyInfo;
        if (info2 == null)
        {
          throw new NotSupportedException("unsupported member type: " + Member.GetType().Name);
        }

        info2.SetValue(ClassInstance, NewValue, null);
      }
    }

    /// <summary>
    /// The set member value.
    /// </summary>
    /// <param name="NewValue">
    /// The new value.
    /// </param>
    /// <param name="ClassInstance">
    /// The class instance.
    /// </param>
    /// <param name="MemberName">
    /// The member name.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool SetMemberValue(object NewValue, object ClassInstance, string MemberName)
    {
      return SetMemberValue(NewValue, ClassInstance, MemberName, false);
    }

    /// <summary>
    /// The set member value.
    /// </summary>
    /// <param name="NewValue">
    /// The new value.
    /// </param>
    /// <param name="ClassInstance">
    /// The class instance.
    /// </param>
    /// <param name="MemberName">
    /// The member name.
    /// </param>
    /// <param name="bNonPublicAllowed">
    /// The b non public allowed.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool SetMemberValue(object NewValue, object ClassInstance, string MemberName, bool bNonPublicAllowed)
    {
      if ((ClassInstance != null) && !string.IsNullOrEmpty(MemberName))
      {
        var type = ClassInstance.GetType();
        if (type != null)
        {
          var bindingAttr = BindingFlags.Public | BindingFlags.Instance;
          if (bNonPublicAllowed)
          {
            bindingAttr |= BindingFlags.NonPublic;
          }

          var member = type.GetMember(MemberName, bindingAttr);
          if ((member != null) && (member.Length > 0))
          {
            var info = member[0];
            if (info != null)
            {
              SetMemberValue(NewValue, ClassInstance, info);
              return true;
            }
          }
        }
      }

      return false;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The collect all type members.
    /// </summary>
    /// <param name="Type">
    /// The type.
    /// </param>
    /// <param name="BindingFlags">
    /// The binding flags.
    /// </param>
    /// <param name="Result">
    /// The result.
    /// </param>
    private static void CollectAllTypeMembers(Type Type, BindingFlags BindingFlags, HashSet<MemberInfo> Result)
    {
      Result.UnionWith(Type.GetMembers(BindingFlags));
      if (Type.BaseType != null)
      {
        CollectAllTypeMembers(Type.BaseType, BindingFlags, Result);
      }
    }

    /// <summary>
    /// The collect interface members.
    /// </summary>
    /// <param name="Type">
    /// The type.
    /// </param>
    /// <param name="BindingFlags">
    /// The binding flags.
    /// </param>
    /// <param name="Result">
    /// The result.
    /// </param>
    private static void CollectInterfaceMembers(Type Type, BindingFlags BindingFlags, HashSet<MemberInfo> Result)
    {
      Result.UnionWith(Type.GetMembers(BindingFlags));
      foreach (var type in Type.GetInterfaces())
      {
        CollectInterfaceMembers(type, BindingFlags, Result);
      }
    }

    #endregion
  }
}