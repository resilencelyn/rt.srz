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
        
        protected rt.srz.business.manager.IPermissionRoleManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.PermissionRole CreateNewPermissionRole()
		{
			rt.srz.model.srz.PermissionRole entity = new rt.srz.model.srz.PermissionRole();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.FakeField = 54;
			
			using(rt.srz.business.manager.IPermissionManager permissionManager = ObjectFactory.GetInstance<IPermissionManager>())
				{
				    var all = permissionManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Permission = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IRoleManager roleManager = ObjectFactory.GetInstance<IRoleManager>())
				{
				    var all = roleManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Role = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.PermissionRole GetFirstPermissionRole()
        {
            IList<rt.srz.model.srz.PermissionRole> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.PermissionRole entity = CreateNewPermissionRole();
				
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
                rt.srz.model.srz.PermissionRole entityA = CreateNewPermissionRole();
				manager.Save(entityA);

                rt.srz.model.srz.PermissionRole entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.PermissionRole entityC = CreateNewPermissionRole();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.PermissionRole entityA = GetFirstPermissionRole();
				
				entityA.FakeField = 41;
				
				manager.Update(entityA);

                rt.srz.model.srz.PermissionRole entityB = manager.GetById(entityA.Id);

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
			    rt.srz.model.srz.PermissionRole entityC = CreateNewPermissionRole();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.PermissionRole entity = GetFirstPermissionRole();
				
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

