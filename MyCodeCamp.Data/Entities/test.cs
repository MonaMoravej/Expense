using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeCamp.Data.Entities
{
    public class test
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public int ProgramId { get; set; }


        [ForeignKey("ProgramId")]
        public virtual test2 Program { get; set; }

    }
}
