using System;
using System.Collections.Generic;

namespace MoviesAppAPI.Models;

public partial class Movie
{
    public int IdMovie { get; set; }

    public string? Nombre { get; set; }

    public string? Autor { get; set; }

    public string? Descripccion { get; set; }

    public string? FechaLanzamiento { get; set; }

    public string? RutaImagen { get; set; }

    public string? NombreImagenOriginal { get; set; }

    public string? NombreImagenServidor { get; set; }
}
