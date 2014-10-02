using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rt.uec.model.dto
{
  /// <summary>
  /// Параметр рабочей станции
  /// </summary>
  public class WorkstationSettingParameter
  {
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Значение
    /// </summary>
    public string Value { get; set; }
  }
}
