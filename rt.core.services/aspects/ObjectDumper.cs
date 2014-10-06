// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectDumper.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The object dumper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

 // See the ReadMe.html for additional information

namespace rt.core.services.aspects
{
  using System;
  using System.Collections;
  using System.IO;
  using System.Reflection;

  /// <summary>
  /// The object dumper.
  /// </summary>
  public class ObjectDumper
  {
    #region Fields

    /// <summary>
    /// The depth.
    /// </summary>
    private readonly int depth;

    /// <summary>
    /// The level.
    /// </summary>
    private int level;

    /// <summary>
    /// The pos.
    /// </summary>
    private int pos;

    /// <summary>
    /// The writer.
    /// </summary>
    private TextWriter writer;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectDumper"/> class.
    /// </summary>
    /// <param name="depth">
    /// The depth.
    /// </param>
    private ObjectDumper(int depth)
    {
      this.depth = depth;
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    public static void Write(object element)
    {
      Write(element, 0);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="depth">
    /// The depth.
    /// </param>
    public static void Write(object element, int depth)
    {
      Write(element, depth, Console.Out);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="depth">
    /// The depth.
    /// </param>
    /// <param name="log">
    /// The log.
    /// </param>
    public static void Write(object element, int depth, TextWriter log)
    {
      var dumper = new ObjectDumper(depth);
      dumper.writer = log;
      dumper.WriteObject(null, element);
    }

    /// <summary>
    /// The write to string.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <param name="depth">
    /// The depth.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string WriteToString(object element, int depth)
    {
      var dumper = new ObjectDumper(depth);
      using (var log = new StringWriter())
      {
        dumper.writer = log;
        dumper.WriteObject(null, element);
        log.Flush();
        return log.ToString();
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    private void Write(string s)
    {
      if (s != null)
      {
        writer.Write(s);
        pos += s.Length;
      }
    }

    /// <summary>
    /// The write indent.
    /// </summary>
    private void WriteIndent()
    {
      for (var i = 0; i < level; i++)
      {
        writer.Write("  ");
      }
    }

    /// <summary>
    /// The write line.
    /// </summary>
    private void WriteLine()
    {
      writer.WriteLine();
      pos = 0;
    }

    /// <summary>
    /// The write object.
    /// </summary>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="element">
    /// The element.
    /// </param>
    private void WriteObject(string prefix, object element)
    {
      if (element == null || element is ValueType || element is string)
      {
        WriteIndent();
        Write(prefix);
        WriteValue(element);
        WriteLine();
      }
      else
      {
        var enumerableElement = element as IEnumerable;
        if (enumerableElement != null)
        {
          foreach (var item in enumerableElement)
          {
            if (item is IEnumerable && !(item is string))
            {
              WriteIndent();
              Write(prefix);
              Write("...");
              WriteLine();
              if (level < depth)
              {
                level++;
                WriteObject(prefix, item);
                level--;
              }
            }
            else
            {
              WriteObject(prefix, item);
            }
          }
        }
        else
        {
          var members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
          WriteIndent();
          Write(prefix);
          var propWritten = false;
          foreach (var m in members)
          {
            var f = m as FieldInfo;
            var p = m as PropertyInfo;
            if (f != null || p != null)
            {
              if (propWritten)
              {
                WriteTab();
              }
              else
              {
                propWritten = true;
              }

              Write(m.Name);
              Write("=");
              var t = f != null ? f.FieldType : p.PropertyType;
              if (t.IsValueType || t == typeof(string))
              {
                WriteValue(f != null ? f.GetValue(element) : p.GetValue(element, null));
              }
              else
              {
                if (typeof(IEnumerable).IsAssignableFrom(t))
                {
                  Write("...");
                }
                else
                {
                  Write("{ }");
                }
              }
            }
          }

          if (propWritten)
          {
            WriteLine();
          }

          if (level < depth)
          {
            foreach (var m in members)
            {
              var f = m as FieldInfo;
              var p = m as PropertyInfo;
              if (f != null || p != null)
              {
                var t = f != null ? f.FieldType : p.PropertyType;
                if (!(t.IsValueType || t == typeof(string)))
                {
                  var value = f != null ? f.GetValue(element) : p.GetValue(element, null);
                  if (value != null)
                  {
                    level++;
                    WriteObject(m.Name + ": ", value);
                    level--;
                  }
                }
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// The write tab.
    /// </summary>
    private void WriteTab()
    {
      Write("  ");
      while (pos % 8 != 0)
      {
        Write(" ");
      }
    }

    /// <summary>
    /// The write value.
    /// </summary>
    /// <param name="o">
    /// The o.
    /// </param>
    private void WriteValue(object o)
    {
      if (o == null)
      {
        Write("null");
      }
      else if (o is DateTime)
      {
        Write(((DateTime)o).ToShortDateString());
      }
      else if (o is ValueType || o is string)
      {
        Write(o.ToString());
      }
      else if (o is IEnumerable)
      {
        Write("...");
      }
      else
      {
        Write("{ }");
      }
    }

    #endregion
  }
}