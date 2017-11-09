using System;
using System.Collections.Generic;
using System.Text;

namespace DataManager
{
    public static class UserOptions
    {
        public static User GetUser(string loginID)
        {
            return new User(loginID);
        }

        public static void AddQuery(User user)
        {
            QueryData.Add(user.UserID);
            user.Queries = UserData.GetQueries(user.UserID);
        }

        public static void DeleteQuery(User user, int number)
        {
            Query query = QueryData.Get(user.UserID, number);
            QueryData.Delete(query);
            user.Queries = UserData.GetQueries(user.UserID);
        }
    }
}
