using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeCamp.Data.Entities
{
    public class TestTable
    {

        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public ICollection<RelationTable> RelationTables { get; set; }
    }
}
