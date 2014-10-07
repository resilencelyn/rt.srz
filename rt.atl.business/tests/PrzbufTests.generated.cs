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
    public partial class PrzbufTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IPrzbufManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
        }
        
        protected rt.atl.business.manager.IPrzbufManager manager;
        
        protected ISession session { get; set; }
		
		public static Przbuf CreateNew (int depth = 0)
		{
			rt.atl.model.atl.Przbuf entity = new rt.atl.model.atl.Przbuf();
			
			
      entity.Fam = "Test Te";
      entity.Im = "Test Te";
      entity.Ot = "Test Test Test Test Test Test Test Test";
      entity.W = 45;
      entity.Dr = System.DateTime.Now;
      entity.Dra = true;
      entity.Mr = "Test Test ";
      entity.Ds = System.DateTime.Now;
      entity.Ss = "Tes";
      entity.Doctp = "Te";
      entity.Docs = "Test Test Test Test";
      entity.Docn = "Test Test Test Te";
      entity.Docdt = System.DateTime.Now;
      entity.Docorg = "Test Test ";
      entity.Cn = "Te";
      entity.Subj = "Te";
      entity.Rn = "Te";
      entity.Indx = "Test ";
      entity.Rnname = "Test Te";
      entity.City = "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Tes";
      entity.Np = "Test Test Test Test Test Test Test Test Test Test Test Test Test";
      entity.Ul = "Test Test Test Test Test Test ";
      entity.Dom = "Tes";
      entity.Kor = "Test ";
      entity.Kv = "Test T";
      entity.Dmj = System.DateTime.Now;
      entity.Bomj = true;
      entity.Psubj = "Test";
      entity.Prn = "T";
      entity.Pindx = "T";
      entity.Prnname = "Test Test Test";
      entity.Pcity = "Test Test Test Test Test Test Tes";
      entity.Pnp = "Test Test Test Te";
      entity.Pul = "Test Test Test Test Test Test Tes";
      entity.Pdom = "Te";
      entity.Pkor = "Test Tes";
      entity.Pkv = "Test Te";
      entity.Pdmj = System.DateTime.Now;
      entity.Rdoctp = "T";
      entity.Rdocs = "Test Test Test";
      entity.Rdocn = "Test";
      entity.Rdocdt = System.DateTime.Now;
      entity.Rdocorg = "Test Test ";
      entity.Rdocend = System.DateTime.Now;
      entity.Email = "Test Test Test Test Test Test Test";
      entity.Q = "Tes";
      entity.Prz = "Test Te";
      entity.Enp = "Test Test Te";
      entity.Opdoc = 99;
      entity.Okato = "Tes";
      entity.Dhpol = System.DateTime.Now;
      entity.Dbeg = System.DateTime.Now;
      entity.Dend = System.DateTime.Now;
      entity.Petition = true;
      entity.Lpu = "Test Test Test";
      entity.Lpuwk = "Tes";
      entity.Lpust = "Test Test ";
      entity.Lpuuch = 49;
      entity.Sp = "T";
      entity.Kt = "T";
      entity.Okved = "Test ";
      entity.Kladrs = "Test Test ";
      entity.Zfam = "Test Test Test Test T";
      entity.Zim = "Test Test ";
      entity.Zot = "Test Test Test Test ";
      entity.Zt = 7;
      entity.Zdr = System.DateTime.Now;
      entity.Zmr = "Test Test ";
      entity.Zdoctp = "T";
      entity.Zdocs = "Test Test Test T";
      entity.Zdocn = "Test Te";
      entity.Zdocdt = System.DateTime.Now;
      entity.Zdocorg = "Test Test ";
      entity.Zaddr = "Test Test ";
      entity.Pid = 16;
      entity.Op = "Tes";
      entity.Oldfam = "Test Test T";
      entity.Oldim = "Test Test Test Test Test Test ";
      entity.Oldot = "Test Test Test Test Test Test Test Test";
      entity.Olddr = System.DateTime.Now;
      entity.Oldw = 27;
      entity.Oldmr = "Test Test ";
      entity.Olddoctp = "Te";
      entity.Olddocs = "Test";
      entity.Olddocn = "Test Test Test Test";
      entity.Olddocdt = System.DateTime.Now;
      entity.Sflk = "Test Test ";
      entity.Eerp = "Test Test ";
      entity.St = 5;
      entity.Repl = "Test Test ";
      entity.Num = "Test Test Test ";
      entity.Opid = 74;
      entity.Opsmo = 41;
      entity.Oppol = 44;
      entity.Polpr = 85;
      entity.Polvid = 58;
      entity.Rescode = 27;
      entity.Result = "Test Test ";
      entity.Phone = "Test Test Test Test Test Test T";
      entity.Zphone = "Test T";
      entity.Spol = "Test Test";
      entity.Npol = "Test Test Tes";
      entity.Qogrn = "Test ";
      entity.Dstop = System.DateTime.Now;
      entity.Dviz = System.DateTime.Now;
      entity.Meth = 53;
      entity.Eins = 87;
      entity.Polisid = 2;
      entity.Rstop = 71;
      entity.Et = 71;
      entity.At = 59;
      entity.Del = true;
      entity.Zp1id = 12;
      entity.A08id = 25;
      entity.Vpdid = 71;
      entity.Photo = "Test T";
      entity.Ptype = "Te";
      entity.Oldenp = "Test T";
      entity.Drt = 97;
      entity.Fiopr = "Test Test ";
      entity.Contact = "Test Test ";
      entity.Nord = "Test Test ";
      entity.Dord = System.DateTime.Now;
      entity.Erp = true;
      entity.Dedit = System.DateTime.Now;
      entity.Lock = true;
      entity.Signat = "Test Test ";
      entity.Force = true;
      entity.Olddocorg = "Test Test ";
      entity.Dz = System.DateTime.Now;
      entity.Lpuauto = 12;
      entity.Lpudt = System.DateTime.Now;
      entity.Lpudx = System.DateTime.Now;
      entity.Kladrg = "Test Test Test T";
      entity.Kladrp = "Test Test Te";
      entity.Mid = 45;
      entity.Oldss = "Test Test Te";
      entity.Dost = "Test";
      entity.Docend = System.DateTime.Now;
      entity.BirthOksm = "T";
      entity.Kateg = 27;
      entity.DstopCs = System.DateTime.Now;
      entity.OldEnp = "Test Test T";
      entity.Olddocend = System.DateTime.Now;
      entity.Oldrdoctp = "Te";
      entity.Oldrdocs = "Test ";
      entity.Oldrdocn = "Test T";
      entity.Oldrdocdt = System.DateTime.Now;
      entity.Oldrdocorg = "Test Test ";
      entity.Oldrdocend = System.DateTime.Now;
			
			using(rt.atl.business.manager.IPrzlogManager przlogManager = ObjectFactory.GetInstance<IPrzlogManager>())
				{
           entity.PRZLOG = null;
				}	
			
			return entity;
		}
		protected rt.atl.model.atl.Przbuf GetFirstPrzbuf()
        {
            IList<rt.atl.model.atl.Przbuf> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.atl.model.atl.Przbuf entity = CreateNew();
				
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
                rt.atl.model.atl.Przbuf entityA = CreateNew();
				manager.Save(entityA);

                rt.atl.model.atl.Przbuf entityB = manager.GetById(entityA.Id);

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
				rt.atl.model.atl.Przbuf entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Przbuf entityA = GetFirstPrzbuf();
				
				entityA.Fam = "Test Test Test Test Test Test Test ";
				
				manager.Update(entityA);

                rt.atl.model.atl.Przbuf entityB = manager.GetById(entityA.Id);

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
			    rt.atl.model.atl.Przbuf entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Przbuf entity = GetFirstPrzbuf();
				
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

