﻿//------------------------------------------------------------------------------
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
using rt.fias.model.fias;

namespace rt.fias.business.manager
{
    public partial interface IHouseManager : IManagerBase<rt.fias.model.fias.House, System.Guid>
    {
		// Get Methods
		IList<House> GetByESTSTATUS(System.Int32 estateStatus);
		IList<House> GetBySTATSTATUS(System.Int32 houseStateStatus);
		IList<House> GetBySTRSTATUS(System.Int32 structureStatus);
    
    }

    partial class HouseManager : ManagerBase<rt.fias.model.fias.House, System.Guid>, IHouseManager
    {
        #region Get Methods

		
		public IList<House> GetByESTSTATUS(System.Int32 estateStatus)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(House));
			
			
			ICriteria estateStatusCriteria = criteria.CreateCriteria("EstateStatus");
            estateStatusCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", estateStatus));
			
			return criteria.List<House>();
        }
		
		public IList<House> GetBySTATSTATUS(System.Int32 houseStateStatus)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(House));
			
			
			ICriteria houseStateStatusCriteria = criteria.CreateCriteria("HouseStateStatus");
            houseStateStatusCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", houseStateStatus));
			
			return criteria.List<House>();
        }
		
		public IList<House> GetBySTRSTATUS(System.Int32 structureStatus)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(House));
			
			
			ICriteria structureStatusCriteria = criteria.CreateCriteria("StructureStatus");
            structureStatusCriteria.Add(NHibernate.Criterion.Expression.Eq("Id", structureStatus));
			
			return criteria.List<House>();
        }
		
		#endregion
    }
}