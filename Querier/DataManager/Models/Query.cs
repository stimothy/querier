using System;
using System.Collections.Generic;
using System.Data;

namespace DataManager
{
    public class Query
    {
        public int UserID;
        public int Number;
        public string Name { get; set; }

        List<Question> Questions;

        public Query()
        {
            UserID = 0;
            Number = 0;
            Questions = new List<Question>();
        }

        public Query(DataRow dr)
        {
            if (dr["userID"] != null) UserID = int.Parse(dr["UserID"].ToString());
            if (dr["number"] != null) Number = int.Parse(dr["number"].ToString());
            if (dr["name"] != null) Name = dr["name"].ToString();

            Questions = QueryData.GetQuestions(UserID, Number);
        }

    }
}
