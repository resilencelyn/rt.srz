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
    public partial class QueryResponseTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IQueryResponseManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IQueryResponseManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.QueryResponse CreateNewQueryResponse()
		{
			rt.srz.model.srz.QueryResponse entity = new rt.srz.model.srz.QueryResponse();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Number = default(Int16);
			entity.PolisNumber = "Test Test Test";
			entity.MainPolisNumber = "Test Test Test ";
			entity.Snils = "Test T";
			entity.IsActive = true;
			entity.Employment = true;
			
			using(rt.srz.business.manager.IaddressManager addressManager = ObjectFactory.GetInstance<IaddressManager>())
				{
				    var all = addressManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Address = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept1Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept1Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Feature = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IDeadInfoManager deadInfoManager = ObjectFactory.GetInstance<IDeadInfoManager>())
				{
				    var all = deadInfoManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.DeadInfo = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IDocumentManager documentManager = ObjectFactory.GetInstance<IDocumentManager>())
				{
				    var all = documentManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.DocumentUdl = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IInsuredPersonDatumManager insuredPersonDatumManager = ObjectFactory.GetInstance<IInsuredPersonDatumManager>())
				{
				    var all = insuredPersonDatumManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.InsuredPersonData = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IMessageManager messageManager = ObjectFactory.GetInstance<IMessageManager>())
				{
				    var all = messageManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Message = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept2Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept2Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.TrustLevel = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.QueryResponse GetFirstQueryResponse()
        {
            IList<rt.srz.model.srz.QueryResponse> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.QueryResponse entity = CreateNewQueryResponse();
				
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
                rt.srz.model.srz.QueryResponse entityA = CreateNewQueryResponse();
				manager.Save(entityA);

                rt.srz.model.srz.QueryResponse entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.QueryResponse entityC = CreateNewQueryResponse();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.QueryResponse entityA = GetFirstQueryResponse();
				
				entityA.Number = default(Int16);
				
				manager.Update(entityA);

                rt.srz.model.srz.QueryResponse entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Number, entityB.Number);
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
			    rt.srz.model.srz.QueryResponse entityC = CreateNewQueryResponse();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.QueryResponse entity = GetFirstQueryResponse();
				
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

