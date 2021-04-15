using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FLAGS.API.Controllers
{
    public class Response
    { 
        public Usuario resultOk { get; set; }
    }

    public class Usuario
    { 
        public string nombre { get; set; }
        public string username { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        public UsuarioController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://apima-flags.azure-api.net/usuario/api/Usuario/GetByUsername?username=fmoral");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var usuario = await JsonSerializer.DeserializeAsync<Response>(responseStream);
                return Ok(usuario);
            }
            else
            {

                return Ok("ERROR");
            }
        }
    }
}
