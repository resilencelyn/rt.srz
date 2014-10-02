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
    public partial interface IIn1Manager : IManagerBase<rt.srz.model.srz.In1, System.Guid>
    {
		// Get Methods
		IList<In1> GetByQueryResponseId(System.Guid queryResponse);
		IList<In1> GetByPolisTypeId(System.Int32 concept);
		IList<In1> GetBySmoId(System.Guid organisation);
		IList<In1> GetByPolisSeriaPolisNumber(System.String polisSeria, System.String polisNumber);
    }

    partial class In1Manager : ManagerBase<rt.srz.model.srz.In1, System.Guid>, IIn1Manager
    {
        #region Get Methods

		
		public IList<In1> GetByQueryResponseId(System.Guid queryResponse)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(In1));
			
			
			ICriteria queryResponseCriteria = criteria.CreateCriteria("QueryResponse");
            queryResponseCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", queryResponse));
			
			return criteria.List<In1>();
        }
		
		public IList<In1> GetByPolisTypeId(System.Int32 concept)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(In1));
			
			
			ICriteria conceptCriteria = criteria.CreateCriteria("Concept");
            conceptCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", concept));
			
			return criteria.List<In1>();
        }
		
		public IList<In1> GetBySmoId(System.Guid organisation)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(In1));
			
			
			ICriteria organisationCriteria = criteria.CreateCriteria("Organisation");
            organisationCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", organisation));
			
			return criteria.List<In1>();
        }
		
		public IList<In1> GetByPolisSeriaPolisNumber(System.String polisSeria, System.String polisNumber)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(In1));
			
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("PolisSeria", polisSeria));
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("PolisNumber", polisNumber));
			
			return criteria.List<In1>();
        }
		
		#endregion
    }
}