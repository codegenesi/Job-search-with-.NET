using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jobpedia.Controllers;
using Jobpedia.Data;
using Jobpedia.Models;
using Xunit;


namespace Jobpedia.Tests
{
    public class JobControllerTest
    {
        [Fact]
        public async Task Index_Basic_Test()
        {
            using (var testDb = new ApplicationDbContext(this.GetTestDbOpts()))
            {
                var testCtrl = new JobController(testDb);
                var res = await testCtrl.Index();
                var resVr = Assert.IsType<ViewResult>(res);
                Assert.IsAssignableFrom<IEnumerable<Job>>(resVr.ViewData.Model);
            }
        }

        [Fact]
        public async Task Add_And_Remove_Test()
        {
            using (var testDb = new ApplicationDbContext(this.GetTestDbOpts()))
            {
                var testCtrl = new JobController(testDb);
                var fakeJobs = MakeFakeJobs(3);
                
                // Adding Jobs
                foreach (var jobs in fakeJobs)
                {
                    var res = await testCtrl.Create(jobs);
                    var resVr = Assert.IsType<RedirectToActionResult>(res);
                    Assert.Equal("Index", resVr.ActionName);
                }

                // Testing Saved Values
                var idxRes = await testCtrl.Index();
                var idxResVr = Assert.IsType<ViewResult>(idxRes);
                var returnedJobs = Assert.IsAssignableFrom<IEnumerable<Job>>(idxResVr.ViewData.Model);
                foreach (var job in fakeJobs)
                {
                    Assert.Contains(job, returnedJobs);
                }

                // Removing All Existing Jobs
                foreach (var job in returnedJobs)
                {
                    var res = await testCtrl.DeleteConfirmed(job.Id);
                    var resVr = Assert.IsType<RedirectToActionResult>(res);
                    Assert.Equal("Index", resVr.ActionName);
                }
            }
        }

        // Create the DB Context to use (note this should be a test database)
        private DbContextOptions<ApplicationDbContext> GetTestDbOpts()
        {
            var opts = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestJobPedia").Options;
            return opts;
        }

        private List<Job> MakeFakeJobs(int i)
        {
            var jobs = new List<Job>();
            for (int j = 0; j < i; j++)
            {
                jobs.Add(new Job
                {
                    Name = $"test{j}",
                    Category = $"testcat{j}"
                });
            }
            return jobs;
        }
    }
}
