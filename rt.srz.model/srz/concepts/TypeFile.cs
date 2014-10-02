namespace rt.srz.model.srz.concepts
{
  /// <summary> Тип файла взаимодействия </summary>
  public class TypeFile : Concept
  {
    /// <summary> Файл ПФР с СНИЛС </summary>
    public const int PfrSnils = 548;
    
    /// <summary> Файл ПФР с расширенными данными </summary>
    public const int PfrData = 549;

    /// <summary> Файл ЗАГС </summary>
    public const int Zags = 602;

    /// <summary> Файл корректировки данных от ТФОМС в СМО </summary>
    public const int Rec = 628;

    /// <summary> Файл с изменениями от СМО в ТФОМС </summary>
    public const int Op = 645;

    /// <summary> Файл подтверждения/отклонения изменений: протокол обработки файла с изменениями от СМО в ТФОМС</summary>
    public const int Rep = 647;

    /// <summary> Файл протокола форматно-логического контроля</summary>
    public const int Flk = 648;

    /// <summary> Файл c изменениями в ЦС </summary>
    public const int PersonErp = 649;
  }
}

