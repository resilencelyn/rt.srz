// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckSrs.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The check za 7.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.external
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The check za 7.
  /// </summary>
  public class CheckSrs : Check
  {
    #region Fields

    /// <summary>
    ///   The manager statement.
    /// </summary>
    private readonly IStatementHl7Manager statementManager;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckSrs"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    /// <param name="statementManager">
    /// The statement Manager. 
    /// </param>
    public CheckSrs(ISessionFactory sessionFactory, IStatementHl7Manager statementManager)
      : base(CheckLevelEnum.External, sessionFactory)
    {
      this.statementManager = statementManager;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the caption.
    /// </summary>
    public override string Caption
    {
      get
      {
        return Resource.CaptionCheckSrs;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    public override void CheckObject(Statement statement)
    {
      ////var managerFlc = new FLCManager("Xml");
      ////var za7 = statementManager.GetZa7(statement);
      ////var person = new PersonCard { Zpi_za7 = new List<ZPI_ZA7> { za7 } };
      ////using (var stream = XmlSerializationHelper.SerializePersonCard(person))
      ////{
      ////  var responseShaper = managerFlc.Operation(stream, string.Empty, string.Empty);
      ////  var template = ObjectFactory.GetInstance<IResponseFlc>();
      ////  responseShaper.CreateResponse(template);

      ////  stream.Close();
      ////}
    }

    #endregion
  }
}