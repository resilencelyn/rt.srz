namespace rt.srz.business.manager.rightedit
{
  using System;
  using System.Linq.Expressions;

  using rt.srz.model.srz;

  /// <summary>
  /// The statement not edit.
  /// </summary>
  public class StatementNotEdit : StatementRightToEdit
  {
    public StatementNotEdit()
      : base(-10)
    {
    }

    /// <summary>
    /// The is edit.
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool IsEdit(Expression<Func<Statement, object>> expression)
    {
      return true;
    }
  }
}