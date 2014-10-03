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
    public partial class RangeNumberTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IRangeNumberManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IRangeNumberManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.RangeNumber CreateNewRangeNumber()
		{
			rt.srz.model.srz.RangeNumber entity = new rt.srz.model.srz.RangeNumber();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.RangelFrom = 13;
			entity.RangelTo = 3;
			entity.ChangeDate = System.DateTime.Now;
			
			using(rt.srz.business.manager.IRangeNumberManager rangeNumberMemberManager = ObjectFactory.GetInstance<IRangeNumberManager>())
				{
				    var all = rangeNumberMemberManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Parent = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IOrganisationManager organisationManager = ObjectFactory.GetInstance<IOrganisationManager>())
				{
				    var all = organisationManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Smo = all[0];
					}
				}	
			
			using(rt.srz.business.manager.ITemplateManager templateManager = ObjectFactory.GetInstance<ITemplateManager>())
				{
				    var all = templateManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Template = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.RangeNumber GetFirstRangeNumber()
        {
            IList<rt.srz.model.srz.RangeNumber> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.RangeNumber entity = CreateNewRangeNumber();
				
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
                rt.srz.model.srz.RangeNumber entityA = CreateNewRangeNumber();
				manager.Save(entityA);

                rt.srz.model.srz.RangeNumber entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.RangeNumber entityC = CreateNewRangeNumber();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.RangeNumber entityA = GetFirstRangeNumber();
				
				entityA.RangelFrom = 65;
				
				manager.Update(entityA);

                rt.srz.model.srz.RangeNumber entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.RangelFrom, entityB.RangelFrom);
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
			    rt.srz.model.srz.RangeNumber entityC = CreateNewRangeNumber();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.RangeNumber entity = GetFirstRangeNumber();
				
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

