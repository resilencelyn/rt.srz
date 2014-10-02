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
    public partial interface IOrganisationManager : IManagerBase<rt.srz.model.srz.Organisation, System.Guid>
    {
		// Get Methods
		IList<Organisation> GetByOid(System.String oid);
		IList<Organisation> GetByParentId(System.Guid organisationMember1);
		IList<Organisation> GetByChangedRowId(System.Guid organisationMember2);
		IList<Organisation> GetByCauseRegistrationId(System.Int32 concept1);
		IList<Organisation> GetByCauseExclusionId(System.Int32 concept2);
		IList<Organisation> GetByCode(System.String code);
    }

    partial class OrganisationManager : ManagerBase<rt.srz.model.srz.Organisation, System.Guid>, IOrganisationManager
    {
        #region Get Methods

		
		public IList<Organisation> GetByOid(System.String oid)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Organisation));
			
			
			ICriteria oidCriteria = criteria.CreateCriteria("Oid");
            oidCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", oid));
			
			return criteria.List<Organisation>();
        }
		
		public IList<Organisation> GetByParentId(System.Guid organisationMember1)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Organisation));
			
			
			ICriteria organisationMember1Criteria = criteria.CreateCriteria("OrganisationMember1");
            organisationMember1Criteria.Add(NHibernate.Criterion.Expression.Eq("Id", organisationMember1));
			
			return criteria.List<Organisation>();
        }
		
		public IList<Organisation> GetByChangedRowId(System.Guid organisationMember2)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Organisation));
			
			
			ICriteria organisationMember2Criteria = criteria.CreateCriteria("OrganisationMember2");
            organisationMember2Criteria.Add(NHibernate.Criterion.Expression.Eq("Id", organisationMember2));
			
			return criteria.List<Organisation>();
        }
		
		public IList<Organisation> GetByCauseRegistrationId(System.Int32 concept1)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Organisation));
			
			
			ICriteria concept1Criteria = criteria.CreateCriteria("Concept1");
            concept1Criteria.Add(NHibernate.Criterion.Expression.Eq("Id", concept1));
			
			return criteria.List<Organisation>();
        }
		
		public IList<Organisation> GetByCauseExclusionId(System.Int32 concept2)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Organisation));
			
			
			ICriteria concept2Criteria = criteria.CreateCriteria("Concept2");
            concept2Criteria.Add(NHibernate.Criterion.Expression.Eq("Id", concept2));
			
			return criteria.List<Organisation>();
        }
		
		public IList<Organisation> GetByCode(System.String code)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Organisation));
			
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("Code", code));
			
			return criteria.List<Organisation>();
        }
		
		#endregion
    }
}