using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Telerik.JustMock;
using FF.Data.Models;
using FF.Contracts.Service;
using FF.Data;

namespace FF.UnitTests.Data
{
    [TestClass]
    public class FruitFinderDalTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var dal = new FruitFinderDal(new FruitFinderContext());
            Assert.IsNotNull(dal);
        }

        [TestMethod]
        public void SaveReviewTest()
        {
            var fruitReview = new Review(Mock.Create<IDateTimeService>());
            var dal = new FruitFinderDal(new FruitFinderContext());
            var review = dal.SaveReview(fruitReview);
            Assert.AreNotEqual(0, review.ReviewId);
        }

        [TestMethod]
        public void SaveToDatabaseTest()
        {
            var fruitReview = new Review(Mock.Create<IDateTimeService>());
            var mockContext = Mock.Create<FruitFinderContext>();
            Mock.Arrange(() => mockContext.SaveChanges()).Occurs(1);
            var dal = new FruitFinderDal(mockContext);
            var review = dal.SaveReview(fruitReview);
            Mock.Assert(mockContext);
        }

        [TestMethod]
        public void GetReview()
        {
            var dal = new FruitFinderDal(new FruitFinderContext());
            var review = dal.GetReview(1);
            Assert.AreEqual(1, review.ReviewId);
        }

    }
}
