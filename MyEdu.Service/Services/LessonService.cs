using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyEdu.Data.Repositories.Interfaces;
using MyEdu.Domain.Common;
using MyEdu.Domain.Configurations;
using MyEdu.Domain.Entities.Lessons;
using MyEdu.Service.DTOs;
using MyEdu.Service.Extensions;
using MyEdu.Service.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VideoLibrary;
using YoutubeExplode;

namespace MyEdu.Service.Services
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LessonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<Lesson>> CreateAsync(LessonDto lessonDto)
        {
            var response = new BaseResponse<Lesson>();

            
            
            var existPart = await unitOfWork.Parts.GetAsync(p => p.Id == lessonDto.PartId);
            if (existPart is null)
            {
                response.Error = new ErrorResponse(404, "Part is not found");
                return response;
            }

            var youtube = new YoutubeClient();
            var video = await youtube.Videos.GetAsync("https://www.youtube.com/watch?v=" + lessonDto.VideoUrl);

            // create after checking success
            var mappedLesson = new Lesson
            {
                Name = video.Title,
                Part = existPart,
                Length = video.Duration.ToString(),
                VideoUrl = lessonDto.VideoUrl
            };

            existPart.Lessons.Add(mappedLesson);

            await unitOfWork.Parts.UpdateAsync(existPart);

            var result = await unitOfWork.Lessons.CreateAsync(mappedLesson);

            await unitOfWork.SaveChangesAsync();

            result.VideoUrl = "https://www.youtube.com/watch?v=" + result.VideoUrl;

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Lesson, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist part
            var existLesson = await unitOfWork.Lessons.GetAsync(expression);
            if (existLesson is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            var result = await unitOfWork.Lessons.UpdateAsync(existLesson);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IQueryable<Lesson>>> GetAllAsync(PaginationParams @params, Expression<Func<Lesson, bool>> expression = null)
        {
            var response = new BaseResponse<IQueryable<Lesson>>();

            var lessons = await unitOfWork.Lessons.GetAllAsync(expression);

            response.Data = lessons.ToPagedList(@params).Include(p => p.Part);

            return response;
        }

        public async Task<BaseResponse<Lesson>> GetAsync(Expression<Func<Lesson, bool>> expression)
        {
            var response = new BaseResponse<Lesson>();

            var lesson = (await unitOfWork.Lessons.GetAllAsync(expression)).Include(p => p.Part).FirstOrDefault();
            if (lesson is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            response.Data = lesson;

            return response;
        }

        public async Task<BaseResponse<Lesson>> UpdateAsync(long id, LessonDto lessonDto)
        {
            var response = new BaseResponse<Lesson>();

            var lesson = await unitOfWork.Lessons.GetAsync(p => p.Id == id);

            if (lesson is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            lesson = mapper.Map(lessonDto, lesson);

            await unitOfWork.Lessons.UpdateAsync(lesson);

            await unitOfWork.SaveChangesAsync();

            response.Data = lesson;

            return response;
        }
    }
}