using System;
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
    public partial class JobLockObjectTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IJobLockObjectManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IJobLockObjectManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.JobLockObject CreateNewJobLockObject()
		{
			rt.srz.model.srz.JobLockObject entity = new rt.srz.model.srz.JobLockObject();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = 86;
			
			entity.Versions = 42;
			
			return entity;
		}
		protected rt.srz.model.srz.JobLockObject GetFirstJobLockObject()
        {
            IList<rt.srz.model.srz.JobLockObject> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.JobLockObject entity = CreateNewJobLockObject();
				
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
                rt.srz.model.srz.JobLockObject entityA = CreateNewJobLockObject();
				manager.Save(entityA);

                rt.srz.model.srz.JobLockObject entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.JobLockObject entityC = CreateNewJobLockObject();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.JobLockObject entityA = GetFirstJobLockObject();
				
				
				manager.Update(entityA);

                rt.srz.model.srz.JobLockObject entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Versions, entityB.Versions);
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
			    rt.srz.model.srz.JobLockObject entityC = CreateNewJobLockObject();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.JobLockObject entity = GetFirstJobLockObject();
				
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

