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
using rt.atl.model.atl;

namespace rt.atl.business.manager
{
    public partial interface IUechiststatusManager : IManagerBase<rt.atl.model.atl.Uechiststatus, int>
    {
		// Get Methods
		IList<Uechiststatus> GetByUEC(System.Int32 uec);
		IList<Uechiststatus> GetByUECSTATUS(System.String uecstatus);
		IList<Uechiststatus> GetByUFILE(System.Int32 ufile);
    }

    partial class UechiststatusManager : ManagerBase<rt.atl.model.atl.Uechiststatus, int>, IUechiststatusManager
    {
        #region Get Methods

		
		public IList<Uechiststatus> GetByUEC(System.Int32 uec)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Uechiststatus));
			
			
			ICriteria uecCriteria = criteria.CreateCriteria("Uec");
            uecCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", uec));
			
			return criteria.List<Uechiststatus>();
        }
		
		public IList<Uechiststatus> GetByUECSTATUS(System.String uecstatus)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Uechiststatus));
			
			
			ICriteria uecstatusCriteria = criteria.CreateCriteria("Uecstatus");
            uecstatusCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", uecstatus));
			
			return criteria.List<Uechiststatus>();
        }
		
		public IList<Uechiststatus> GetByUFILE(System.Int32 ufile)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Uechiststatus));
			
			
			ICriteria ufileCriteria = criteria.CreateCriteria("Ufile");
            ufileCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", ufile));
			
			return criteria.List<Uechiststatus>();
        }
		
		#endregion
    }
}