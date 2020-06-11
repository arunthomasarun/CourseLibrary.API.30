using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API._30.Helpers;
using CourseLibrary.API._30.Models;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API._30.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            this._courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AuthorsDTO>))]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors().ToList();
            //return Ok(authorsFromRepo);

            //var authors = new List<AuthorsDTO>();
            //foreach (var author in authorsFromRepo)
            //{
            //    authors.Add(new AuthorsDTO()
            //    {
            //        Id = author.Id,
            //        Name = $"{author.FirstName} {author.LastName}",
            //        Age = author.DateOfBirth.GetAge(), //Extension method
            //        MainCategory = author.MainCategory
            //    }) ;
            //}

            //return Ok(authors);

            return Ok(_mapper.Map<IEnumerable<AuthorsDTO>>(authorsFromRepo));
        }


        [HttpGet("{mainCategory}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AuthorsDTO>))]
        public IActionResult GetAuthors([FromQuery] string mainCategory, [FromQuery] string searchQuery)
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors(mainCategory, searchQuery);
           
            return Ok(_mapper.Map<IEnumerable<AuthorsDTO>>(authorsFromRepo));
        }


        [HttpGet("{authorId:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Author))]
        public IActionResult GetAuthor(Guid authorId)
        {
            var author = _courseLibraryRepository.GetAuthor(authorId);
            if (author == null)
                return StatusCode(StatusCodes.Status404NotFound, "Author not found!");

            return Ok(author);
        }
    }
}
