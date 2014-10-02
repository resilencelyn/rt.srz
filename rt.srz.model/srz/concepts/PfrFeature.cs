namespace rt.srz.model.srz.concepts
{
  /// <summary> Признак идентификации застрахованного лица в ОПФР </summary>
  public class PfrFeature : Concept
  {
    /// <summary> СНИЛС   в   ОПФР найден,   данные   по застрахованному  лицу соответствуют </summary>
    public const int PfrFeature1 = 555;

    /// <summary> СНИЛС   в   ОПФР найден, но данные  по застрахованному  лицу не соответствуют </summary>
    public const int PfrFeature2 = 556;

    /// <summary> Дополнительные сведения  по застрахованным лицам, не включенным в  файл ТФОМС </summary>
    public const int PfrFeature3 = 557;
  }
}

