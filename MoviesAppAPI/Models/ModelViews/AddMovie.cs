namespace MoviesAppAPI.Models.ModelViews
{
    public class AddMovie
    {
        public string nombre { get; set; }
        public string autor { get; set; }
        public string descripccion { get; set; }
        public string fechaLanzamiento { get; set; }
        public IFormFile imagen { get; set; }
    }
}
