using Dapper;
using ProjetoMVC_LocJogos.Entities;
using ProjetoMVC_LocJogos.Interfaces;
using ProjetoMVC_LocJogos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC_LocJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        //atributo (campo) privado para armazenar a connectionstring
        //readonly -> somente leitura (valor não poderá ser modificado)
        private readonly string _connectionstring;

        //método construtor da classe, faz com que seja obrigatorio passarmos
        //para a classe o valor da connectionstring
        public JogoRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }


        public void Inserir(Jogo jogo)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute("SP_INSERIRJOGO",
                    new
                    {
                        @NOME = jogo.Nome,
                        @PRECO = jogo.Preco,
                        @QUANTIDADE = jogo.Quantidade
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Alterar(Jogo jogo)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute("SP_ALTERARJOGO",
                    new
                    {
                        @IDJOGO = jogo.IdJogo,
                        @NOME = jogo.Nome,
                        @PRECO = jogo.Preco,
                        @QUANTIDADE = jogo.Quantidade
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Excluir(Jogo jogo)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute("SP_EXCLUIRJOGO",
                    new
                    {
                        @IDJOGO = jogo.IdJogo
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public List<Jogo> Consultar()
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                        .Query<Jogo>("SP_CONSULTARJOGOS",
                        commandType: CommandType.StoredProcedure)
                        .ToList(); //retornar muitos registros
            }
        }

        public Jogo ObterPorId(Guid idJogo)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                        .Query<Jogo>("SP_OBTERJOGOPORID",
                        new
                        {
                            @IDJOGO = idJogo
                        },
                        commandType: CommandType.StoredProcedure)
                        .FirstOrDefault(); //retornar 1 unico registro
            }
        }

        public List<Jogo> ConsultarPorDatas(DateTime dataMin, DateTime dataMax)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                        .Query<Jogo>("SP_CONSULTARJOGOSPORDATAS",
                        new
                        {
                            @DataMin = dataMin,
                            @DataMax = dataMax
                        },
                        commandType: CommandType.StoredProcedure)
                        .ToList();
            }
        }

        public List<JogoGraficoModel> ConsultarTotal()
        {
           using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                        .Query<JogoGraficoModel>("SP_CONSULTARTOTALJOGOS",
                        commandType: CommandType.StoredProcedure)
                        .ToList();

            }
        }
    }
}

