using Messenger.BLL.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Managers
{
    public class AccountManagerAPI : IAccountManagerAPI
    {
        public IEnumerable<UserViewModel> GetAllFriends(string userId);
    }
}
