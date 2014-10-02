using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rt.srz.ui.pvp.Controls
{
    public interface IKLADRUserControl
    {
        //Текущее значение идентификатора и базы КЛАДР
        Guid SelectedKLADRId
        {
            get;
            set;
        }
        
        /// <summary>
        /// Устанавливает значение региона по умолчанию
        /// </summary>
        void SetDefaultSubject();
    }
}
