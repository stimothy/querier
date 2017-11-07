using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataManager
{
    public class User
    {
        [Key]
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
            bool IsFullLoad = true;

            LoginID = loginID;
            UserID = UserData.GetUserID(loginID);

            if (IsFullLoad) 
                Queries = UserData.GetQueries(UserID);
        }
    }
}
