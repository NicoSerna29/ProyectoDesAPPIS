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
                id = 1,
                name  = "Nicolas",
                Apellido  = "Serna",
                eMail = "n.serna@utp.edu.co",
                tel  = 32188100027,
                direccion = "Galicia"
            },
            new Usuario
            {
                id = 2,
                name  = "Nicolas",
                Apellido  = "Serna",
                eMail = "n.serna@utp.edu.co",
                tel  = 32188100027,
                direccion = "Galicia"
            }
        };
        [HttpGet("Consultar")]
        public List<Usuario> Consultar()
        {
            return usuarios;
        }
        [HttpGet("Item")]
        public Usuario Item(int ID)
        {
            return usuarios[ID];
        }
        [HttpGet("Detalle")]
        public dynamic Detail(int ID)
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
            var item = usuarios.Where(x => x.id == ID).ToList();
            if (item.Count > 0)
            {

                if (ID == 0)
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
        [HttpPost("Save")]
        public IActionResult Save([FromBody] Usuario item)
        {
            //var hdr_key = Request.Headers["key_app"];
            var hdr_key = Request.Headers.Where(x => x.Key.Equals("key_app")).FirstOrDefault();

            if (hdr_key.Value.Count == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    code = "API001",
                    message = "Falta Parametro",
                    Detail = ""
                });


            }
            else
            {
                if (hdr_key.Value != "x1234")
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new
                    {
                        code = "API002",
                        message = "No Autorizado",
                        Detail = ""
                    });
                }


                usuarios.Add(item);
                return StatusCode(StatusCodes.Status201Created, new
                {
                    code = "OK",
                    message = "Datos Almacenados",
                    Detail = item
                });

            }

        }




        [HttpPut("update")]
        public IActionResult update([FromBody] Usuario item)
        {
            //var hdr_key = Request.Headers["key_app"];
            var hdr_key = Request.Headers.Where(x => x.Key.Equals("key_app")).FirstOrDefault();

            if (hdr_key.Value.Count == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    code = "API001",
                    message = "Falta Parametro",
                    Detail = ""
                });


            }
            else
            {
                if (hdr_key.Value != "x1234")
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new
                    {
                        code = "API002",
                        message = "No Autorizado",
                        Detail = ""
                    });
                }



                foreach (var det in usuarios.Where(x => x.id == item.id).ToList())
                {
                    det.name = item.name;
                }


                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Datos Modificados",
                    Detail = item
                });


            }

        }



        [HttpDelete("Delete")]
        public IActionResult delete(int ID)
        {
            //var hdr_key = Request.Headers["key_app"];
            var hdr_key = Request.Headers.Where(x => x.Key.Equals("key_app")).FirstOrDefault();

            if (hdr_key.Value.Count == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    code = "API001",
                    message = "Falta Parametro",
                    Detail = ""
                });


            }
            else
            {
                if (hdr_key.Value != "x1234")
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new
                    {
                        code = "API002",
                        message = "No Autorizado",
                        Detail = ""
                    });
                }


                if (ID.Equals(0))
                {
                    return StatusCode(StatusCodes.Status200OK, new
                    {
                        code = "OK",
                        message = "CC invalido",
                        Detail = "0"
                    });
                }
                else
                {

                    Usuario objprueba = (Usuario)usuarios.Where(x => x.id == ID).First();
                    if (objprueba != null)
                        usuarios.Remove(objprueba);

                    return StatusCode(StatusCodes.Status200OK, new
                    {
                        code = "OK",
                        message = "Datos Eliminados",
                        Detail = ID
                    });
                }

            }
        }
    }
}

