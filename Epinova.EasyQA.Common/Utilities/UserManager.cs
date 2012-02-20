using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;

namespace Epinova.EasyQA.Common.Utilities
{
    public class UserManager
    {
        const string UserNameListCacheName = "EQAUsers";

        private List<string> _usernames;
        public List<string> Usernames
        {
            get {
                if (_usernames == null)
                {
                    if (HttpRuntime.Cache[UserNameListCacheName] == null)
                        HttpRuntime.Cache[UserNameListCacheName] = RetrieveUsernames(Membership.GetAllUsers());
                    _usernames = HttpRuntime.Cache[UserNameListCacheName] as List<string>;
                }
                return _usernames;
            }
            set
            {
                _usernames = value;
            }
        }

        private List<string> RetrieveUsernames(MembershipUserCollection allUsers)
        {
            return (from MembershipUser user in allUsers select user.UserName).ToList();
        }
    }
}