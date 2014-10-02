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
    public partial interface ISertificateUecManager : IManagerBase<rt.srz.model.srz.SertificateUec, System.Guid>
    {
		// Get Methods
		IList<SertificateUec> GetByTypeId(System.Int32 concept);
		IList<SertificateUec> GetBySmoId(System.Guid organisation);
		IList<SertificateUec> GetByWorkstationId(System.Guid workstation);
    }

    partial class SertificateUecManager : ManagerBase<rt.srz.model.srz.SertificateUec, System.Guid>, ISertificateUecManager
    {
        #region Get Methods

		
		public IList<SertificateUec> GetByTypeId(System.Int32 concept)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(SertificateUec));
			
			
			ICriteria conceptCriteria = criteria.CreateCriteria("Concept");
            conceptCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", concept));
			
			return criteria.List<SertificateUec>();
        }
		
		public IList<SertificateUec> GetBySmoId(System.Guid organisation)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(SertificateUec));
			
			
			ICriteria organisationCriteria = criteria.CreateCriteria("Organisation");
            organisationCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", organisation));
			
			return criteria.List<SertificateUec>();
        }
		
		public IList<SertificateUec> GetByWorkstationId(System.Guid workstation)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(SertificateUec));
			
			
			ICriteria workstationCriteria = criteria.CreateCriteria("Workstation");
            workstationCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", workstation));
			
			return criteria.List<SertificateUec>();
        }
		
		#endregion
    }
}