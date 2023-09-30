namespace MoviesAppAPI.Models.ModelViews
{
    public class Registrar
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string Usuario { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public int? IdUsuario { get; set; }
    }
}
