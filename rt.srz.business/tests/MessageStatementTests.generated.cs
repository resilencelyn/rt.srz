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
    public partial class MessageStatementTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IMessageStatementManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IMessageStatementManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.MessageStatement CreateNewMessageStatement()
		{
			rt.srz.model.srz.MessageStatement entity = new rt.srz.model.srz.MessageStatement();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Version = 92;
			
			using(rt.srz.business.manager.IConceptManager conceptManager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = conceptManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Type = all[0];
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
			
			using(rt.srz.business.manager.IStatementManager statementManager = ObjectFactory.GetInstance<IStatementManager>())
				{
				    var all = statementManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Statement = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.MessageStatement GetFirstMessageStatement()
        {
            IList<rt.srz.model.srz.MessageStatement> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.MessageStatement entity = CreateNewMessageStatement();
				
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
                rt.srz.model.srz.MessageStatement entityA = CreateNewMessageStatement();
				manager.Save(entityA);

                rt.srz.model.srz.MessageStatement entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.MessageStatement entityC = CreateNewMessageStatement();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.MessageStatement entityA = GetFirstMessageStatement();
				
				entityA.Version = 68;
				
				manager.Update(entityA);

                rt.srz.model.srz.MessageStatement entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Version, entityB.Version);
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
			    rt.srz.model.srz.MessageStatement entityC = CreateNewMessageStatement();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.MessageStatement entity = GetFirstMessageStatement();
				
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

