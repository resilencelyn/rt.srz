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
    public partial class SertificateUecTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<ISertificateUecManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.ISertificateUecManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.SertificateUec CreateNewSertificateUec()
		{
			rt.srz.model.srz.SertificateUec entity = new rt.srz.model.srz.SertificateUec();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Key = new System.Byte[]{};
			entity.Version = default(Int16);
			entity.IsActive = true;
			entity.InstallDate = System.DateTime.Now;
			
			using(rt.srz.business.manager.IConceptManager conceptManager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = conceptManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Type = all[0];
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
			
			using(rt.srz.business.manager.IWorkstationManager workstationManager = ObjectFactory.GetInstance<IWorkstationManager>())
				{
				    var all = workstationManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Workstation = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.SertificateUec GetFirstSertificateUec()
        {
            IList<rt.srz.model.srz.SertificateUec> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.SertificateUec entity = CreateNewSertificateUec();
				
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
                rt.srz.model.srz.SertificateUec entityA = CreateNewSertificateUec();
				manager.Save(entityA);

                rt.srz.model.srz.SertificateUec entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.SertificateUec entityC = CreateNewSertificateUec();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.SertificateUec entityA = GetFirstSertificateUec();
				
				entityA.Key = new System.Byte[]{};
				
				manager.Update(entityA);

                rt.srz.model.srz.SertificateUec entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Key, entityB.Key);
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
			    rt.srz.model.srz.SertificateUec entityC = CreateNewSertificateUec();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.SertificateUec entity = GetFirstSertificateUec();
				
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

