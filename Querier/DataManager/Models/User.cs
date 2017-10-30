using System;
using System.Collections.Generic;
using System.Text;

namespace DataManager
{
    public class User
    {
        public string LoginID { get; set; }
        public int UserID { get; set; }

        public List<Query> Queries;

        public User()
        {
            LoginID = String.Empty;
            UserID = 0;
            Queries = new List<Query>();
        }

        public User(string loginID)
        {
            LoginID = loginID;
            UserID = UserData.GetUserID(loginID);

            Queries = UserData.GetQueries(UserID);
        }
    }
}
