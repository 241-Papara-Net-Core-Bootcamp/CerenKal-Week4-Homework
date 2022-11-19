using AutoMapper;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Services.Concretes
{
    public class CompanyServices : ICompanyService
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IMapper _mapper;

        public CompanyServices(IRepository<Company> companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task Add(CompanyDto companydto)
        {
            var company = _mapper.Map<Company>(companydto);
            await _companyRepository.Add(company);
        }

        public async Task Delete(int id)
        {
            var company = await _companyRepository.GetById(id);
            if (company is null)
                throw new Exception("not found");
            await _companyRepository.Delete(id);
        }

        public async Task<CompanyDto> Get(int id)
        {
            var company = await _companyRepository.GetById(id);
            if (company is null)
                throw new Exception("not found");
            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }

        public async Task<List<CompanyDto>> GetAll()
        {
            var companies = await _companyRepository.GetAll();
            var companyDto = _mapper.Map<List<CompanyDto>>(companies);
            return companyDto;
        }

        public async Task Update(CompanyDto companyDto, int id)
        {
            var company = await _companyRepository.GetById(id);
            if (company is null)
                throw new Exception("not found");
            var updatedCompany = _mapper.Map<Company>(companyDto);
            updatedCompany.Id = id;
            await _companyRepository.Add(updatedCompany);
        }
    }
}
