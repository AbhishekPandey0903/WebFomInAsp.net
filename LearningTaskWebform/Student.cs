using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningTaskWebform
{
    public class StudentGet
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
       public string LastName { get; set; }
       public string DOB { get; set; }
       public string Gender { get; set; }
       public string Category { get; set; }
       public string Contact { get; set; }

        public string UploadStudentImage { get; set; }

    }
}