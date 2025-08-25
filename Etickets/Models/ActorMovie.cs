namespace Etickets.Models
{
    public class ActorMovie
    {
        public int MoviesId { get; set; }

        public int ActorsId { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }


    }
}
