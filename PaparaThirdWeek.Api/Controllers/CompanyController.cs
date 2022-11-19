using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PaparaThirdWeek.Api.Filters;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.DTOs;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ICompanyService companyService;
        private readonly IMapper mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper, 
            IMemoryCache memoryCache)
        {
            this.companyService = companyService;
            this.mapper = mapper;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]      
        public async Task<IActionResult> AddCompany(CompanyDto companyDto)
        {
            await companyService.Add(companyDto);
            return Ok();
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetCompany()
        {
            var companyList = await companyService.GetAll();
            return Ok(companyList);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await companyService.Get(id);
            return Ok(company);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(CompanyDto companyDto, int id)
        {

            await companyService.Update(companyDto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await companyService.Delete(id);
            return Ok();
        }
    }
}
