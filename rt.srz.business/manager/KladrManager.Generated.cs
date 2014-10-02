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
    public partial interface IKladrManager : IManagerBase<rt.srz.model.srz.Kladr, System.Guid>
    {
		// Get Methods
		IList<Kladr> GetByKLADR_PARENT_ID(System.Guid kladrMember);
		IList<Kladr> GetByFULL_ADDRESS(System.String fullAddress);
		Kladr GetByNAMEID(System.String name, System.Guid id);
		IList<Kladr> GetByCODE(System.String code);
		IList<Kladr> GetByNAME(System.String name);
		IList<Kladr> GetByOCATD(System.String ocatd);
		IList<Kladr> GetBySTATUS(System.Int32 status);
    }

    partial class KladrManager : ManagerBase<rt.srz.model.srz.Kladr, System.Guid>, IKladrManager
    {
        #region Get Methods

		
		public IList<Kladr> GetByKLADR_PARENT_ID(System.Guid kladrMember)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Kladr));
			
			
			ICriteria kladrMemberCriteria = criteria.CreateCriteria("KladrMember");
            kladrMemberCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", kladrMember));
			
			return criteria.List<Kladr>();
        }
		
		public IList<Kladr> GetByFULL_ADDRESS(System.String fullAddress)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Kladr));
			
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("FullAddress", fullAddress));
			
			return criteria.List<Kladr>();
        }
		
		public Kladr GetByNAMEID(System.String name, System.Guid id)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Kladr));
			
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("Name", name));
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("Id", id));
			
			IList<Kladr> result = criteria.List<Kladr>();
			return (result.Count > 0) ? result[0] : null;
        }
		
		public IList<Kladr> GetByCODE(System.String code)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Kladr));
			
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("Code", code));
			
			return criteria.List<Kladr>();
        }
		
		public IList<Kladr> GetByNAME(System.String name)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Kladr));
			
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("Name", name));
			
			return criteria.List<Kladr>();
        }
		
		public IList<Kladr> GetByOCATD(System.String ocatd)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Kladr));
			
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("Ocatd", ocatd));
			
			return criteria.List<Kladr>();
        }
		
		public IList<Kladr> GetBySTATUS(System.Int32 status)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Kladr));
			
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("Status", status));
			
			return criteria.List<Kladr>();
        }
		
		#endregion
    }
}