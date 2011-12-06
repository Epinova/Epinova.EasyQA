using System;
using Epinova.EasyQA.Controllers;
using Epinova.EasyQA.Utilities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Epinova.EasyQA.Tests.Controllers
{
    [TestFixture]
    public class QaControllerTests
    {
        private QaController qaController;

        [SetUp]
        public void Setup()
        {
            qaController = new QaController();
            CacheManager.Usernames = new List<string> { "arve.systad", "ola.nordman", "kari.nordman" };
        }

        [Test]
        public void FindUser_ValidUsername_UserFound()
        {
            throw new NotImplementedException();
        }
        
        [Test]
        public void FindUser_InValidUsername_UserNotFound()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void FindUser_ValidUsernamePart_SeveralUsersFound()
        {
            throw new NotImplementedException();
        }
    }
}