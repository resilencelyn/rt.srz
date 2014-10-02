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
    public partial interface ISearchKeyTypeManager : IManagerBase<rt.srz.model.srz.SearchKeyType, System.Guid>
    {
		// Get Methods
		IList<SearchKeyType> GetByOperationKeyId(System.Int32 concept);
		IList<SearchKeyType> GetByTfomsId(System.Guid organisation);
    }

    partial class SearchKeyTypeManager : ManagerBase<rt.srz.model.srz.SearchKeyType, System.Guid>, ISearchKeyTypeManager
    {
        #region Get Methods

		
		public IList<SearchKeyType> GetByOperationKeyId(System.Int32 concept)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(SearchKeyType));
			
			
			ICriteria conceptCriteria = criteria.CreateCriteria("Concept");
            conceptCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", concept));
			
			return criteria.List<SearchKeyType>();
        }
		
		public IList<SearchKeyType> GetByTfomsId(System.Guid organisation)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(SearchKeyType));
			
			
			ICriteria organisationCriteria = criteria.CreateCriteria("Organisation");
            organisationCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", organisation));
			
			return criteria.List<SearchKeyType>();
        }
		
		#endregion
    }
}