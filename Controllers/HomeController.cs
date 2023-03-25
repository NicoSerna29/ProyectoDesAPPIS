using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Punto1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        public static List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario
            {
                nombre = "Nicolas",
                cc = 1112039498,
                direccion = "cra 7, casa 4",
                telefono = "3218810027",
                nacimiento = DateTime.Parse("2004-07-29")
            },
            new Usuario
            {
                nombre = "Nicolas",
                cc = 1112039498,
                direccion = "cra 7, casa 4",
                telefono = "3218810027",
                nacimiento = DateTime.Parse("2004-07-29")
            }
        };
        [HttpGet("Consultar")]
        public List<Usuario> Consultar()
        {
            return usuarios;
        }
        [HttpGet("Consultar 2")]
        public string Consultar2()
        {
            return "Hola Mundo 2";
        }
    }
}
