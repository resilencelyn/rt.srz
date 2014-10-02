﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text;
using NHibernate;
using NHibernate.Context;
using NUnit.Framework;
using StructureMap;
using rt.core.business.nhibernate;
using rt.core.business.registry;
using rt.core.model;
using rt.srz.business.manager;
using rt.srz.model.srz;

namespace rt.srz.business.tests
{
	[TestFixture]
    public partial class TwinTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<ITwinManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.ITwinManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.Twin CreateNewTwin()
		{
			rt.srz.model.srz.Twin entity = new rt.srz.model.srz.Twin();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			
			using(rt.srz.business.manager.IInsuredPersonManager insuredPerson1Manager = ObjectFactory.GetInstance<IInsuredPersonManager>())
				{
				    var all = insuredPerson1Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.FirstInsuredPerson = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IInsuredPersonManager insuredPerson2Manager = ObjectFactory.GetInstance<IInsuredPersonManager>())
				{
				    var all = insuredPerson2Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.SecondInsuredPerson = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager conceptManager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = conceptManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.TwinType = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.Twin GetFirstTwin()
        {
            IList<rt.srz.model.srz.Twin> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.Twin entity = CreateNewTwin();
				
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
                rt.srz.model.srz.Twin entityA = CreateNewTwin();
				manager.Save(entityA);

                rt.srz.model.srz.Twin entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA, entityB);
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
			    rt.srz.model.srz.Twin entityC = CreateNewTwin();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Twin entity = GetFirstTwin();
				
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
