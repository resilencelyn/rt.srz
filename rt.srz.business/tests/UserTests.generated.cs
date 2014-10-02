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
        
        protected rt.srz.business.manager.IUserManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.User CreateNewUser()
		{
			rt.srz.model.srz.User entity = new rt.srz.model.srz.User();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Login = "Test Tes";
			entity.Password = "Test Test T";
			entity.Email = "Test Test Test Test Te";
			entity.Salt = "Test Test Test Test Test Test Test Test ";
			entity.CreationDate = System.DateTime.Now;
			entity.LastLoginDate = System.DateTime.Now;
			entity.IsApproved = true;
			entity.Fio = "Test Test ";
			
			using(rt.srz.business.manager.IOrganisationManager organisationManager = ObjectFactory.GetInstance<IOrganisationManager>())
				{
				    var all = organisationManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.PointDistributionPolicy = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.User GetFirstUser()
        {
            IList<rt.srz.model.srz.User> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.User entity = CreateNewUser();
				
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
                rt.srz.model.srz.User entityA = CreateNewUser();
				manager.Save(entityA);

                rt.srz.model.srz.User entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.User entityC = CreateNewUser();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.User entityA = GetFirstUser();
				
				entityA.Login = "Test Test Test Test Te";
				
				manager.Update(entityA);

                rt.srz.model.srz.User entityB = manager.GetById(entityA.Id);

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
			    rt.srz.model.srz.User entityC = CreateNewUser();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.User entity = GetFirstUser();
				
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

