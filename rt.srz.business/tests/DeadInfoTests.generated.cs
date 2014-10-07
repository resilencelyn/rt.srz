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
using rt.srz.business.manager;
using rt.srz.model.srz;

namespace rt.srz.business.tests
{
	[TestFixture]
    public partial class DeadInfoTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IDeadInfoManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
        }
        
        protected rt.srz.business.manager.IDeadInfoManager manager;
        
        protected ISession session { get; set; }
		
		public static DeadInfo CreateNew (int depth = 0)
		{
			rt.srz.model.srz.DeadInfo entity = new rt.srz.model.srz.DeadInfo();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
      entity.DateDead = System.DateTime.Now;
      entity.ActRecordDate = System.DateTime.Now;
      entity.ActRecordNumber = 57;
			
			return entity;
		}
		protected rt.srz.model.srz.DeadInfo GetFirstDeadInfo()
        {
            IList<rt.srz.model.srz.DeadInfo> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.DeadInfo entity = CreateNew();
				
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
                rt.srz.model.srz.DeadInfo entityA = CreateNew();
				manager.Save(entityA);

                rt.srz.model.srz.DeadInfo entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.DeadInfo entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.DeadInfo entityA = GetFirstDeadInfo();
				
				entityA.DateDead = System.DateTime.Now;
				
				manager.Update(entityA);

                rt.srz.model.srz.DeadInfo entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.DateDead, entityB.DateDead);
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
			    rt.srz.model.srz.DeadInfo entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.DeadInfo entity = GetFirstDeadInfo();
				
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

