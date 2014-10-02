using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rt.srz.model.HL7.person;
using rt.srz.model.HL7.person.messages;
using rt.srz.model.srz;

namespace rt.srz.business.manager
{
  public interface IStatementADT_A01Manager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Выгружает ADT_A01 для выполнения ФЛК с помощью шлюза РС
    /// </summary>
    /// <param name="statement"></param>
    void Export_ADT_A01_ForFLK(Batch batch, Statement statement);

    /// <summary>
    /// Создает бачт и сообщение в БД
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    Batch CreateBatchForExportADT_A01(Statement statement);
    
    

    #endregion
  }
}
