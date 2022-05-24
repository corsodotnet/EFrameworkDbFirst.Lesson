using System;
using System.Collections.Generic;

#nullable disable

namespace EFrameworkDbFirst.Lesson.Models
{
    public partial class ApplicantSubmission
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int ApplicantId { get; set; }
        public string Title { get; set; }
        public DateTime SubmissionDate { get; set; }

        public virtual Applicant Applicant { get; set; }
    }
}
