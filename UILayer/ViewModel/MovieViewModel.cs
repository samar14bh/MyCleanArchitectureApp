using Microsoft.AspNetCore.Mvc.Rendering;



public class MovieViewModel
{
    public int Id { get; set; }  // Unique identifier, but might not be set when creating a new movie

    public string Name { get; set; }

    public int GenreId { get; set; }

    public double AverageRating { get; set; }


}
