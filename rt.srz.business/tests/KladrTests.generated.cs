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
    public partial class KladrTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IKladrManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IKladrManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.Kladr CreateNewKladr()
		{
			rt.srz.model.srz.Kladr entity = new rt.srz.model.srz.Kladr();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Version = default(Int16);
			entity.Name = "Test Test ";
			entity.Socr = "Test Test Tes";
			entity.Code = "Test Test ";
			entity.Index = 28;
			entity.Gninmb = "Te";
			entity.Uno = "T";
			entity.Ocatd = "T";
			entity.Status = 56;
			entity.FullAddress = "Test Test ";
			entity.Level = 93;
			
			using(rt.srz.business.manager.IKladrManager kladrMemberManager = ObjectFactory.GetInstance<IKladrManager>())
				{
				    var all = kladrMemberManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.KLADRPARENT = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.Kladr GetFirstKladr()
        {
            IList<rt.srz.model.srz.Kladr> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.Kladr entity = CreateNewKladr();
				
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
                rt.srz.model.srz.Kladr entityA = CreateNewKladr();
				manager.Save(entityA);

                rt.srz.model.srz.Kladr entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.Kladr entityC = CreateNewKladr();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Kladr entityA = GetFirstKladr();
				
				entityA.Version = default(Int16);
				
				manager.Update(entityA);

                rt.srz.model.srz.Kladr entityB = manager.GetById(entityA.Id);

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
			    rt.srz.model.srz.Kladr entityC = CreateNewKladr();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Kladr entity = GetFirstKladr();
				
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

