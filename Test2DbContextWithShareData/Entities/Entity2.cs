using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test2DbContextWithShareData.Entities
{
    public class Entity2
    {
        public  int Id { get; set; }

        public string Name { get; set; }

        public int Entity1Id { get; set; }

        [ForeignKey("Entity1Id")]
        public Entity1 Entity1 { get; set; }
    }
}
