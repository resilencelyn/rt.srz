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
    public partial class SearchKeyTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<ISearchKeyManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
        }
        
        protected rt.srz.business.manager.ISearchKeyManager manager;
        
        protected ISession session { get; set; }
		
		public static SearchKey CreateNew (int depth = 0)
		{
			rt.srz.model.srz.SearchKey entity = new rt.srz.model.srz.SearchKey();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
      entity.KeyValue = new System.Byte[]{};
			
			using(rt.srz.business.manager.IConceptManager conceptManager = ObjectFactory.GetInstance<IConceptManager>())
				{
           entity.DocumentUdlType = null;
				}	
			
			using(rt.srz.business.manager.IInsuredPersonManager insuredPersonManager = ObjectFactory.GetInstance<IInsuredPersonManager>())
				{
           entity.InsuredPerson = null;
				}	
			
			using(rt.srz.business.manager.IQueryResponseManager queryResponseManager = ObjectFactory.GetInstance<IQueryResponseManager>())
				{
           entity.QueryResponse = null;
				}	
			
			using(rt.srz.business.manager.ISearchKeyTypeManager searchKeyTypeManager = ObjectFactory.GetInstance<ISearchKeyTypeManager>())
				{
           entity.KeyType = null;
				}	
			
			using(rt.srz.business.manager.IStatementManager statementManager = ObjectFactory.GetInstance<IStatementManager>())
				{
           entity.Statement = null;
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.SearchKey GetFirstSearchKey()
        {
            IList<rt.srz.model.srz.SearchKey> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.SearchKey entity = CreateNew();
				
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
                rt.srz.model.srz.SearchKey entityA = CreateNew();
				manager.Save(entityA);

                rt.srz.model.srz.SearchKey entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.SearchKey entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.SearchKey entityA = GetFirstSearchKey();
				
				entityA.KeyValue = new System.Byte[]{};
				
				manager.Update(entityA);

                rt.srz.model.srz.SearchKey entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.KeyValue, entityB.KeyValue);
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
			    rt.srz.model.srz.SearchKey entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.SearchKey entity = GetFirstSearchKey();
				
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

