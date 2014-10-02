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
    public partial interface IImManager : IManagerBase<rt.atl.model.atl.Im, int>
    {
		// Get Methods
		IList<Im> GetByW(System.Int32 w);
    }

    partial class ImManager : ManagerBase<rt.atl.model.atl.Im, int>, IImManager
    {
        #region Get Methods

		
		public IList<Im> GetByW(System.Int32 w)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(Im));
			
			
			criteria.Add(NHibernate.Criterion.Expression.Eq("W", w));
			
			return criteria.List<Im>();
        }
		
		#endregion
    }
}