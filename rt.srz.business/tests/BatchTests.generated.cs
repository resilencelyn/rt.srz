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
    public partial class BatchTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IBatchManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IBatchManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.Batch CreateNewBatch()
		{
			rt.srz.model.srz.Batch entity = new rt.srz.model.srz.Batch();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.FileName = "T";
			entity.Number = default(Int16);
			
			using(rt.srz.business.manager.IConceptManager concept1Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept1Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.CodeConfirm = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept2Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept2Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Subject = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept3Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept3Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Type = all[0];
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
			
			using(rt.srz.business.manager.IOrganisationManager organisation1Manager = ObjectFactory.GetInstance<IOrganisationManager>())
				{
				    var all = organisation1Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Receiver = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IOrganisationManager organisation2Manager = ObjectFactory.GetInstance<IOrganisationManager>())
				{
				    var all = organisation2Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Sender = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.Batch GetFirstBatch()
        {
            IList<rt.srz.model.srz.Batch> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.Batch entity = CreateNewBatch();
				
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
                rt.srz.model.srz.Batch entityA = CreateNewBatch();
				manager.Save(entityA);

                rt.srz.model.srz.Batch entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.Batch entityC = CreateNewBatch();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Batch entityA = GetFirstBatch();
				
				entityA.FileName = "Test Test Test Test Test Test Test Test Test";
				
				manager.Update(entityA);

                rt.srz.model.srz.Batch entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.FileName, entityB.FileName);
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
			    rt.srz.model.srz.Batch entityC = CreateNewBatch();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Batch entity = GetFirstBatch();
				
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

