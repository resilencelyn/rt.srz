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
using rt.core.business.manager;
using rt.core.model.core;

namespace rt.core.business.tests
{
	[TestFixture]
    public partial class PermissionRoleTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IPermissionRoleManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.core.business.manager.IPermissionRoleManager manager;
        
        protected ISession session { get; set; }
		
		public static PermissionRole CreateNew (int depth = 0)
		{
			rt.core.model.core.PermissionRole entity = new rt.core.model.core.PermissionRole();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
      entity.FakeField = 88;
			
			using(rt.core.business.manager.IPermissionManager permissionManager = ObjectFactory.GetInstance<IPermissionManager>())
				{
           entity.Permission = null;
				}	
			
			using(rt.core.business.manager.IRoleManager roleManager = ObjectFactory.GetInstance<IRoleManager>())
				{
           entity.Role = null;
				}	
			
			return entity;
		}
		protected rt.core.model.core.PermissionRole GetFirstPermissionRole()
        {
            IList<rt.core.model.core.PermissionRole> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.core.model.core.PermissionRole entity = CreateNew();
				
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
                rt.core.model.core.PermissionRole entityA = CreateNew();
				manager.Save(entityA);

                rt.core.model.core.PermissionRole entityB = manager.GetById(entityA.Id);

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
				rt.core.model.core.PermissionRole entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.core.model.core.PermissionRole entityA = GetFirstPermissionRole();
				
				entityA.FakeField = 86;
				
				manager.Update(entityA);

                rt.core.model.core.PermissionRole entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.FakeField, entityB.FakeField);
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
			    rt.core.model.core.PermissionRole entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.core.model.core.PermissionRole entity = GetFirstPermissionRole();
				
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

