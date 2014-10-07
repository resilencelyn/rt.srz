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
    public partial class SearchKeyTypeTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<ISearchKeyTypeManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
        }
        
        protected rt.srz.business.manager.ISearchKeyTypeManager manager;
        
        protected ISession session { get; set; }
		
		public static SearchKeyType CreateNew (int depth = 0)
		{
			rt.srz.model.srz.SearchKeyType entity = new rt.srz.model.srz.SearchKeyType();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
      entity.Code = "Test Test Test Test Test Test Test Test Te";
      entity.Name = "Test Test Test Test Test";
      entity.IsActive = true;
      entity.FirstName = true;
      entity.LastName = true;
      entity.MiddleName = true;
      entity.Birthday = true;
      entity.Birthplace = true;
      entity.Snils = true;
      entity.DocumentType = true;
      entity.DocumentSeries = true;
      entity.DocumentNumber = true;
      entity.Okato = true;
      entity.PolisType = true;
      entity.PolisSeria = true;
      entity.PolisNumber = true;
      entity.FirstNameLength = default(Int16);
      entity.LastNameLength = default(Int16);
      entity.MiddleNameLength = default(Int16);
      entity.BirthdayLength = default(Int16);
      entity.AddressStreet = true;
      entity.AddressStreetLength = default(Int16);
      entity.AddressHouse = true;
      entity.AddressRoom = true;
      entity.AddressStreet2 = true;
      entity.AddressStreetLength2 = default(Int16);
      entity.AddressHouse2 = true;
      entity.AddressRoom2 = true;
      entity.DeleteTwinChar = true;
      entity.IdenticalLetters = "Test Test ";
      entity.Recalculated = true;
      entity.Enp = true;
      entity.MainEnp = true;
      entity.Weight = 14;
      entity.Insertion = true;
			
			using(rt.srz.business.manager.IConceptManager conceptManager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = conceptManager.GetAll(1);
            Concept entityRef = null;
					  if (all.Count > 0)
					  {
              entityRef = all[0];
					  }
          
					 if (entityRef == null && depth < 3)
           {
             depth++;
             entityRef = ConceptTests.CreateNew(depth);
             ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Save(entityRef);
           }
           
					 entity.OperationKey = entityRef ;
				}	
			
			using(rt.srz.business.manager.IOrganisationManager organisationManager = ObjectFactory.GetInstance<IOrganisationManager>())
				{
           entity.Tfoms = null;
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.SearchKeyType GetFirstSearchKeyType()
        {
            IList<rt.srz.model.srz.SearchKeyType> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.SearchKeyType entity = CreateNew();
				
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
                rt.srz.model.srz.SearchKeyType entityA = CreateNew();
				manager.Save(entityA);

                rt.srz.model.srz.SearchKeyType entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.SearchKeyType entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.SearchKeyType entityA = GetFirstSearchKeyType();
				
				entityA.Code = "Test Test Test Test Test Test Test Te";
				
				manager.Update(entityA);

                rt.srz.model.srz.SearchKeyType entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Code, entityB.Code);
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
			    rt.srz.model.srz.SearchKeyType entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.SearchKeyType entity = GetFirstSearchKeyType();
				
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

