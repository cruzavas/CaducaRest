using System;
using System.Collections.Generic;

#nullable disable

namespace CaducaRest.sakila
{
    public partial class Actor
    {
        public Actor()
        {
            FilmActors = new HashSet<FilmActor>();
        }

        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastUpdate { get; set; }

        public virtual ICollection<FilmActor> FilmActors { get; set; }
    }
}
