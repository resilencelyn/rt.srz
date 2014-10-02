namespace rt.srz.database.business.standard.keyscompiler
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Xml;
  using System.Xml.Linq;

  using rt.srz.database.business.standard.keyscompiler.Fields;

  public static class KeysLoader
    {
        public static readonly SearchFieldNameResolver FieldNameResolver;
        public static List<LoadedKey> LoadKeys(Stream configFile, out Dictionary<string, string> subTypes, SearchFieldNameResolver FieldNameResolver = null)
        {
            List<LoadedKey> keys = new List<LoadedKey>();
            subTypes = new Dictionary<string, string>();
            using (XmlReader reader = XmlReader.Create(configFile))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        try
                        {
                            switch (reader.Name)
                            {
                                case "Key":
                                case "key":
                                    XElement xKey = XElement.Load(reader.ReadSubtree());
                                    LoadedKey key = new LoadedKey()
                                    {

                                        name = xKey.Attribute("name").Value,
                                        type = int.Parse(xKey.Attribute("type").Value),                                        
                                        formula = GetFieldsTypes(xKey.Attribute("value").Value, FieldNameResolver)
                                    };
                                    if (xKey.Attribute("subtype") != null)
                                        key.subtype = int.Parse(xKey.Attribute("subtype").Value);
                                    keys.Add(key);
                                    break;
                                case "subtype":
                                case "subType":
                                    XElement xsubtype = XElement.Load(reader.ReadSubtree());
                                    subTypes.Add(xsubtype.Attribute("name").Value, xsubtype.Attribute("value").Value);
                                    break;
                            }
                            
                                
                            }
                        catch (Exception ex)
                        {
                            throw new Exception("Ошибка при чтении ключей из конфигурационного файла: ", ex);
                        }
                    }
                }
                            }
            return keys;
        }
        private static FieldTypes[] GetFieldsTypes(string value, SearchFieldNameResolver FieldNameResolver = null)
        {
            string[] fields = value.Split(new char[] { '+' });
            FieldTypes[] fieldTypes = new FieldTypes[fields.Length];
            try
            {
                for (int i = 0; i < fields.Length; i++)
                {                   
                    fieldTypes[i] = FieldNameResolver(fields[i].Trim());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при чтении ключей из конфигурационного файла:", ex);
            }
            return fieldTypes;
        }
        
    }
}
