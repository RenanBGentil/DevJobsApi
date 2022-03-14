using DevJobs.Api.Entities;
using DevJobs.Api.Models;
using DevJobs.Api.Persistence;
using DevJobs.Api.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DevJobs.Api.Controllers
{
    [ApiController]
    [Route("api/job-vacancies/{id}/applications")]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;   
        public JobApplicationController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model) {
            var jobVacancy = _repository.GetById(id);
            if (jobVacancy == null){ return NotFound(); }

            var application = new JobApplication(
                model.ApplicantName,
                model.ApplicantEmail,
                id);

            _repository.AddApplication(application);

            return NoContent();
        }    
    }
}
