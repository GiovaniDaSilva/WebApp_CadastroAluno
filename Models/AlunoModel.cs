using App.Domain;
using App.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace WebApp.Models
{
    public class AlunoModel
    {
        
        public List<AlunoDTO> ListarAluno()
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data\Base.json");
            var json = System.IO.File.ReadAllText(caminhoArquivo);
            var listaAlunos = JsonConvert.DeserializeObject<List<AlunoDTO>>(json);

            return listaAlunos;
        }

        public List<AlunoDTO> ListarAlunoDB(int? id = null)
        {

            try
            {
                var alunoDB = new AlunoDAO();

                return alunoDB.ListarAluno(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar Alunos: Erro = {ex.Message}");
            }
        }


        public bool RescreverArquivo(List<AlunoDTO> listaAlunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data\Base.json");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);

            File.WriteAllText(caminhoArquivo, json);

            return true;
        }


        public AlunoDTO Inserir(AlunoDTO Aluno)
        {
            var listaAlunos = this.ListarAluno();

            var maxID = 1;

            if (listaAlunos.Count > 0)
            {
                maxID = listaAlunos.Max(p => p.id);
            }

            Aluno.id = maxID + 1;
            listaAlunos.Add(Aluno);
            RescreverArquivo(listaAlunos);
            return Aluno;

        }
        public void InserirDB(AlunoDTO Aluno)
        {
            try
            {
                var alunoDB = new AlunoDAO();
                alunoDB.InserirAlunoDB(Aluno);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir aluno: Erro = {ex.Message}");
            }
                        
        }

        public void  AtualizarDB(AlunoDTO Aluno)
        {
            try
            {
                var alunoDB = new AlunoDAO();
                alunoDB.AtualizarAlunoDB(Aluno);                
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar aluno: Erro = {ex.Message}");
            }

        }

        public AlunoDTO Atualizar(int id, AlunoDTO aluno)
        {
            var listaAlunos = this.ListarAluno();
            var itemIndex = listaAlunos.FindIndex(p => p.id == id);

            if (itemIndex >= 0)
            {
                aluno.id = id;
                listaAlunos[itemIndex] = aluno;
            }
            else
            {
                return null;
            }

            RescreverArquivo(listaAlunos);
            return aluno;
        }

        public bool Deletar(int id)
        {
            var listaAlunos = ListarAluno();

            var itemIndex = listaAlunos.FindIndex(p => p.id == id);

            if (itemIndex >= 0)
            {
                listaAlunos.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            RescreverArquivo(listaAlunos);
            return true;
        }

        public void DeletarDB(int id)
        {
            try
            {
                var alunoDB = new AlunoDAO();
                alunoDB.DeletarAlunoDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar aluno: Erro = {ex.Message}");
            }
        }
    }
}