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
    public partial class MessageTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IMessageManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IMessageManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.Message CreateNewMessage()
		{
			rt.srz.model.srz.Message entity = new rt.srz.model.srz.Message();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.IsCommit = true;
			entity.IsError = true;
			
			using(rt.srz.business.manager.IBatchManager batchManager = ObjectFactory.GetInstance<IBatchManager>())
				{
				    var all = batchManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Batch = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IMessageManager messageMemberManager = ObjectFactory.GetInstance<IMessageManager>())
				{
				    var all = messageMemberManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.DependsOnMessage = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept1Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept1Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Reason = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept2Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept2Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Type = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.Message GetFirstMessage()
        {
            IList<rt.srz.model.srz.Message> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.Message entity = CreateNewMessage();
				
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
                rt.srz.model.srz.Message entityA = CreateNewMessage();
				manager.Save(entityA);

                rt.srz.model.srz.Message entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.Message entityC = CreateNewMessage();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Message entityA = GetFirstMessage();
				
				entityA.IsCommit = true;
				
				manager.Update(entityA);

                rt.srz.model.srz.Message entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.IsCommit, entityB.IsCommit);
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
			    rt.srz.model.srz.Message entityC = CreateNewMessage();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Message entity = GetFirstMessage();
				
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

