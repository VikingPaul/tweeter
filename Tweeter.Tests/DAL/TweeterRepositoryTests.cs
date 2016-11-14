using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tweeter.DAL;
using Tweeter.Models;
using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Tweeter.Tests.DAL
{
    [TestClass]
    public class TweeterRepositoryTests
    {
        Mock<TweeterContext> moq_context { get; set; }
        Mock<DbSet<Twit>> moq_twit { get; set; }
        List<Twit> Twit_list { get; set; }
        TweeterRepository repo { get; set; }
        

        public void ConnectMockToData()
        {
            var queryList = Twit_list.AsQueryable();
            moq_twit.As<IQueryable<Twit>>().Setup(x => x.Provider).Returns(queryList.Provider);
            moq_twit.As<IQueryable<Twit>>().Setup(x => x.Expression).Returns(queryList.Expression);
            moq_twit.As<IQueryable<Twit>>().Setup(x => x.ElementType).Returns(queryList.ElementType);
            moq_twit.As<IQueryable<Twit>>().Setup(x => x.GetEnumerator()).Returns(() => queryList.GetEnumerator());
            moq_context.Setup(x => x.TweeterUsers).Returns(moq_twit.Object);
            //moq_twit.Setup(x => x.Add(It.IsAny<Twit>())).Callback((Twit y) => Twit_list.Add(y));
            //moq_twit.Setup(x => x.Remove(It.IsAny<Twit>())).Callback((Twit y) => Twit_list.Remove(y));
        }

        [TestInitialize]
        public void init()
        {
            moq_context = new Mock<TweeterContext>();
            moq_twit = new Mock<DbSet<Twit>>();
            Twit_list = new List<Twit>();
            ConnectMockToData();
            repo = new TweeterRepository(moq_context.Object);
        }
        [TestMethod]
        public void RepoIsNotNull()
        {
            TweeterRepository repo = new TweeterRepository();
            Assert.IsNotNull(repo);
        }
        [TestMethod]
        public void RepoCanGetUserNames()
        {
            List<string> usernames = repo.GetUsernames();
            Assert.AreEqual(0, usernames.Count);
        }
        [TestMethod]
        public void RepoUsernamesAreUnique()
        {
            bool unique = repo.UsernameIsUnique("steve");
            Assert.AreEqual(true, unique);
        }
    }
}
