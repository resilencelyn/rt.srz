using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Context;
using NUnit.Framework;
using StructureMap;
using rt.core.business.nhibernate;
using rt.core.business.registry;
using rt.atl.business.manager;
using rt.atl.model.atl;

namespace rt.atl.business.tests
{
  using rt.core.model;

  [TestFixture]
    public partial class TestprocTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<ITestprocManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.atl.business.manager.ITestprocManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.atl.model.atl.Testproc CreateNewTestproc()
		{
			rt.atl.model.atl.Testproc entity = new rt.atl.model.atl.Testproc();
			
			
			entity.Dedit = System.DateTime.Now;
			entity.Caption = "Test Test ";
			entity.Code = "T";
			entity.Strong = true;
			entity.Act = true;
			entity.Srv = true;
			entity.Procname = "Test Tes";
			entity.Nosrop = true;
			entity.Flds = "Test Test Test Test Test T";
			
			return entity;
		}
		protected rt.atl.model.atl.Testproc GetFirstTestproc()
        {
            IList<rt.atl.model.atl.Testproc> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.atl.model.atl.Testproc entity = CreateNewTestproc();
				
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
                rt.atl.model.atl.Testproc entityA = CreateNewTestproc();
				manager.Save(entityA);

                rt.atl.model.atl.Testproc entityB = manager.GetById(entityA.Id);

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
				rt.atl.model.atl.Testproc entityC = CreateNewTestproc();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Testproc entityA = GetFirstTestproc();
				
				entityA.Dedit = System.DateTime.Now;
				
				manager.Update(entityA);

                rt.atl.model.atl.Testproc entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Dedit, entityB.Dedit);
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
			    rt.atl.model.atl.Testproc entityC = CreateNewTestproc();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Testproc entity = GetFirstTestproc();
				
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

