namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  using System;
  using System.Xml.Linq;

  // помощник для работы с XML
    public static class XmlHelper
    {       

        // получить значение атрибута

        public static string RetrieveAttribute(this XElement xmlNode, string attributeName, bool trimToNull = false)
        {
            var attribute = xmlNode.Attribute(attributeName);
            if (attribute != null)
                return (trimToNull) ? TStringHelper.StringToNull(attribute.Value, bTrimFirst: true) : attribute.Value;
            return null;
        }
        

        // получить булевское значение

        public static bool RetrieveBool(this XElement xmlNode, string attribute, bool dflt)
        {
            string attributeValue = RetrieveAttribute(xmlNode, attribute);
            if (attributeValue != null)
            {
                switch (ConversionHelper.StringToBool(attributeValue))
                {
                    case BooleanFlag.True:
                        return true;
                    case BooleanFlag.False:
                        return false;
                    default:
                        ThrowCastException(attribute, "Булевский");
                        break;
                }
            }
            return dflt;
        }

       

        // выкинуть сообщение о невозможности преобразования атрибута
        public static void ThrowCastException(string attribute, string type)
        {
            throw new ArgumentException(string.Format("Не удалось преобразовать значение атрибута '{0}' в тип {1}", attribute, type));
        }

        
    }
}
