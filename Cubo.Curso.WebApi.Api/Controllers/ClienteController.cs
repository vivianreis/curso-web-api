using Cubo.Curso.WebApi.Dominio.Entidades;
using Cubo.Curso.WebApi.Dominio.Validacoes;
using Cubo.Curso.WebApi.Servicos.Servicos;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cubo.Curso.WebApi.Api.Controllers
{
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        private ServicoBase<Cliente> servico = new ServicoBase<Cliente>();

        [SwaggerResponse(200, "ok")]
        [SwaggerResponse(400, "BadRequest")]
        [SwaggerResponse(200, "NotFound")]
        [HttpPost]
        [Route("incluir")]
        public IHttpActionResult Post([FromBody] Cliente item)
        {
            servico.Post<ClienteValidator>(item);
            return Ok();
        }

        [SwaggerResponse(200, "ok")]
        [SwaggerResponse(400, "BadRequest")]
        [SwaggerResponse(200, "NotFound")]
        [HttpPut]
        [Route("atualizar")]
        public IHttpActionResult Put([FromBody] Cliente item)
        {
            servico.Put<ClienteValidator>(item);
            return Ok();
        }

        [SwaggerResponse(200, "ok")]
        [SwaggerResponse(400, "BadRequest")]
        [SwaggerResponse(200, "NotFound")]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            servico.Delete(id);
            return Ok();
        }

        [SwaggerResponse(200, "ok")]
        [SwaggerResponse(400, "BadRequest")]
        [SwaggerResponse(200, "NotFound")]
        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Get()
        {
            var data = servico.Get();

            if (!data.Any())
                return NotFound();

            return Ok(data);
        }

        [SwaggerResponse(200, "ok")]
        [SwaggerResponse(400, "BadRequest")]
        [SwaggerResponse(200, "NotFound")]
        [HttpGet]
        [Route("buscar/{id}")]
        public IHttpActionResult Get(int id)
        {
            var data = servico.Get(id);

            if (data == null)
                return NotFound();

            return Ok();
        }
    }
}
