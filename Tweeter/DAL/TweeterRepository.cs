using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweeter.Models;

namespace Tweeter.DAL
{
    public class TweeterRepository
    {
        public TweeterContext Context { get; set; }
        public TweeterRepository(TweeterContext _context)
        {
            Context = _context;
        }

        public TweeterRepository() {}

        public List<string> GetUsernames()
        {
            return Context.TweeterUsers.Select(u => u.BaseUser.UserName).ToList();
        }

        public Twit UsernameExistsOfTwit(string v)
        {
            return Context.TweeterUsers.FirstOrDefault(u => u.BaseUser.UserName.ToLower() == v.ToLower());
        }

        public bool UsernameExists(string v)
        {
            // Works if mocked the UserManager
            /*
            if (Context.Users.Any(u => u.UserName.Contains(v)))
            {
                return true;
            }
            return false;
            */
            
            Twit found_twit = Context.TweeterUsers.FirstOrDefault(u => u.BaseUser.UserName.ToLower() == v.ToLower());
            if (found_twit != null)
            {
                return true;
            }

            return false;
            
        }

        public void FollowUser(string user, string follow)
        {
            if(user != follow)
            {
                int a = 1;
                Twit Follower = Context.TweeterUsers.FirstOrDefault(x => x.BaseUser.UserName == user);
                Twit Followee = Context.TweeterUsers.FirstOrDefault(x => x.BaseUser.UserName == follow);
                if (Follower.Follows == null)
                {
                    Follower.Follows = new List<Twit>
                    {
                        Followee
                    };
                } else
                {
                    if (!Follower.Follows.Contains(Followee))
                    {
                        Follower.Follows.Add(Followee);
                    }
                }
                Context.SaveChanges();
            }
        }

        public void UnfollowUser(string user, string follow)
        {
            Twit Follower = Context.TweeterUsers.FirstOrDefault(x => x.BaseUser.UserName == user);
            Twit Followee = Context.TweeterUsers.FirstOrDefault(x => x.BaseUser.UserName == follow);
            Follower.Follows.Remove(Followee);
            Context.SaveChanges();
        }
    }
}