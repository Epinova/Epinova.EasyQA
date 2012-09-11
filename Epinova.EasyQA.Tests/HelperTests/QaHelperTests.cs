using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Core;
using Epinova.EasyQA.Utilities;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Core;
using Epinova.EasyQA.Tests.TestHelpers;

namespace Epinova.EasyQA.Tests.HelperTests
{
    [TestFixture]
    public class QaHelperTests
    {
        [Test]
        public void GetScoreOutOfSix_50points_ScoreShouldBe3()
        {
            var qaInstance = new QaInstance(); 
            qaInstance.SetScore(50);
            QaHelper.GetScoreOutOfSix(qaInstance).ShouldBe(3);
        }

        [Test]
        public void GetScoreOutOfSix_80points_ScoreShouldBe5()
        {
            var qaInstance = new QaInstance();
            qaInstance.SetScore(80);
            QaHelper.GetScoreOutOfSix(qaInstance).ShouldBe(5);
        }

        [Test]
        public void GetScoreOutOfSix_20points_ScoreShouldBe1() 
        {
            var qaInstance = new QaInstance();
            qaInstance.SetScore(20);
            QaHelper.GetScoreOutOfSix(qaInstance).ShouldBe(1);
        }

        [Test]
        public void GetScoreOutOfSix_90points_ScoreShouldBe6() 
        {
            var qaInstance = new QaInstance();
            qaInstance.SetScore(90);
            QaHelper.GetScoreOutOfSix(qaInstance).ShouldBe(6);
        }
    }
}