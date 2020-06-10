using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API._30.Models
{
    public class AuthorsDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string MainCategory { get; set; }
    }
}
