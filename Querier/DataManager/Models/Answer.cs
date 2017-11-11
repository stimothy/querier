using System;
using System.Data;

namespace DataManager
{
    public class Answer
    {
        public int UserID { get; set; }
        public int QueryNumber { get; set; }
        public int QuestionNumber { get; set; }
        public int Number { get; set; }
        public int Score { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public Answer()
        {
            UserID = 0;
            QueryNumber = 0;
            QuestionNumber = 0;
            Number = 0;
            Order = 0;
        }

        public Answer(DataRow dr)
        {
            bool IsFullLoad = (dr.Table.Columns.Contains("userID"));

            if (dr["number"] != null) Number = int.Parse(dr["number"].ToString());
            if (dr["name"] != null) Name = dr["name"].ToString();
            if (dr["score"] != null) Score = int.Parse(dr["score"].ToString());
            if (dr["Ordinality"] != null) Order = int.Parse(dr["Ordinality"].ToString());

            if (IsFullLoad)
            {
                if (dr["userID"] != null) UserID = int.Parse(dr["UserID"].ToString());
                if (dr["queryNumber"] != null) QueryNumber = int.Parse(dr["queryNumber"].ToString());
                if (dr["questionNumber"] != null) QuestionNumber = int.Parse(dr["questionNumber"].ToString());
            }
        }

    }
}
