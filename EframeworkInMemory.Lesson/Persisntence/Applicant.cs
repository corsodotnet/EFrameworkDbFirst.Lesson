using System;
using System.Collections.Generic;

#nullable disable

namespace EframeworkInMemory.Lesson.Persisntence
{
    public partial class Applicant
    {
        public Applicant()
        {
            ApplicantSubmissions = new HashSet<ApplicantSubmission>();
        }

        public int ApplicantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }

        public virtual ICollection<ApplicantSubmission> ApplicantSubmissions { get; set; }
    }
}
