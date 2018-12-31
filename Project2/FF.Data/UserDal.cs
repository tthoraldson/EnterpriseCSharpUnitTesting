using FF.Contracts.Service;
using FF.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Data
{
    public class UserDal : IDisposable
    {
        private FruitFinderContext _context;
        private ISecurityService _securityService;

        public UserDal(FruitFinderContext context, ISecurityService securityService)
        {
            _context = context;
            _securityService = securityService;
        }

        public User GetUserByUsername(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.Username == userName);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
