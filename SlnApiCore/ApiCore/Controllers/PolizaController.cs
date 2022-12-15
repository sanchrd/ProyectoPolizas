using ApiCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolizaController : ControllerBase
    {
        private readonly string cadenaSQL;
        public PolizaController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }


        //***********************
        //****  Metodo Listar
        //***********************
        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Poliza> lista = new List<Poliza>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("Sp_Poliza_Listar", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Poliza()
                            {
                                IdPoliza          = rd.GetInt32("IdPoliza"),
                                Placa             = rd.GetString("Placa"),
                                IdCiudad          = rd.GetInt32("IdCiudad"),
                                Nombre_Tomador    = rd.GetString("Nombre_Tomador"),
                                Fecha_Inicio      = rd.GetDateTime("Fecha_Inicio"),
                                Fecha_Fin         = rd.GetDateTime("Fecha_Fin"),
                                Fecha_Vencimiento = rd.GetDateTime("Fecha_Vencimiento")
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });
            }
        }


        //***********************
        //****  Metodo Obtener
        //***********************
        [HttpGet]
        [Route("Obtener/{IdPoliza:int}")]
        public IActionResult Obtener(int IdPoliza)
        {
            List<Poliza> lista = new List<Poliza>();

            Poliza poliza = new Poliza();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("Sp_Poliza_Listar", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Poliza()
                            {
                                IdPoliza          = rd.GetInt32("IdPoliza"),
                                Placa             = rd.GetString("Placa"),
                                IdCiudad          = rd.GetInt32("IdCiudad"),
                                Nombre_Tomador    = rd.GetString("Nombre_Tomador"),
                                Fecha_Inicio      = rd.GetDateTime("Fecha_Inicio"),
                                Fecha_Fin         = rd.GetDateTime("Fecha_Fin"),
                                Fecha_Vencimiento = rd.GetDateTime("Fecha_Vencimiento")
                            });
                        }
                    }
                }
                poliza = lista.Where(item => item.IdPoliza == IdPoliza).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = poliza });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = poliza });
            }
        }

   
        //***********************
        //****  Metodo Guardar
        //***********************
        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Poliza objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("Sp_Poliza_Guardar", conexion);

                    cmd.Parameters.AddWithValue("Placa", objeto.Placa);
                    cmd.Parameters.AddWithValue("IdCiudad", objeto.IdCiudad);
                    cmd.Parameters.AddWithValue("Nombre_Tomador", objeto.Nombre_Tomador);
                    cmd.Parameters.AddWithValue("Fecha_Inicio", objeto.Fecha_Inicio);
                    cmd.Parameters.AddWithValue("Fecha_Fin", objeto.Fecha_Fin);
                    cmd.Parameters.AddWithValue("Fecha_Vencimiento", objeto.Fecha_Vencimiento);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }


        //***********************
        //****  Metodo Editar 
        //***********************
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Poliza objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("Sp_Poliza_Editar", conexion);

                    cmd.Parameters.AddWithValue("IdPoliza", objeto.IdPoliza == 0 ? DBNull.Value : objeto.IdPoliza);
                    cmd.Parameters.AddWithValue("Nombre_Tomador", objeto.Nombre_Tomador is null ? DBNull.Value : objeto.Nombre_Tomador);
                    cmd.Parameters.AddWithValue("IdCiudad", objeto.IdCiudad == 0 ? DBNull.Value : objeto.IdCiudad);
                    cmd.Parameters.AddWithValue("Placa", objeto.Placa  is null ? DBNull.Value : objeto.Placa);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Editado" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }



        //***********************
        //****  Metodo Eliminar 
        //***********************
        [HttpDelete]
        [Route("Eliminar/{idPoliza:int}")]
        public IActionResult Eliminar(int idPoliza)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("Sp_Poliza_Eliminar", conexion);

                    cmd.Parameters.AddWithValue("idPoliza", idPoliza);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Poliza Eliminada" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
