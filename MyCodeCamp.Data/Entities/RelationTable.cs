using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeCamp.Data.Entities
{
    public class RelationTable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid TestTableId { get; set; }

        [ForeignKey("TestTableId")]
        public TestTable TestTable { get; set; }
    }
}
