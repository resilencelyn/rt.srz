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
    public partial class UserTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IUserManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.core.business.manager.IUserManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.core.model.core.User CreateNewUser()
		{
			rt.core.model.core.User entity = new rt.core.model.core.User();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Login = "Test Test Test Test";
			entity.Password = "Test Test Test Tes";
			entity.Email = "Test T";
			entity.Salt = "Test Test Test Test Test Test ";
			entity.CreationDate = System.DateTime.Now;
			entity.LastLoginDate = System.DateTime.Now;
			entity.IsApproved = true;
			entity.Fio = "Test Test ";
			entity.PointDistributionPolicyId = System.Guid.NewGuid();
			entity.UserId1 = System.Guid.NewGuid();
			entity.UserId2 = System.Guid.NewGuid();
			entity.UserId3 = System.Guid.NewGuid();
			
			return entity;
		}
		protected rt.core.model.core.User GetFirstUser()
        {
            IList<rt.core.model.core.User> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.core.model.core.User entity = CreateNewUser();
				
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
                rt.core.model.core.User entityA = CreateNewUser();
				manager.Save(entityA);

                rt.core.model.core.User entityB = manager.GetById(entityA.Id);

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
				rt.core.model.core.User entityC = CreateNewUser();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.core.model.core.User entityA = GetFirstUser();
				
				entityA.Login = "Test Test Test Test Test Test Test Test T";
				
				manager.Update(entityA);

                rt.core.model.core.User entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Login, entityB.Login);
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
			    rt.core.model.core.User entityC = CreateNewUser();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.core.model.core.User entity = GetFirstUser();
				
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

