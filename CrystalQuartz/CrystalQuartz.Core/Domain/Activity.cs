namespace CrystalQuartz.Core.Domain
{
  public class Activity : NamedObject
  {
    public Activity(string name, ActivityStatus status)
      : base(name)
    {
      Status = status;
    }

    public Activity(string name)
      : base(name)
    {
    }

    public virtual void Init() { }

    public ActivityStatus Status { get; protected set; }

    public string StatusDecription
    {
      get
      {
        switch (Status)
        {
          case ActivityStatus.Active:
            return "�������";
          case ActivityStatus.Complete:
            return "���������";
          case ActivityStatus.Mixed:
            return "��������";
          case ActivityStatus.Paused:
            return "��������������";
          default:
            return null;
        }
      }
    }

    public bool CanStart
    {
      get
      {
        return Status == ActivityStatus.Paused || Status == ActivityStatus.Mixed;
      }
    }

    public bool CanPause
    {
      get
      {
        return Status == ActivityStatus.Active || Status == ActivityStatus.Mixed;
      }
    }
  }
}