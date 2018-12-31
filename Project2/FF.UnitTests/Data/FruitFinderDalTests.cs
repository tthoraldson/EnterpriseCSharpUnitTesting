using System;
using System.Collections.Generic;
using System.Linq;
using FF.Contracts.Service;
using FF.Data;
using FF.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using Telerik.JustMock.EntityFramework;
using FluentAssertions;
using Telerik.JustMock.Helpers;

namespace FF.UnitTests.Data
{
    public class FruidFinderDalBuilder
    {
        public const string SecurityServiceUserName = "test";
        public const int SecurityServiceUserId = 1;

        private FruitFinderContext _context = EntityFrameworkMock
                .Create<FruitFinderContext>()
                .PrepareMock();

        private IDateTimeService _dateTimeService = Mock.CreateLike<IDateTimeService>(
            dts => dts.UtcNow() == new DateTime(2000,1,1));

        private ISecurityService _securityService = Mock.CreateLike<ISecurityService>(
            ss => ss.CurrentUser() == SecurityServiceUserName &&
                  ss.CurrentUserId() == SecurityServiceUserId);

        public FruidFinderDalBuilder WithContext(FruitFinderContext context)
        {
            _context = context;
            return this;
        }

        public FruidFinderDalBuilder WithDateTime(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
            return this;
        }

        public FruidFinderDalBuilder WithSecurityService(ISecurityService securityService)
        {
            _securityService = securityService;
            return this;
        }

        public FruitFinderDal Build()
        {
            return new FruitFinderDal(_context, _dateTimeService, _securityService);
        }

        public IDateTimeService GetDateTimeService()
        {
            return _dateTimeService;
        }

        public Review MakeReview()
        {
            return new Review(_dateTimeService);
        }
    }

    [TestClass]
    public class FruitFinderDalTests
    {
        [TestMethod] //A value test
        public void GettingTopFiveReviews_WhenFiveExist_ReturnsFiveInDescendingOrder()
        {
            //Arrange
            var mockContext = Mock.Create<FruitFinderContext>().PrepareMock();
            var reviews = new List<Review>();
            var builder = new FruidFinderDalBuilder().WithContext(mockContext);
            var numberToInclude = 5;
            for (int i = 0; i < numberToInclude * 2; i++)
            {
                var review = builder.MakeReview();
                review.VoteTally = i;
                reviews.Add(review);
            }
            mockContext.Reviews.Bind(reviews);
            var dal = builder.Build();

            //Act
            var result = dal.GetTopReviews(numberToInclude);

            //Assert
            result.Count().Should().Be(numberToInclude, 
                "the list should have " + numberToInclude + " reviews");
            result.Should().BeInDescendingOrder(x => x.VoteTally, 
                "the reviews should be in descending order by VoteTally");
        }

        [TestMethod] //A state test
        public void SavingAReview_WhenUpdatingIt_UpdatesTheUpdatedWhenAndBy()
        {
            //Arrange
            var builder = new FruidFinderDalBuilder();
            var dal = builder.Build();
            var dateTimeService = builder.GetDateTimeService();
            var review = builder.MakeReview();
            review.ReviewId = 1;
            review.UpdatedBy = FruidFinderDalBuilder.SecurityServiceUserId + 1;
            review.UpdatedWhen = dateTimeService.UtcNow().AddDays(-1);

            //Act
            dal.SaveReview(review);

            //Assert
            review.UpdatedWhen.ShouldBeEquivalentTo(dateTimeService.UtcNow(),
                "the updated date should be set to UtcNow when the review is saved");
            review.UpdatedBy.ShouldBeEquivalentTo(FruidFinderDalBuilder.SecurityServiceUserId,
                "the updated by should be set to the current user when a review is saved");

        }

        [TestMethod] //An interaction test
        public void SavingAReview_WhenSuccessful_CallsContextSaveChanges()
        {
            //Arrange
            var mockContext = Mock.Create<FruitFinderContext>();
            var builder = new FruidFinderDalBuilder().WithContext(mockContext);
            var dal = builder.Build();
            Mock.Arrange(() => mockContext.SaveChanges()).OccursOnce();
            var review = builder.MakeReview();

            //Act
            dal.SaveReview(review);

            //Assert
            Mock.Assert(mockContext);
        }

    }
}
