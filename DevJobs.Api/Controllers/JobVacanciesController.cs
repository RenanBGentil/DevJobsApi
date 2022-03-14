using DevJobs.Api.Controllers.Models;
using DevJobs.Api.Entities;
using DevJobs.Api.Persistence;
using DevJobs.Api.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DevJobs.Api.Controllers
{
    [ApiController]
    [Route("api/job-vacancies")]
    public class JobVacanciesController : ControllerBase
    { 
        private readonly IJobVacancyRepository _repository;
        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var jobVacancies = _repository.GetAll();
            return Ok(jobVacancies);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null) return NotFound();

            return Ok(jobVacancy);
        }
        /// <summary>
        /// Cadastrar uma vaga de emprego
        /// </summary>
        /// <remarks>
        /// {
        ///"title": "Dev .Net Jr",
        ///"description": "Vaga para sustentações de aplicações .Net Core.",
        ///"company": "LuisDev",
        ///"isRemote": true,
        ///"salaryRange": "3000-5000"
        ///}
        /// </remarks>
        /// <param name="model">Dados da vaga.</param>
        /// <returns>Objeto recém - criado</returns>
        /// <response code="201">Sucesso.</response>
        /// <response code="400">Dados Inválidos.</response>
        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model) {
            Log.Information("POST JobVacancy chamado");
            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
                );
            _repository.Add(jobVacancy);

            if(jobVacancy.Title.Length > 30)
            {
                return BadRequest("Título precisa ter menos de 30 caracteres");
            }
           
            return CreatedAtAction("GetById", new { id = jobVacancy.Id}, jobVacancy); 
        }

        [HttpPut("{id}")]
        public IActionResult Put(UpdateJobVacancyInputModel model, int id) {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null) return NotFound();

            jobVacancy.Update(model.Title, model.Description);
            _repository.Update(jobVacancy);
            return NoContent();
        }
    }
}
