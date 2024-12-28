namespace MyCleanArchitectureApp.UI.ViewModel
{
    public class ReviewViewModel
    {
        public int MovieId { get; set; }
        public string Text { get; set; } // Using "Text" to match the Review class
        public int Rating { get; set; } // Rating from 1 to 5
    }

}
