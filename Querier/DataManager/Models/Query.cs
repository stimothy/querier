using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace DataManager
{
    public class Query
    {
        [Key]
        public int UserID { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public List<Question> Questions;

        public Query()
        {
            UserID = 0;
            Number = 0;
            Questions = new List<Question>();
            Code = null;
        }

        public Query(DataRow dr)
        {
            bool IsFullLoad = (dr.Table.Columns.Contains("userID"));

            if (dr["number"] != null) Number = int.Parse(dr["number"].ToString());
            if (dr["name"] != null) Name = dr["name"].ToString();
            if (dr["activeCode"] != null) Code = dr["activeCode"].ToString();

            if (IsFullLoad)
            {
                if (dr["userID"] != null) UserID = int.Parse(dr["UserID"].ToString());
                
                Questions = QueryData.GetQuestions(UserID, Number);
                Questions = Questions.OrderBy(x => x.Order).ToList();
            }
        }

    }
}
