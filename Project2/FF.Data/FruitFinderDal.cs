using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FF.Data.Models;
using FF.Contracts.Data;
using FF.Contracts.Service;

namespace FF.Data
{
    public class FruitFinderDal : IFruitFinderDal, IDisposable
    {
        private FruitFinderContext _context;
        private IDateTimeService _dateTimeService;
        private ISecurityService _securityService;

        public FruitFinderDal(
            FruitFinderContext context, 
            IDateTimeService dateTimeService,
            ISecurityService securityService)
        {
            _context = context;
            _dateTimeService = dateTimeService;
            _securityService = securityService;
        }

        public void SaveReview(Review review)
        {
            review.UpdatedWhen = _dateTimeService.UtcNow();
            review.UpdatedBy = _securityService.CurrentUserId();
            if(review.ReviewId == 0)
            {
                review.AddedBy = _securityService.CurrentUserId();
                review.AddedWhen = review.UpdatedWhen;
            }
            _context.SaveChanges();
        }

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.SingleOrDefault(r => r.ReviewId == reviewId);
        }

        public IEnumerable<Review> GetTopReviews(int numberToInclude)
        {
            return _context.Reviews
                .OrderByDescending(r => r.VoteTally)
                .Take(numberToInclude);
        }



        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
