using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVC_LocJogos.Entities;
using ProjetoMVC_LocJogos.Models;
using ProjetoMVC_LocJogos.Reports;
using ProjetoMVC_LocJogos.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC_LocJogos.Controllers// A classe Controller é a camada que chama a Repository, faz a consulta e envia ela para a View
{
    public class JogoController : Controller
    {
        public IActionResult Cadastro() // IActionResult  é o método que abre a página de cadastro

        {
            return View();
        }

        [HttpPost] //abre quando clica no SUBMIT(botão REALIZAR CADASTRO da página), o  [HttpPost] envia pro Controller os dados preenchidos na View
        public IActionResult Cadastro(JogoCadastroModel model,
            [FromServices] JogoRepository jogoRepository) // fazendo isso eu não preciso instaciar com o  var e new
        {
            //verificando se todos os campos da model
            //passaram nas regras de validação..
            if (ModelState.IsValid)
            {
                try
                {
                    //cadastrar no banco de dados..
                    Jogo jogo = new Jogo();
                    jogo.Nome = model.Nome;// eu uso no lugar do Console.ReadLine
                    jogo.Preco = Convert.ToDecimal(model.Preco);//Convert.To eu uso no lugar do Parse
                    jogo.Quantidade = Convert.ToInt32(model.Quantidade);//Convert.To eu uso no lugar do Parse

                    //inserir o produto no banco de dados..
                    jogoRepository.Inserir(jogo);

                    TempData["Mensagem"] = $"Jogo {jogo.Nome}, cadastrado com sucesso.";
                    ModelState.Clear(); //limpa os campos do formulário, para aparecer só a msg cadastrado com sucesso 
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = "Erro ao cadastrar o jogo: " + e.Message;
                }
            }

            return View();
        }

        public IActionResult Consulta([FromServices] JogoRepository jogoRepository)// IActionResult  método que abre a página de consulta
        {
            var model = new JogoConsultaModel();
            try
            {
                //executar a consulta no banco de dados 
                //e armazenar o resultado
                //no atributo 'Jogos' da classe JogoConsultaModel
                model.Jogos = jogoRepository.Consultar();//O método consultar no repository vai no banco, executa a procedure consultarjogos e devolve uma lista de todos os jogos que foram cadastrados no banco
            }
            catch (Exception e)
            {
                //exibir mensagem  na página..
                TempData["Mensagem"] = "Erro ao consultar o jogo: " + e.Message;
            }

            //enviando o objeto 'model' para a página..
            return View(model);
        }
        public IActionResult Exclusao(Guid id,
           [FromServices] JogoRepository jogoRepository)
        {

            try
            {
                //buscar no banco de dados o Jogo atraves do id..
                var jogo = jogoRepository.ObterPorId(id);
                //excluindo o Jogo..
                jogoRepository.Excluir(jogo);

                TempData["Mensagem"] = "Jogo excluído com sucesso.";
            }
            catch (Exception e)
            {
                //exibir mensagem de erro na página..
                TempData["Mensagem"] = "Erro ao excluir o jogo: " + e.Message;
            }

            //redirecionamento do usuário de volta para a página de consulta..
            return RedirectToAction("Consulta");
        }

        public IActionResult Edicao(Guid id,
            [FromServices] JogoRepository jogoRepository)

        {//classe de modelo de dados..
            var model = new JogoEdicaoModel();
            try
            {
                //buscar o jogo no banco de dados atraves do id..
                var jogo = jogoRepository.ObterPorId(id);

                //transferir os dados do jogo para a classe model..
                model.IdJogo = jogo.IdJogo;
                model.Nome = jogo.Nome;
                model.Preco = jogo.Preco;
                model.Quantidade = jogo.Quantidade;
            }
            catch (Exception e)
            {
                //exibir mensagem de erro na página..
                TempData["Mensagem"] = "Erro ao exibir o jogo: " + e.Message;
            }
            //enviando o objeto model para a página..
            return View(model);
        }

        [HttpPost] //recebe o evento SUBMIT do formulário
        public IActionResult Edicao(JogoEdicaoModel model,
            [FromServices] JogoRepository jogoRepository)
        {
            //verifica se todos os campos da model passaram nas regras
            //de validação do formulário (se foram validados com sucesso)
            if (ModelState.IsValid)
            {
                try
                {
                    //buscar o jogo no banco de dados atraves do ID..
                    var jogo = jogoRepository.ObterPorId(model.IdJogo);

                    //alterando os dados do jogo..
                    jogo.Nome = model.Nome;
                    jogo.Preco = Convert.ToDecimal(model.Preco);
                    jogo.Quantidade = Convert.ToInt32(model.Quantidade);

                    //atualizando no banco de dados..
                    jogoRepository.Alterar(jogo);
                    TempData["Mensagem"] = "Jogo atualizado com sucesso.";
                    //redirecionamento de volta para a página de consulta..
                    return RedirectToAction("Consulta");
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = "Erro ao atualizar o jogo: " + e.Message;
                }
            }
            return View();
        }
        public IActionResult Relatorio()
        {
            return View();
        }

        [HttpPost] //recebe os dados enviados pelo formulário
        public IActionResult Relatorio(JogoRelatorioModel model, [FromServices] JogoRepository jogoRepository)
        {
            //verifica se todos os campos da model foram validados com sucesso!
            if (ModelState.IsValid)
            {
                try
                {
                    //capturando as datas informadas na página (formulario)
                    var filtroDataMin = Convert.ToDateTime(model.DataMin);
                    var filtroDataMax = Convert.ToDateTime(model.DataMax);

                    //executando a consulta de jogos no banco de dados..
                    var jogos = jogoRepository.ConsultarPorDatas
                                  (filtroDataMin, filtroDataMax);

                    //gerando o arquivo PDF..
                    var jogoReport = new JogoReport();
                    var pdf = jogoReport.GerarPdf
                              (filtroDataMin, filtroDataMax, jogos);

                    //fazer o download do arquivo..
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.Headers.Add("content-disposition",
                                         "attachment; filename=jogos.pdf");
                    Response.Body.WriteAsync(pdf, 0, pdf.Length);
                    Response.Body.Flush();
                    Response.StatusCode = StatusCodes.Status200OK;
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = "Erro ao gerar relatório: "
                                           + e.Message;
                }

            }

            return View();
        }
        //método que será chamado (executado) por um código JavaScript
        //localizado em alguma página no sistema..
        public JsonResult ObterDadosGrafico([FromServices] JogoRepository jogoRepository)
        {
            try
            {
                //retornar para o javascript, o conteudo 
                //da consulta feita no banco de dados..
                return Json(jogoRepository.ConsultarTotal());
            }

            catch (Exception e)
            {
                //retornando mensagem de erro..
                return Json(e.Message);
            }
        }
    }
}
