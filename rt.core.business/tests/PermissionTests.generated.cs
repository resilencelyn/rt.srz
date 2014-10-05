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
using rt.core.business.manager;
using rt.core.model.core;

namespace rt.core.business.tests
{
	[TestFixture]
    public partial class PermissionTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IPermissionManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.core.business.manager.IPermissionManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.core.model.core.Permission CreateNewPermission()
		{
			rt.core.model.core.Permission entity = new rt.core.model.core.Permission();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Name = "Test Test ";
			entity.Code = 9;
			
			return entity;
		}
		protected rt.core.model.core.Permission GetFirstPermission()
        {
            IList<rt.core.model.core.Permission> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.core.model.core.Permission entity = CreateNewPermission();
				
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
                rt.core.model.core.Permission entityA = CreateNewPermission();
				manager.Save(entityA);

                rt.core.model.core.Permission entityB = manager.GetById(entityA.Id);

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
				rt.core.model.core.Permission entityC = CreateNewPermission();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.core.model.core.Permission entityA = GetFirstPermission();
				
				entityA.Name = "Test Test ";
				
				manager.Update(entityA);

                rt.core.model.core.Permission entityB = manager.GetById(entityA.Id);

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
			    rt.core.model.core.Permission entityC = CreateNewPermission();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.core.model.core.Permission entity = GetFirstPermission();
				
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

