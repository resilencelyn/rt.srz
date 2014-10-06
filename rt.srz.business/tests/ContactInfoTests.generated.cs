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
    public partial class ContactInfoTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IContactInfoManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IContactInfoManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.ContactInfo CreateNewContactInfo()
		{
			rt.srz.model.srz.ContactInfo entity = new rt.srz.model.srz.ContactInfo();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.HomePhone = "Test Test Test Test Test Test Test Test T";
			entity.WorkPhone = "Test Test Test Test Test Tes";
			entity.Email = "Test Test Test Test Test";
			
			return entity;
		}
		protected rt.srz.model.srz.ContactInfo GetFirstContactInfo()
        {
            IList<rt.srz.model.srz.ContactInfo> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.ContactInfo entity = CreateNewContactInfo();
				
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
                rt.srz.model.srz.ContactInfo entityA = CreateNewContactInfo();
				manager.Save(entityA);

                rt.srz.model.srz.ContactInfo entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.ContactInfo entityC = CreateNewContactInfo();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.ContactInfo entityA = GetFirstContactInfo();
				
				entityA.HomePhone = "Test Test Test Test Test Test Test";
				
				manager.Update(entityA);

                rt.srz.model.srz.ContactInfo entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.HomePhone, entityB.HomePhone);
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
			    rt.srz.model.srz.ContactInfo entityC = CreateNewContactInfo();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.ContactInfo entity = GetFirstContactInfo();
				
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

