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
    public partial class EmploymentHistoryTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IEmploymentHistoryManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IEmploymentHistoryManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.EmploymentHistory CreateNewEmploymentHistory()
		{
			rt.srz.model.srz.EmploymentHistory entity = new rt.srz.model.srz.EmploymentHistory();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Employment = true;
			
			using(rt.srz.business.manager.IConceptManager conceptManager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = conceptManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.SourceType = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IInsuredPersonManager insuredPersonManager = ObjectFactory.GetInstance<IInsuredPersonManager>())
				{
				    var all = insuredPersonManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.InsuredPerson = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IPeriodManager periodManager = ObjectFactory.GetInstance<IPeriodManager>())
				{
				    var all = periodManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Period = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IQueryResponseManager queryResponseManager = ObjectFactory.GetInstance<IQueryResponseManager>())
				{
				    var all = queryResponseManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.QueryResponse = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.EmploymentHistory GetFirstEmploymentHistory()
        {
            IList<rt.srz.model.srz.EmploymentHistory> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.EmploymentHistory entity = CreateNewEmploymentHistory();
				
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
                rt.srz.model.srz.EmploymentHistory entityA = CreateNewEmploymentHistory();
				manager.Save(entityA);

                rt.srz.model.srz.EmploymentHistory entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.EmploymentHistory entityC = CreateNewEmploymentHistory();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.EmploymentHistory entityA = GetFirstEmploymentHistory();
				
				entityA.Employment = true;
				
				manager.Update(entityA);

                rt.srz.model.srz.EmploymentHistory entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Employment, entityB.Employment);
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
			    rt.srz.model.srz.EmploymentHistory entityC = CreateNewEmploymentHistory();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.EmploymentHistory entity = GetFirstEmploymentHistory();
				
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

