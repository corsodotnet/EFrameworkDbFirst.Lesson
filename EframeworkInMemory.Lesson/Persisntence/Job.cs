using System;
using System.Collections.Generic;



namespace EframeworkInMemory.Lesson.Persisntence
{
    public partial class Job
    {
        public Job()
        {
            JobApplicants = new HashSet<JobApplicant>();
        }

        public int JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public DateTime? PostedDate { get; set; }
        public string Location { get; set; }

        public virtual ICollection<JobApplicant> JobApplicants { get; set; }
    }
}
