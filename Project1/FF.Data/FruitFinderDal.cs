using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FF.Data.Models;
using FF.Contracts.Data;

namespace FF.Data
{
    public class FruitFinderDal : IFruitFinderDal, IDisposable
    {
        private FruitFinderContext _context;
        private int _currentUser = 5;

        public FruitFinderDal(FruitFinderContext context)
        {
            _context = context;
        }

        public Review SaveReview(Review review)
        {
            review.UpdatedWhen = DateTime.Now;
            review.UpdatedBy = _currentUser;
            if(review.ReviewId == 0)
            {
                review.AddedBy = _currentUser;
                review.AddedWhen = review.UpdatedWhen;
            }
            review.ReviewId = 1;

            return review;
        }

        public Review GetReview(int reviewId)
        {
            return new Review(null)
            {
                ReviewId = reviewId
            };
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
