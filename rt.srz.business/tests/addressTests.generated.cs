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
    public partial class addressTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IaddressManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IaddressManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.address CreateNewaddress()
		{
			rt.srz.model.srz.address entity = new rt.srz.model.srz.address();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.IsHomeless = true;
			entity.Postcode = "Test Te";
			entity.Subject = "Te";
			entity.Area = "Test Test Test Test Test Test Test Test T";
			entity.City = "Test Test Test Test Test Test Test Te";
			entity.Town = "Test Test Test Test Test T";
			entity.Street = "Test Test Test Test Test Test Test";
			entity.House = "Test T";
			entity.Housing = "Test Test Test T";
			entity.Room = default(Int16);
			entity.DateRegistration = System.DateTime.Now;
			entity.IsNotStructureAddress = true;
			entity.Okato = "Test Test Test Te";
			entity.Unstructured = "Test Test ";
			
			using(rt.srz.business.manager.IKladrManager kladrManager = ObjectFactory.GetInstance<IKladrManager>())
				{
				    var all = kladrManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Kladr = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.address GetFirstaddress()
        {
            IList<rt.srz.model.srz.address> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.address entity = CreateNewaddress();
				
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
                rt.srz.model.srz.address entityA = CreateNewaddress();
				manager.Save(entityA);

                rt.srz.model.srz.address entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.address entityC = CreateNewaddress();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.address entityA = GetFirstaddress();
				
				entityA.IsHomeless = true;
				
				manager.Update(entityA);

                rt.srz.model.srz.address entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.IsHomeless, entityB.IsHomeless);
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
			    rt.srz.model.srz.address entityC = CreateNewaddress();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.address entity = GetFirstaddress();
				
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

