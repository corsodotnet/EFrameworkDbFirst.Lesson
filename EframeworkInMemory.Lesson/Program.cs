using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EframeworkInMemory.Lesson.Persisntence;


namespace EframeworkInMemory.Persisntence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var contextDb = new ApplicantsContext())
            {

                    var applicant =  GetUser(contextDb,"Zenaida");
                    AddApplication(contextDb, applicant);               
                    contextDb.SaveChanges();

                    var applications = GetApplicantions(contextDb, applicant.ApplicantId);

                    Console.WriteLine($" Name : {applicant.Name}");
                    foreach (var appSub in applications)
                    {
                        Console.WriteLine($"        :  {appSub.Title}");
                    }

                var query = applications.Where(a => a.Title == "Lead Software Developer Senior");

                   if(query != null && query.Count() > 0 )
                    {
                        if (query.Count() < 2)
                        {
                            var application = query.First();
                            UpdateApplication(contextDb, application, "Lead Software Developer Senior");
                            DeleteApplication(contextDb, application);
                        }
                        else
                        {

                        }
                    }
                
            } 
            static void UpdateApplication(ApplicantsContext ctx, ApplicantSubmission application , string title)
            {
                application.Title = title;  
                using (ctx)
                {
                    ctx.Update(application);  
                    ctx.SaveChanges();
                }
            } 
            static void DeleteApplication(ApplicantsContext ctx, List<ApplicantSubmission> applications)
            { 
                using (ctx)
                {
                    ctx.Remove(applicapplicationsationId); 
                    ctx.SaveChanges();
                }
            }
            static  IQueryable<ApplicantSubmission> GetApplicantions(ApplicantsContext ctx, int UserId)
            {
                var applications = ctx.ApplicantSubmissions
                    .Where(x => x.ApplicantId == UserId);


                return applications;
            }
            static Applicant GetUser(ApplicantsContext ctx, string Name)
            {
                var applicant = ctx.Applicants
                    .Where(x => x.Name == Name)
                     .Include(x => x.ApplicantSubmissions)
                     .FirstOrDefault();

                return applicant;
            }
            static void AddApplication(ApplicantsContext ctx, Applicant user)
            {
                var job = new Job();
                using (var jobCtx = new JobsContext())
                {
                    var jobs = jobCtx.Jobs.ToArray();
                    Random random = new Random();
                    int start2 = random.Next(0, jobs.Count());
                    job = jobs[start2];

                    var applicantSubmission = new ApplicantSubmission()
                    {
                        Applicant = user,
                        JobId = job.JobId,
                        ApplicantId = user.ApplicantId,
                        Title = job.Title,
                        SubmissionDate = DateTime.Now
                    };

                    try
                    {
                        ctx.ApplicantSubmissions.Add(applicantSubmission);

                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }



            }
        }



    }
}
