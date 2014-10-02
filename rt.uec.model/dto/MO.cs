using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rt.uec.model.dto
{
  /// <summary>
  /// Медицинская орагнизация(ТФОМС, СМО, ЛПУ)
  /// </summary>
  public class MO
  {
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Код
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
  }
}
