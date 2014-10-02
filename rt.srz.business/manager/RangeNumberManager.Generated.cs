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
    public partial interface IRangeNumberManager : IManagerBase<rt.srz.model.srz.RangeNumber, System.Guid>
    {
		// Get Methods
		IList<RangeNumber> GetByParentId(System.Guid rangeNumberMember);
		IList<RangeNumber> GetBySmoId(System.Guid organisation);
		IList<RangeNumber> GetByTemplateId(System.Guid template);
    }

    partial class RangeNumberManager : ManagerBase<rt.srz.model.srz.RangeNumber, System.Guid>, IRangeNumberManager
    {
        #region Get Methods

		
		public IList<RangeNumber> GetByParentId(System.Guid rangeNumberMember)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(RangeNumber));
			
			
			ICriteria rangeNumberMemberCriteria = criteria.CreateCriteria("RangeNumberMember");
            rangeNumberMemberCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", rangeNumberMember));
			
			return criteria.List<RangeNumber>();
        }
		
		public IList<RangeNumber> GetBySmoId(System.Guid organisation)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(RangeNumber));
			
			
			ICriteria organisationCriteria = criteria.CreateCriteria("Organisation");
            organisationCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", organisation));
			
			return criteria.List<RangeNumber>();
        }
		
		public IList<RangeNumber> GetByTemplateId(System.Guid template)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(RangeNumber));
			
			
			ICriteria templateCriteria = criteria.CreateCriteria("Template");
            templateCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", template));
			
			return criteria.List<RangeNumber>();
        }
		
		#endregion
    }
}