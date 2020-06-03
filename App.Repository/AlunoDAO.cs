using App.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace App.Repository
{
    public class AlunoDAO
    {

        //string STR_CONEXAO = ConfigurationManager.AppSettings["ConnectionString"];
        private string STR_CONEXAO = ConfigurationManager.ConnectionStrings["ConnectionDev"].ConnectionString;
        private IDbConnection conexao;

        public AlunoDAO()
        {
            conexao = new SqlConnection(STR_CONEXAO);
            conexao.Open();
        }



        public List<AlunoDTO> ListarAluno(int? id)
        {

            try
            {
                var listaAluno = new List<AlunoDTO>();
                string sql;

                IDbCommand selectCmd = conexao.CreateCommand();

                sql = "SELECT * FROM ALUNOS";
                if (id != null)
                {
                    sql += $" WHERE ID = {id}";
                }

                selectCmd.CommandText = sql;

                IDataReader resultado = selectCmd.ExecuteReader();

                while (resultado.Read())
                {
                    var alu = new AlunoDTO
                    {
                        id = Convert.ToInt32(resultado["Id"]),
                        nome = Convert.ToString(resultado["nome"]),
                        sobrenome = Convert.ToString(resultado["sobrenome"]),
                        telefone = Convert.ToString(resultado["telefone"]),
                        ra = Convert.ToInt32(resultado["ra"]),
                        data = Convert.ToString(resultado["data"]),
                    };

                    listaAluno.Add(alu);
                }

                return listaAluno;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();

            }
        }

        public void InserirAlunoDB(AlunoDTO aluno)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "INSERT INTO ALUNOS (nome, sobrenome, telefone, ra, data) VALUES (@nome, @sobrenome, @telefone, @ra, @data)";

                IDbDataParameter pNome = new SqlParameter("nome", aluno.nome);
                insertCmd.Parameters.Add(pNome);

                IDbDataParameter pSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
                insertCmd.Parameters.Add(pSobrenome);

                IDbDataParameter pTelefone = new SqlParameter("telefone", aluno.telefone);
                insertCmd.Parameters.Add(pTelefone);

                IDbDataParameter pRa = new SqlParameter("ra", aluno.ra);
                insertCmd.Parameters.Add(pRa);

                if (aluno.data is null)
                {
                    aluno.data = Convert.ToString(DateTime.Now.ToString("yyyy-MM"));
                }

                IDbDataParameter pData = new SqlParameter("data", aluno.data);
                insertCmd.Parameters.Add(pData);

                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();

            }


        }


        public void AtualizarAlunoDB(AlunoDTO aluno)
        {

            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "UPDATE ALUNOS SET nome = @nome, sobrenome = @sobrenome, telefone = @telefone, ra = @ra WHERE id = @id";

                IDbDataParameter pId = new SqlParameter("id", aluno.id);
                updateCmd.Parameters.Add(pId);


                IDbDataParameter pNome = new SqlParameter("nome", aluno.nome);
                updateCmd.Parameters.Add(pNome);

                IDbDataParameter pSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
                updateCmd.Parameters.Add(pSobrenome);

                IDbDataParameter pTelefone = new SqlParameter("telefone", aluno.telefone);
                updateCmd.Parameters.Add(pTelefone);

                IDbDataParameter pRa = new SqlParameter("ra", aluno.ra);
                updateCmd.Parameters.Add(pRa);

                if (aluno.data is null)
                {
                    aluno.data = Convert.ToString(DateTime.Now.ToString("yyyy-MM"));
                }

                IDbDataParameter pData = new SqlParameter("data", aluno.data);
                updateCmd.Parameters.Add(pData);

                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();

            }


        }

        public void DeletarAlunoDB(int id)
        {
            try
            {
                IDbCommand deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "DELETE FROM ALUNOS WHERE id = @id";

                IDbDataParameter pId = new SqlParameter("id", id);
                deleteCmd.Parameters.Add(pId);

                deleteCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();

            }

        }
    }

}