using rt.srz.model.srz;

namespace rt.srz.ui.pvp.Controls.StatementSelectionWizardSteps
{
    public interface IWizardStep
    {
      /// <summary>
      /// Переносит данные из объекта в элементы на форме
      /// </summary>
      /// <param name="obj"></param>
      void MoveDataFromObject2GUI(Statement statement);

      /// <summary>
      /// Переносит данные из элементов на форме в объект
      /// </summary>
      /// <param name="statement">
      /// The statement. 
      /// </param>
      /// </summary>
      /// <param name="setCurrentStatement">
      /// Обновлять ли свойство CurrentStatement после присвоения заявлению данных из дизайна 
      /// </param>
      void MoveDataFromGui2Object(ref Statement statement, bool setCurrentStatement = true);

      /// <summary>
      /// Проверка доступности элементов редактирования для ввода
      /// </summary>
      void CheckIsRightToEdit();
    }
}
