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
    public partial interface IOkatoManager : IManagerBase<rt.atl.model.atl.Okato, int>
    {
		// Get Methods
		Okato GetByCODE(System.String code);
		IList<Okato> GetByDEDIT(System.DateTime dedit);
    }

    partial class OkatoManager : ManagerBase<rt.atl.model.atl.Okato, int>, IOkatoManager
    {
        #region Get Methods

		
		public Okato GetByCODE(System.String code)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Okato));
			
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("Code", code));
			
			IList<Okato> result = criteria.List<Okato>();
			return (result.Count > 0) ? result[0] : null;
        }
		
		public IList<Okato> GetByDEDIT(System.DateTime dedit)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Okato));
			
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("Dedit", dedit));
			
			return criteria.List<Okato>();
        }
		
		#endregion
    }
}