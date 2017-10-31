using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Querier.Models
{
    
    public class ManageQueryModel
    {
        //private int PrivUserID { get; set; }
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int UserID
        //{
        //    get { return PrivUserID; }
        //}
        //private int PrivQueryID { get; set; }
        //[ForeignKey("ManageQuestionModel")]
        //public int QueryID
        //{
        //    get { return PrivQueryID; }
        //}
        [Key]
        public int UserID { get; set; }
        [ForeignKey("ManageQuestionModel")]
        [Editable(false)]
        public int QueryID { get; set; }
        [Display (Name="Query Name")]
        public string QueryName { get; set; }
    }
}
