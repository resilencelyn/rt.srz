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
    public partial class UserGroupRoleTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IUserGroupRoleManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.core.business.manager.IUserGroupRoleManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.core.model.core.UserGroupRole CreateNewUserGroupRole()
		{
			rt.core.model.core.UserGroupRole entity = new rt.core.model.core.UserGroupRole();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			
			using(rt.core.business.manager.IGroupManager groupManager = ObjectFactory.GetInstance<IGroupManager>())
				{
				    var all = groupManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Group = all[0];
					}
				}	
			
			using(rt.core.business.manager.IRoleManager roleManager = ObjectFactory.GetInstance<IRoleManager>())
				{
				    var all = roleManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Role = all[0];
					}
				}	
			
			using(rt.core.business.manager.IUserManager userManager = ObjectFactory.GetInstance<IUserManager>())
				{
				    var all = userManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.User = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.core.model.core.UserGroupRole GetFirstUserGroupRole()
        {
            IList<rt.core.model.core.UserGroupRole> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.core.model.core.UserGroupRole entity = CreateNewUserGroupRole();
				
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
                rt.core.model.core.UserGroupRole entityA = CreateNewUserGroupRole();
				manager.Save(entityA);

                rt.core.model.core.UserGroupRole entityB = manager.GetById(entityA.Id);

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
			    rt.core.model.core.UserGroupRole entityC = CreateNewUserGroupRole();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.core.model.core.UserGroupRole entity = GetFirstUserGroupRole();
				
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

