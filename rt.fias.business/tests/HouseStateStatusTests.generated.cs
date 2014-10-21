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
using NHibernate.Context;
using NUnit.Framework;
using StructureMap;
using rt.core.business.nhibernate;
using rt.core.business.registry;
using rt.core.model;
using rt.fias.business.manager;
using rt.fias.model.fias;

namespace rt.fias.business.tests
{
	[TestFixture]
    public partial class HouseStateStatusTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IHouseStateStatusManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
        }
        
        protected rt.fias.business.manager.IHouseStateStatusManager manager;
        
        protected ISession session { get; set; }
		
		public static HouseStateStatus CreateNew (int depth = 0)
		{
			rt.fias.model.fias.HouseStateStatus entity = new rt.fias.model.fias.HouseStateStatus();
			
			
      entity.Name = "Test Test Test Test Test Test Test Test Test Test Test T";
			
			return entity;
		}
		protected rt.fias.model.fias.HouseStateStatus GetFirstHouseStateStatus()
        {
            IList<rt.fias.model.fias.HouseStateStatus> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.fias.model.fias.HouseStateStatus entity = CreateNew();
				
                object result = manager.Save(entity);

                Assert.IsNotNull(result);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        [Test]
        public void Read()
        {
            try
            {
                rt.fias.model.fias.HouseStateStatus entityA = CreateNew();
				manager.Save(entityA);

                rt.fias.model.fias.HouseStateStatus entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA, entityB);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
		[Test]
		public void Update()
        {
            try
            {
				rt.fias.model.fias.HouseStateStatus entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.fias.model.fias.HouseStateStatus entityA = GetFirstHouseStateStatus();
				
				entityA.Name = "Test Test Test Test Test Test Tes";
				
				manager.Update(entityA);

                rt.fias.model.fias.HouseStateStatus entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Name, entityB.Name);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        [Test]
        public void Delete()
        {
            try
            {
			    rt.fias.model.fias.HouseStateStatus entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.fias.model.fias.HouseStateStatus entity = GetFirstHouseStateStatus();
				
                manager.Delete(entity);

                entity = manager.GetById(entity.Id);
                Assert.IsNull(entity);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
	}
}

