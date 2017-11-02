using System;
using System.Data;

namespace DataManager
{
    public class Answer
    {
        public int UserID;
        public int QueryNumber;
        public int QuestionNumber;
        public int Number;
        public int Score { get; set; }
        public string Name { get; set; }

        public Answer()
        {
            UserID = 0;
            QueryNumber = 0;
            QuestionNumber = 0;
            Number = 0;
        }

        public Answer(DataRow dr)
        {
            if (dr["userID"] != null) UserID = int.Parse(dr["UserID"].ToString());
            if (dr["queryNumber"] != null) QueryNumber = int.Parse(dr["queryNumber"].ToString());
            if (dr["questionNumber"] != null) QuestionNumber = int.Parse(dr["questionNumber"].ToString());
            if (dr["number"] != null) Number = int.Parse(dr["number"].ToString());
            if (dr["score"] != null) Score = int.Parse(dr["score"].ToString());
            if (dr["name"] != null) Name = dr["name"].ToString();
        }

    }
}
