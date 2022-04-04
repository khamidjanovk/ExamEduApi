using AutoMapper;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Parts;
using MyEdu.Service.DTOs;
using MyEdu.Service.Extensions;
using MyEdu.Service.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyEdu.Service.Services
{
    public class PartService : IPartService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public PartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<BaseResponse<Part>> CreateAsync(PartDto partDto)
        {
            var response = new BaseResponse<Part>();

            // check for user
            var existPart = await unitOfWork.Parts.GetAsync(p => p.Name == partDto.Name);
            if (existPart is not null)
            {
                response.Error = new ErrorResponse(400, "User is exist");
                return response;
            }

            // create after checking success
            var mappedPart = mapper.Map<Part>(partDto);

            var result = await unitOfWork.Parts.CreateAsync(mappedPart);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Part, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IQueryable<Part>>> GetAllAsync(PaginationParams @params, Expression<Func<Part, bool>> expression = null)
        {
            var response = new BaseResponse<IQueryable<Part>>();

            var parts = await unitOfWork.Parts.GetAllAsync(expression);

            response.Data = parts.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Part>> GetAsync(Expression<Func<Part, bool>> expression)
        {
            var response = new BaseResponse<Part>();

            var part = await unitOfWork.Parts.GetAsync(expression);
            if (part is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            response.Data = part;

            return response;
        }

        public Task<BaseResponse<Part>> UpdateAsync(long id, PartDto partDto)
        {
            throw new NotImplementedException();
        }
    }
}