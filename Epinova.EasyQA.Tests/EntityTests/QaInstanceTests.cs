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
            qaType = new QaType()
                            {
                                CriteriaCategories =
                                    {
                                        new CriteriaCategory(){ Id = 1, Text = "asd", Criterias = { new QaCriteria() { Id= 1 }, new QaCriteria() { Id = 3}}},
                                        new CriteriaCategory(){ Id = 2, Text = "asd", Criterias = { }},
                                        new CriteriaCategory(){ Id = 5, Text = "asd", Criterias = { new QaCriteria() { Id= 2 }}},
                                    }
                            };
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

            int instanceCount = 0;
            foreach (QaInstanceCategory category in qaInstance.Categories)
            {
                instanceCount += category.Criterias.Count;
            }
            instanceCount.ShouldEqual(totalCatCount);
        }
    }
}