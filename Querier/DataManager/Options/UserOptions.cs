using System;
using System.Collections.Generic;
using System.Text;

namespace DataManager.Options
{
    public static class UserOptions
    {
        public static User GetUser(string loginID)
        {
            return new User(loginID);
        }

        public static User AddQuery(User user)
        {
            QueryData.Add(user.UserID);
            user.Queries = UserData.GetQueries(user.UserID);
            return user;
        }
    }
}
