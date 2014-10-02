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
    public partial interface IWorkstationManager : IManagerBase<rt.srz.model.srz.Workstation, System.Guid>
    {
		// Get Methods
		IList<Workstation> GetByPointDistributionPolicyId(System.Guid organisation);
    }

    partial class WorkstationManager : ManagerBase<rt.srz.model.srz.Workstation, System.Guid>, IWorkstationManager
    {
        #region Get Methods

		
		public IList<Workstation> GetByPointDistributionPolicyId(System.Guid organisation)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Workstation));
			
			
			ICriteria organisationCriteria = criteria.CreateCriteria("Organisation");
            organisationCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", organisation));
			
			return criteria.List<Workstation>();
        }
		
		#endregion
    }
}