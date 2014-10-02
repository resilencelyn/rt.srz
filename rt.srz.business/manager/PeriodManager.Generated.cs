//------------------------------------------------------------------------------
// <auto-generated>
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using rt.core.business.nhibernate;
using rt.srz.model.srz;

namespace rt.srz.business.manager
{
    public partial interface IPeriodManager : IManagerBase<rt.srz.model.srz.Period, System.Guid>
    {
		// Get Methods
		IList<Period> GetByCodeId(System.Int32 concept);
		IList<Period> GetByYear(System.DateTime year);
    }

    partial class PeriodManager : ManagerBase<rt.srz.model.srz.Period, System.Guid>, IPeriodManager
    {
        #region Get Methods

		
		public IList<Period> GetByCodeId(System.Int32 concept)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Period));
			
			
			ICriteria conceptCriteria = criteria.CreateCriteria("Concept");
            conceptCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", concept));
			
			return criteria.List<Period>();
        }
		
		public IList<Period> GetByYear(System.DateTime year)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Period));
			
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("Year", year));
			
			return criteria.List<Period>();
        }
		
		#endregion
    }
}