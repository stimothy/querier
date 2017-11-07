using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DataManager
{
    public class Query
    {
        [Key]
        public int UserID;
        public int Number;
        public string Name { get; set; }

        public List<Question> Questions;

        public Query()
        {
            UserID = 0;
            Number = 0;
            Questions = new List<Question>();
        }

        public Query(DataRow dr)
        {
            bool IsFullLoad = (dr.Table.Columns.Contains("userID"));

            if (dr["number"] != null) Number = int.Parse(dr["number"].ToString());
            if (dr["name"] != null) Name = dr["name"].ToString();

            if (IsFullLoad)
            {
                if (dr["userID"] != null) UserID = int.Parse(dr["UserID"].ToString());
                Questions = QueryData.GetQuestions(UserID, Number); 
            }
        }

    }
}
