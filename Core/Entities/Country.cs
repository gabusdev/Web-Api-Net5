using System.Collections.Generic;

namespace Core.Models
{
    public class Country
    {
        public Country()
        {
            Hotels = new HashSet<Hotel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
