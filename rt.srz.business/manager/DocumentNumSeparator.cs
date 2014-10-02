using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using rt.srz.model.srz;
using NHibernate;
using NHibernate.Criterion;

namespace rt.srz.business.manager
{
    public class DocumentNumSeparator
    {
        public static string[] SeparateSpecFormat(string sf)
        {
            var r = sf.Split('№').Select(m => m.Trim()).ToList();
            if (r.Count < 2)
                r.Insert(0, null);
            return r.ToArray();
        }

        public static string SpecFormat(string seria, string num)
        {
            return seria == null ? String.Format("{0}", num) : String.Format("{0} № {1}", seria, num);
        }

        public static string GetFullString(Concept dt, string seria, string num)
        {
            var splited = dt.Description.Split(' ')
                .Select(s => s.Substring(s.IndexOf('=') + 1).Trim('"')).ToArray();
            for (int i = 0; i < splited[1].Count(); i++)
                if (splited[1][i] < num[i])
                    return null;
            if (seria != "")
            {
                for (int i = 0; i < splited[0].Count(); i++)
                    if (splited[0][i] < seria[i])
                        return null;
                return dt.Description.Replace(splited[0], seria).Replace(splited[1], num);
            }
            else
            {
                return dt.Description.Split(' ').ToArray()[1].Replace(splited[1], num);
            }
        }

        public static string GetFullString(string dt, string seria, string num)
        {
            var c = GetDocType(dt);
            if (c == null)
                return null;
            return GetFullString(c, seria, num);
        }

        private static Concept GetDocType(string typeName)
        {
            var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
            var c = session.QueryOver<Concept>()
                .Where(f => f.Name == typeName || f.ShortName == typeName)
                .List();
            return c.Count > 0 ? c.First() : null;
        }
    }
}
