// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Kladr.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The Kladr.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace rt.srz.model.srz
{
  /// <summary>
  ///   The Kladr.
  /// </summary>
  public partial class Kladr
  {    
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
          MemoryStream stream = new MemoryStream();
          xml.Save(stream);
          stream.Position = 0;
          StreamReader sr = new StreamReader(stream);
          string str = sr.ReadToEnd();
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
          MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
          XmlSerializer reader = new XmlSerializer(typeof(Kladr));
          StreamReader file = new StreamReader(stream);
          return (Kladr)reader.Deserialize(file);
      }        
  }
}