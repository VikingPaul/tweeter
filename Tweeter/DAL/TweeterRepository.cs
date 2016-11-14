using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tweeter.DAL
{
    public class TweeterRepository
    {
        public TweeterRepository()
        {
            Context = new TweeterContext();
        }

        public TweeterRepository(TweeterContext _context)
        {
            Context = _context;
        }

        public TweeterContext Context { get; set; }

        public List<string> GetUsernames()
        {
            return Context.TweeterUsers.Select(x => x.BaseUser.UserName).ToList();
        }

        public bool UsernameIsUnique(string v)
        {
            return !Context.TweeterUsers.Select(x => x.BaseUser.UserName).ToList().Contains(v);
        }
    }
}