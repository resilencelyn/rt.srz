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
    public partial class ConceptTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IConceptManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IConceptManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.Concept CreateNewConcept()
		{
			rt.srz.model.srz.Concept entity = new rt.srz.model.srz.Concept();
			
			
			entity.Code = "Test Test ";
			entity.Name = "Test Test ";
			entity.Description = "Test Test ";
			entity.ShortName = "Test Test ";
			entity.RelatedCode = "Test Test ";
			entity.RelatedOid = "Test Test ";
			entity.RelatedType = "Test Test ";
			entity.DateFrom = System.DateTime.Now;
			entity.DateTo = System.DateTime.Now;
			entity.Relevance = 72;
			
			using(rt.srz.business.manager.IOidManager oidManager = ObjectFactory.GetInstance<IOidManager>())
				{
				    var all = oidManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Oid = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.Concept GetFirstConcept()
        {
            IList<rt.srz.model.srz.Concept> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.Concept entity = CreateNewConcept();
				
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
                rt.srz.model.srz.Concept entityA = CreateNewConcept();
				manager.Save(entityA);

                rt.srz.model.srz.Concept entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.Concept entityC = CreateNewConcept();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Concept entityA = GetFirstConcept();
				
				entityA.Code = "Test Test ";
				
				manager.Update(entityA);

                rt.srz.model.srz.Concept entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Code, entityB.Code);
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
			    rt.srz.model.srz.Concept entityC = CreateNewConcept();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Concept entity = GetFirstConcept();
				
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

