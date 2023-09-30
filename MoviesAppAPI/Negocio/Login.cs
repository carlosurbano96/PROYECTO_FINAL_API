using MoviesAppAPI.Models;
using MoviesAppAPI.Models.ModelViews;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace MoviesAppAPI.Negocio
{
    public class Login
    {
        private readonly string _miServicio;

        public Login(IConfiguration servicio)
        {
            _miServicio = servicio.GetSection("RutaArchivos")["ImagenesMovies"];
        }

        public List<Usuario> GetUsers()
        {
            MoviesAppContext db = new MoviesAppContext();
            List<Usuario> listaUsers = new List<Usuario>();

            listaUsers = db.Usuarios.ToList();
            return listaUsers;
        }

        public bool Registra(Registrar Form)
        {
            MoviesAppContext _dbcontext = new MoviesAppContext();
            Usuario user = new Usuario();
            try
            {
                //registra usuario
                user.Usuario1 = Form.Usuario;
                user.Pass = Form.Pass;
                _dbcontext.Usuarios.Add(user);
                _dbcontext.SaveChanges();

                //consulta ultimo usuario regustrado
                user = _dbcontext.Usuarios.OrderBy(x => x.IdUsuario).LastOrDefault();

                //Registra en tabla persona
                Persona persona = new Persona();
                persona.Nombre = Form.Nombre;
                persona.Apellido = Form.Apellido;
                persona.IdUsuario = user.IdUsuario;
                _dbcontext.Personas.Add(persona);
                _dbcontext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IniciarSesion (string User, string Pass)
        {
            MoviesAppContext _dbcontext = new MoviesAppContext();
            try
            {
                var query = _dbcontext.Usuarios.Where(u => u.Usuario1 == User && u.Pass == Pass).FirstOrDefault();
                if (query != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool AddMovie (AddMovie Movie)
        {
            MoviesAppContext _dbcontext = new MoviesAppContext();
            Movie objMovie = new Movie();
            try
            {
                

                objMovie.Nombre = Movie.nombre;
                objMovie.Autor = Movie.autor;
                objMovie.Descripccion = Movie.descripccion;
                objMovie.FechaLanzamiento = Movie.fechaLanzamiento;
                objMovie.NombreImagenOriginal = Movie.imagen.FileName;
                var indexS = objMovie.NombreImagenOriginal.LastIndexOf('.');
                var indexE = objMovie.NombreImagenOriginal.Length;
                var extencion = objMovie.NombreImagenOriginal.Substring(indexS+1, indexE - indexS-1);
                objMovie.NombreImagenServidor = "imagen_"+DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")+"."+extencion;
                objMovie.RutaImagen = _miServicio+"\\"+ objMovie.NombreImagenServidor;
                objMovie.RutaImagen = _miServicio;

                string rutaDocumento = Path.Combine(_miServicio, objMovie.NombreImagenServidor);
                using (FileStream newFile = System.IO.File.Create(rutaDocumento))
                {
                    Movie.imagen.CopyTo(newFile);
                    newFile.Flush();
                }
                _dbcontext.Movies.Add(objMovie);
                _dbcontext.SaveChanges();
                return true;


            }
            catch (Exception)
            {
                return false;              
            }
        }

        public List<Movie> GetListMovies()
        {
            MoviesAppContext _dbcontext = new MoviesAppContext();
            List<Movie> list = new List<Movie>();
            list = _dbcontext.Movies.ToList();
            return list;
        }

        public Movie GetMovie(int id)
        {
            MoviesAppContext _dbcontext = new MoviesAppContext();
            Movie movie = _dbcontext.Movies.Where(x => x.IdMovie == id).FirstOrDefault();
            return movie;
        }
    }
}
