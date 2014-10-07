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
using rt.atl.business.manager;
using rt.atl.model.atl;

namespace rt.atl.business.tests
{
	[TestFixture]
    public partial class personTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IpersonManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
        }
        
        protected rt.atl.business.manager.IpersonManager manager;
        
        protected ISession session { get; set; }
		
		public static person CreateNew (int depth = 0)
		{
			rt.atl.model.atl.person entity = new rt.atl.model.atl.person();
			
			
      entity.Fam = "Test Test Test ";
      entity.Im = "Test Test Test Test Test Test Te";
      entity.Ot = "Test Test Test Test Test Test Test";
      entity.W = 60;
      entity.Dr = System.DateTime.Now;
      entity.Dra = true;
      entity.Mr = "Test Test ";
      entity.Ds = System.DateTime.Now;
      entity.Ss = "Test ";
      entity.Doctp = "T";
      entity.Docs = "Test T";
      entity.Docn = "Test Test Te";
      entity.Docdt = System.DateTime.Now;
      entity.Docorg = "Test Test ";
      entity.Cn = "T";
      entity.Subj = "Tes";
      entity.Rn = "Test Te";
      entity.Indx = "Te";
      entity.Rnname = "Test Test Test Test Test Test Test Test Test Test ";
      entity.City = "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test";
      entity.Np = "Test Test ";
      entity.Ul = "Test Test Test Test Test Test Test Test Test Test Test Test";
      entity.Dom = "Test T";
      entity.Kor = "Test ";
      entity.Kv = "Test";
      entity.Dmj = System.DateTime.Now;
      entity.Bomj = true;
      entity.Psubj = "Test";
      entity.Prn = "Test Te";
      entity.Pindx = "Tes";
      entity.Prnname = "Test Test Test Test Test Test Test Test Test Tes";
      entity.Pcity = "Test Test Test Test Test Test Test Test Tes";
      entity.Pnp = "Test Test Test Test Test Test Test Test Test Tes";
      entity.Pul = "Test Test Test Test Test Test Test Test Test Test Test Test";
      entity.Pdom = "Test Test T";
      entity.Pkor = "Test Te";
      entity.Pkv = "Test Test T";
      entity.Pdmj = System.DateTime.Now;
      entity.Rdoctp = "Te";
      entity.Rdocs = "Test Test Test Test";
      entity.Rdocn = "Test T";
      entity.Rdocdt = System.DateTime.Now;
      entity.Rdocorg = "Test Test ";
      entity.Rdocend = System.DateTime.Now;
      entity.Email = "Test Test Test Test Test Test Test Test Test ";
      entity.Q = "Test";
      entity.Prz = "Test ";
      entity.Enp = "Test Test Test";
      entity.Opdoc = 44;
      entity.Okato = "Te";
      entity.Dhpol = System.DateTime.Now;
      entity.Dbeg = System.DateTime.Now;
      entity.Dend = System.DateTime.Now;
      entity.Petition = true;
      entity.Lpu = "Test Te";
      entity.Lpuwk = "Test Tes";
      entity.Lpust = "Test Tes";
      entity.Lpuuch = 58;
      entity.Sp = "T";
      entity.Kt = "T";
      entity.Okved = "Te";
      entity.Kladrs = "Test Test Te";
      entity.Zfam = "Test Test";
      entity.Zim = "Test Test Test Test Test Test Te";
      entity.Zot = "Test Test Tes";
      entity.Zt = 9;
      entity.Zdr = System.DateTime.Now;
      entity.Zmr = "Test Test ";
      entity.Zdoctp = "Te";
      entity.Zdocs = "Test Test Test T";
      entity.Zdocn = "Test Test";
      entity.Zdocdt = System.DateTime.Now;
      entity.Zdocorg = "Test Test ";
      entity.Zaddr = "Test Test ";
      entity.Pid = 16;
      entity.Dedit = System.DateTime.Now;
      entity.Derp = System.DateTime.Now;
      entity.Unemp = true;
      entity.Extid = "T";
      entity.Phone = "Test Test Test Test Test Test Te";
      entity.Zphone = "Test Test Test Test Test Test";
      entity.Spol = "Test Test ";
      entity.Npol = "Test Tes";
      entity.Qogrn = "T";
      entity.Dstop = System.DateTime.Now;
      entity.Dviz = System.DateTime.Now;
      entity.Meth = 24;
      entity.Zenp = "Tes";
      entity.Denp = System.DateTime.Now;
      entity.Erp = true;
      entity.Zerr = "Test Test Test Test Test Test Test Tes";
      entity.Opsmo = 73;
      entity.Oppol = 99;
      entity.Polpr = 50;
      entity.Polvid = 86;
      entity.Polisid = 8;
      entity.Pfrss = "Test Test Te";
      entity.Rstop = 98;
      entity.Dakt = System.DateTime.Now;
      entity.Nakt = "Test Test Test ";
      entity.Takt = "Test ";
      entity.Dzp1 = System.DateTime.Now;
      entity.Zp1repl = "Test Test ";
      entity.Del = true;
      entity.Oldenp = "Test Test Test ";
      entity.Tenp = 15;
      entity.Senp = "Test T";
      entity.Photo = "Test Te";
      entity.Ptype = "Tes";
      entity.Drt = 69;
      entity.Fiopr = "Test Test ";
      entity.Contact = "Test Test ";
      entity.Nord = "Test Test ";
      entity.Dord = System.DateTime.Now;
      entity.Signat = "Test Test Te";
      entity.Dz = System.DateTime.Now;
      entity.Lpuauto = 62;
      entity.Lpudt = System.DateTime.Now;
      entity.Lpudx = System.DateTime.Now;
      entity.Kladrg = "Test Te";
      entity.Kladrp = "Test";
      entity.Bid = 48;
      entity.Mid = 47;
      entity.Other = true;
      entity.Fame = "Test Test Tes";
      entity.Ime = "Test Test Test Test Test Test Test";
      entity.Ote = "Test Test Test Test Test ";
      entity.Ssold = "Test Test ";
      entity.Force = 79;
      entity.PzScan = true;
      entity.Dost = "Tes";
      entity.Docend = System.DateTime.Now;
      entity.BirthOksm = "T";
      entity.Kateg = 39;
      entity.DstopCs = System.DateTime.Now;
      entity.IsExported = true;
      entity.ExportError = "Test Test ";
			entity.Zid = null;
			
			return entity;
		}
		protected rt.atl.model.atl.person GetFirstperson()
        {
            IList<rt.atl.model.atl.person> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.atl.model.atl.person entity = CreateNew();
				
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
                rt.atl.model.atl.person entityA = CreateNew();
				manager.Save(entityA);

                rt.atl.model.atl.person entityB = manager.GetById(entityA.Id);

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
				rt.atl.model.atl.person entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.person entityA = GetFirstperson();
				
				entityA.Fam = "Test Test";
				
				manager.Update(entityA);

                rt.atl.model.atl.person entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Fam, entityB.Fam);
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
			    rt.atl.model.atl.person entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.person entity = GetFirstperson();
				
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

