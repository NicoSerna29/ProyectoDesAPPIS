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
        [HttpGet("Item")]
        public Usuario Item(int CC)
        {
            return usuarios[CC];
        }
        [HttpGet("Detalle")]
        public dynamic Detail(int CC)
        {
            //var hdr_key = Request.Headers["key_app"];
            var hdr_key = Request.Headers.Where(x => x.Key.Equals("key_app")).FirstOrDefault();

            if (hdr_key.Value.Count == 0)
            {
                return new
                {
                    code = "API ERROR",
                    message = "NO ESTÁ AUTORIZADO",
                    Detail = "N/A"

                };
            }
            else
            {
                if (hdr_key.Value != "x1234") 
                {
                    return new
                    {
                        code = "API ERROR",
                        message = "KEY INVALIDO",
                        Detail = "N/A"

                    };

                }
            }
            var item = usuarios.Where(x => x.cc == CC).ToList();
            if (item.Count > 0)
            {

                if (CC == 0 )
                {
                    return new
                    {
                        code = "OK",
                        message = "USUARIO ENCONTRADO",
                        Detail = "N/A"
                    };
                }
                else
                {
                    return item;
                }
            }
            else
            {
                return new
                {
                    code = "API COUNT",
                    message = "NO EXISTEN USUARIOS REGISTRADOS CON ESE NUMERO DE DOCUMENTO",
                    Detail = "N/A"
                };
            }
        }
    }
}

