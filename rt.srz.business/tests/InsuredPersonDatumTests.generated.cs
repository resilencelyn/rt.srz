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
    public partial class InsuredPersonDatumTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IInsuredPersonDatumManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IInsuredPersonDatumManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.InsuredPersonDatum CreateNewInsuredPersonDatum()
		{
			rt.srz.model.srz.InsuredPersonDatum entity = new rt.srz.model.srz.InsuredPersonDatum();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.FirstName = "Test ";
			entity.LastName = "Test Test Test Test Test Test Test Test Test";
			entity.MiddleName = "Test Test Test Test Tes";
			entity.Birthday = System.DateTime.Now;
			entity.Birthday2 = "Test T";
			entity.BirthdayType = 67;
			entity.IsIncorrectDate = true;
			entity.IsNotGuru = true;
			entity.Snils = "Test Te";
			entity.Birthplace = "Test Test ";
			entity.IsNotCitizenship = true;
			entity.IsRefugee = true;
			entity.IsBadSnils = true;
			entity.NhFirstName = 49;
			entity.NhLastName = 62;
			entity.NhMiddleName = 50;
			
			using(rt.srz.business.manager.IConceptManager concept1Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept1Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Citizenship = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept2Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept2Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Gender = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept3Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept3Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Category = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept4Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept4Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.OldCountry = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.InsuredPersonDatum GetFirstInsuredPersonDatum()
        {
            IList<rt.srz.model.srz.InsuredPersonDatum> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.InsuredPersonDatum entity = CreateNewInsuredPersonDatum();
				
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
                rt.srz.model.srz.InsuredPersonDatum entityA = CreateNewInsuredPersonDatum();
				manager.Save(entityA);

                rt.srz.model.srz.InsuredPersonDatum entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.InsuredPersonDatum entityC = CreateNewInsuredPersonDatum();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.InsuredPersonDatum entityA = GetFirstInsuredPersonDatum();
				
				entityA.FirstName = "Test Test Test Test Test Test Test Test Test Te";
				
				manager.Update(entityA);

                rt.srz.model.srz.InsuredPersonDatum entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.FirstName, entityB.FirstName);
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
			    rt.srz.model.srz.InsuredPersonDatum entityC = CreateNewInsuredPersonDatum();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.InsuredPersonDatum entity = GetFirstInsuredPersonDatum();
				
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

