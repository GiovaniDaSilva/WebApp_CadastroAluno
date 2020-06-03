using App.Domain;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Aluno")]
    public class AlunoController : ApiController
    {
              
     
        // GET: api/Aluno
        [HttpGet]
        [Route("RecuperarDB")]
        [Authorize(Roles =Funcao.Professor)]
        public IHttpActionResult RecuperarDB()
        {
            try
            {
                AlunoModel aluno = new AlunoModel();

                return Ok(aluno.ListarAlunoDB());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }


        // GET: api/Aluno
        [HttpGet]
        [Route("RecuperarDB/{id}")]
        public IHttpActionResult RecuperarDB(int? id = null)
        {
            try
            {
                AlunoModel aluno = new AlunoModel();

                return Ok(aluno.ListarAlunoDB(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        // POST: api/Aluno
        [HttpPost]
        public IHttpActionResult Post(AlunoDTO aluno)
        {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try
            {
                AlunoModel _aluno = new AlunoModel();
                _aluno.InserirDB(aluno);
                return Ok(_aluno.ListarAlunoDB());
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        // PUT: api/Aluno/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]AlunoDTO aluno)
        {

            try
            {
                AlunoModel _aluno = new AlunoModel();
                aluno.id = id;
                _aluno.AtualizarDB(aluno);
                return Ok(_aluno.ListarAlunoDB(id));
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }

        // DELETE: api/Aluno/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                AlunoModel _aluno = new AlunoModel();
                _aluno.DeletarDB(id);
                return Ok("Deletado com sucesso");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }


        //Os metodos abaixo, estão apenas de exemplo, acessando o arquivo json
        // GET: api/Aluno
        [Obsolete]
        [HttpGet]
        [Route("Recuperar")]
        public IHttpActionResult Recuperar()
        {

            try
            {
                AlunoModel aluno = new AlunoModel();

                return Ok(aluno.ListarAluno());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // GET: api/Aluno/5
        [Obsolete]
        [HttpGet]
        [Route("Recuperar/{id}")]
        public AlunoDTO Get(int id)
        {
            AlunoModel aluno = new AlunoModel();

            return aluno.ListarAluno().Where(x => x.id == id).FirstOrDefault();


        }

        [Obsolete]
        [HttpGet]
        //[Route(@"RecuperarPorDataNome/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlenght(4)}")]
        [Route(@"RecuperarPorDataNome/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlength(4)}")]

        public IHttpActionResult Recuperar(string data, string nome)
        {
            try
            {
                AlunoModel aluno = new AlunoModel();

                IEnumerable<AlunoDTO> alunos = aluno.ListarAluno().Where(x => x.data == data || x.nome == nome);

                if (!alunos.Any())
                {
                    return NotFound();
                }

                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

       
    }
}
