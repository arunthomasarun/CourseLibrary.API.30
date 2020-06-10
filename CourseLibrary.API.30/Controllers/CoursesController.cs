using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API._30.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API._30.Controllers
{
    [Route("api/authors/{authorId}/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper)
        {
            this._courseLibraryRepository = courseLibraryRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
                return NotFound();

            var courses = _courseLibraryRepository.GetCourses(authorId);
            return Ok(_mapper.Map<IEnumerable<CoursesDTO>>(courses));
        }


        [HttpGet("{courseId}")]
        public IActionResult GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
                return NotFound();

            var course = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (course == null)
                return NotFound();

            return Ok(_mapper.Map<CoursesDTO>(course));
        }

    }
}
