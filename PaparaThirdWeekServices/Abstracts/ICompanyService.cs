using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.Abstracts
{
    public interface ICompanyService
    {
        Task<List<CompanyDto>> GetAll();
        Task<CompanyDto> Get(int id);
        Task Add(CompanyDto company);
        Task Delete(int id);
        Task Update(CompanyDto companyDto, int id);
        

    }
}
