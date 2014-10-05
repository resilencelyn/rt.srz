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
    public partial class NumberPolicyCounterTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<INumberPolicyCounterManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.INumberPolicyCounterManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.NumberPolicyCounter CreateNewNumberPolicyCounter()
		{
			rt.srz.model.srz.NumberPolicyCounter entity = new rt.srz.model.srz.NumberPolicyCounter();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = "Test T";
			
			entity.CurrentNumber = 80;
			
			return entity;
		}
		protected rt.srz.model.srz.NumberPolicyCounter GetFirstNumberPolicyCounter()
        {
            IList<rt.srz.model.srz.NumberPolicyCounter> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.NumberPolicyCounter entity = CreateNewNumberPolicyCounter();
				
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
                rt.srz.model.srz.NumberPolicyCounter entityA = CreateNewNumberPolicyCounter();
				manager.Save(entityA);

                rt.srz.model.srz.NumberPolicyCounter entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.NumberPolicyCounter entityC = CreateNewNumberPolicyCounter();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.NumberPolicyCounter entityA = GetFirstNumberPolicyCounter();
				
				entityA.CurrentNumber = 25;
				
				manager.Update(entityA);

                rt.srz.model.srz.NumberPolicyCounter entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.CurrentNumber, entityB.CurrentNumber);
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
			    rt.srz.model.srz.NumberPolicyCounter entityC = CreateNewNumberPolicyCounter();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.NumberPolicyCounter entity = GetFirstNumberPolicyCounter();
				
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

