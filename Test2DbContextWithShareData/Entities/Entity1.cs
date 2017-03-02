using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test2DbContextWithShareData.Entities
{
    public class Entity1
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Entity2> Entities2 { get; set; }

    }
}
