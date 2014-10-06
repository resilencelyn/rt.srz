// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Kladr.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The Kladr.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System.IO;
  using System.Text;
  using System.Xml;
  using System.Xml.Serialization;

  /// <summary>
  ///   The Kladr.
  /// </summary>
  public partial class Kladr
  {
    #region Public Methods and Operators

    /// <summary>
    /// �������������� �� ������� XML
    /// </summary>
    /// <param name="xml">
    /// xmlDocument ��� ��������������
    /// </param>
    /// <returns>
    /// ������ Kladr
    /// </returns>
    public static Kladr Deserializating(XmlDocument xml)
    {
      var stream = new MemoryStream();
      xml.Save(stream);
      stream.Position = 0;
      var sr = new StreamReader(stream);
      var str = sr.ReadToEnd();
      return Deserializating(str);
    }

    /// <summary>
    /// �������������� �� ������� XML
    /// </summary>
    /// <param name="xml">
    /// xml-������ ���������� ��������������
    /// </param>
    /// <returns>
    /// ������ Kladr
    /// </returns>
    public static Kladr Deserializating(string xml)
    {
      var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
      var reader = new XmlSerializer(typeof(Kladr));
      var file = new StreamReader(stream);
      return (Kladr)reader.Deserialize(file);
    }

    #endregion
  }
}