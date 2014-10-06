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
    public partial class UserActionTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IUserActionManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IUserActionManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.UserAction CreateNewUserAction()
		{
			rt.srz.model.srz.UserAction entity = new rt.srz.model.srz.UserAction();
			
			
			entity.UserId = System.Guid.NewGuid();
			
			using(rt.srz.business.manager.IConceptManager conceptManager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = conceptManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Event = all[0];
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
		protected rt.srz.model.srz.UserAction GetFirstUserAction()
        {
            IList<rt.srz.model.srz.UserAction> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.UserAction entity = CreateNewUserAction();
				
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
                rt.srz.model.srz.UserAction entityA = CreateNewUserAction();
				manager.Save(entityA);

                rt.srz.model.srz.UserAction entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.UserAction entityC = CreateNewUserAction();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.UserAction entityA = GetFirstUserAction();
				
				entityA.UserId = System.Guid.NewGuid();
				
				manager.Update(entityA);

                rt.srz.model.srz.UserAction entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.UserId, entityB.UserId);
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
			    rt.srz.model.srz.UserAction entityC = CreateNewUserAction();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.UserAction entity = GetFirstUserAction();
				
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

