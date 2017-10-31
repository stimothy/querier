using DataManager;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Querier.Models
{
    
    public class ManageUserModel
    {
        public int UserID;
        public int Number;
        public string Name { get; set; }

        List<Question> Questions;

        public void Query()
        {
            UserID = 0;
            Number = 0;
            Questions = new List<Question>();
        }

        public void Query(System.Data.DataRow dr)
        {
            if (dr["userID"] != null) UserID = int.Parse(dr["UserID"].ToString());
            if (dr["number"] != null) Number = int.Parse(dr["number"].ToString());
            if (dr["name"] != null) Name = dr["name"].ToString();

            Questions = QueryData.GetQuestions(UserID, Number);
        }

    }
}
