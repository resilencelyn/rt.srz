
namespace rt.srz.model.srz.concepts
{
  /// <summary> Статус застрахованного </summary>
  public class StatusPerson : Concept
  {
    /// <summary> Застрахован </summary>
    public const int Active = 465;

    /// <summary> Аннулирован </summary>
    public const int Annuled = 466;

    /// <summary> Умерший </summary>
    public const int Dead = 467;
  }
}
