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
    public partial class InsuredPersonTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IInsuredPersonManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IInsuredPersonManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.InsuredPerson CreateNewInsuredPerson()
		{
			rt.srz.model.srz.InsuredPerson entity = new rt.srz.model.srz.InsuredPerson();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.MainPolisNumber = "Test Test";
			
			using(rt.srz.business.manager.IDeadInfoManager deadInfoManager = ObjectFactory.GetInstance<IDeadInfoManager>())
				{
				    var all = deadInfoManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.DeadInfo = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager conceptManager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = conceptManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Status = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.InsuredPerson GetFirstInsuredPerson()
        {
            IList<rt.srz.model.srz.InsuredPerson> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.InsuredPerson entity = CreateNewInsuredPerson();
				
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
                rt.srz.model.srz.InsuredPerson entityA = CreateNewInsuredPerson();
				manager.Save(entityA);

                rt.srz.model.srz.InsuredPerson entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.InsuredPerson entityC = CreateNewInsuredPerson();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.InsuredPerson entityA = GetFirstInsuredPerson();
				
				entityA.MainPolisNumber = "Test Te";
				
				manager.Update(entityA);

                rt.srz.model.srz.InsuredPerson entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.MainPolisNumber, entityB.MainPolisNumber);
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
			    rt.srz.model.srz.InsuredPerson entityC = CreateNewInsuredPerson();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.InsuredPerson entity = GetFirstInsuredPerson();
				
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

