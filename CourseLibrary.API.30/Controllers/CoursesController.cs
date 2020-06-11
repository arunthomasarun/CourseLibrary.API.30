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


        [HttpGet("{courseId}", Name = nameof(GetCourseForAuthor))]
        public IActionResult GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
                return NotFound();

            var course = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (course == null)
                return NotFound();

            return Ok(_mapper.Map<CoursesDTO>(course));
        }

        [HttpPost]
        public IActionResult CreateCoursesForAuthor(Guid authorId, CoursesForCreationDTO courses)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
                return NotFound();

            if (authorId != courses.AuthorId)
                return BadRequest();

            var coursesDTO = _mapper.Map<Entities.Course>(courses);

            _courseLibraryRepository.AddCourse(authorId, coursesDTO);
            _courseLibraryRepository.Save();

            var courseToReturn = _mapper.Map<Models.CoursesDTO>(coursesDTO);
            return CreatedAtRoute(nameof(GetCourseForAuthor), new { authorId = authorId, courseId = courseToReturn.Id}, courseToReturn);
        }
    }
}
