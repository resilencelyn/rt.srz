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
    public partial class ErrorTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IErrorManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IErrorManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.Error CreateNewError()
		{
			rt.srz.model.srz.Error entity = new rt.srz.model.srz.Error();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Message1 = "Test Test ";
			entity.Code = "T";
			entity.Repl = "Test Test ";
			
			using(rt.srz.business.manager.IConceptManager conceptManager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = conceptManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Application = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IMessageManager message2Manager = ObjectFactory.GetInstance<IMessageManager>())
				{
				    var all = message2Manager.GetAll(1);
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
		protected rt.srz.model.srz.Error GetFirstError()
        {
            IList<rt.srz.model.srz.Error> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.Error entity = CreateNewError();
				
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
                rt.srz.model.srz.Error entityA = CreateNewError();
				manager.Save(entityA);

                rt.srz.model.srz.Error entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.Error entityC = CreateNewError();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Error entityA = GetFirstError();
				
				entityA.Message1 = "Test Test ";
				
				manager.Update(entityA);

                rt.srz.model.srz.Error entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Message1, entityB.Message1);
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
			    rt.srz.model.srz.Error entityC = CreateNewError();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Error entity = GetFirstError();
				
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

