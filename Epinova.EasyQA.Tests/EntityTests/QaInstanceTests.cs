using System;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Tests.TestHelpers;
using NUnit.Framework;
using NUnit.Core;

namespace Epinova.EasyQA.Tests.EntityTests
{
    [TestFixture]
    public class QaInstanceTests
    {
        QaType qaType;

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void QaInstanceConstructor_ValidQaType_QaInstanceHasCorrectNumberOfCriterias()
        {
            QaInstance qaInstance = new QaInstance(qaType);
            int totalCatCount = 0;
            foreach(CriteriaCategory category in qaType.CriteriaCategories)
            {
                totalCatCount += category.Criterias.Count;    
            }

            qaInstance.Criterias.Count.ShouldEqual(totalCatCount);
        }
    }
}