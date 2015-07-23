// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dump.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ������ ������ � xml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.aspects
{
  #region references

  using System;
  using System.IO;
  using System.Xml.Serialization;

  using Serialize.Linq.Nodes;

  #endregion

  /// <summary>
  ///   ������ ������ � xml
  /// </summary>
  public static class Dump
  {
    #region Public Methods and Operators

    /// <summary>
    /// ���� �������
    /// </summary>
    /// <param name="output">
    /// ������
    /// </param>
    /// <returns>
    /// Xml
    /// </returns>
    public static string ObjectToXml(object output)
    {
      string objectAsXmlString;
      try
      {
        using (var sw = new StringWriter())
        {
          try
          {
            var type = output.GetType();
            if (output == null || type.IsInterface || output is ExpressionNode)
            {
              return string.Empty;
            }

            var xs = new XmlSerializer(type);
            xs.Serialize(sw, output);
            objectAsXmlString = sw.ToString();
          }
          catch (Exception ex)
          {
            objectAsXmlString = ex.ToString();
          }
        }
      }
      catch (Exception)
      {
        return "�� �������� ������������� ������";
      }

      return objectAsXmlString;
    }

    #endregion
  }
}