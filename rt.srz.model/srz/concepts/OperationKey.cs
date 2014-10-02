namespace rt.srz.model.srz.concepts
{
  /// <summary> Тип использования ключа поиска </summary>
  public class OperationKey : Concept
  {
    /// <summary> Ключ центрального сегмента </summary>
    public const int CentralSegmentKey = 540;

    /// <summary> Ключ для поиска дубликата при полном сканировании и сохранении </summary>
    public const int FullScanAndSaveKey = 541;

    /// <summary> Ключ для поиска дубликата при полном сканировании </summary>
    public const int FullScanKey = 542;
  }
}

