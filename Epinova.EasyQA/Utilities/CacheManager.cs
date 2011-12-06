using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using Epinova.EasyQA.Core;

namespace Epinova.EasyQA.Utilities
{
    public static class CacheManager
    {

        private static List<string> _usernames;
        public static List<string> Usernames
        {
            get {
                if (_usernames == null)
                {
                    if (HttpRuntime.Cache[Constants.UserNameListCacheName] == null)
                        HttpRuntime.Cache[Constants.UserNameListCacheName] = RetrieveUsernames(Membership.GetAllUsers());
                    _usernames = HttpRuntime.Cache[Constants.UserNameListCacheName] as List<string>;
                }
                return _usernames;
            }
            set
            {
                _usernames = value;
            }
        }

        private static List<string> RetrieveUsernames(MembershipUserCollection allUsers)
        {
            return (from MembershipUser user in allUsers select user.UserName).ToList();
        }
    }
}