using AutoMapper;
using FrontEnd.Models;
using Manager.API.Ultilities;
using Manager.API.ViewModels;
using Manager.Core.Exceptions;
using Manager.Services.DTO;
using Manager.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {

        private readonly string Baseurl = "https://localhost:5001/";

        private HttpClient Initialize()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            HttpResponseMessage Res = await Initialize().GetAsync("api/v1/produto/GetAll");

            if (Res.IsSuccessStatusCode)
            {

                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                var ObjResposta = JsonConvert.DeserializeObject<Response>(EmpResponse);
                ViewBag.Status = new Response(ObjResposta.message, ObjResposta.success);
                List<Produto> AllProdutos = new List<Produto>();
                foreach (var obj in ObjResposta.data)
                {
                    Produto produto = new Produto(obj.id, obj.nome_produto, obj.valor, obj.data_vencimento, obj.color);
                    AllProdutos.Add(produto);
                }

                return View(AllProdutos);
            }
            return View();
        }




        public IActionResult Cadastrar()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Cadastrar([FromForm] CreateViewModel createView)
        {
            Produto p = new Produto
            {
                nome_produto = createView.Nome_produto,
                valor = (double)createView.Valor,
                data_vencimento = createView.Data_vencimento
            };

            HttpResponseMessage Res = await Initialize().PostAsJsonAsync($"api/v1/produto/create", p);
            if (Res.IsSuccessStatusCode)
            {
                var ProdReponse = Res.Content.ReadAsStringAsync().Result;
                var ObjResposta = JsonConvert.DeserializeObject<ResultViewModel>(ProdReponse);
                ViewBag.Status = new Response(ObjResposta.Message, ObjResposta.Success);
                TempData["MSG_S"] = "Registro salvo com sucesso!";
                return new RedirectResult(nameof(Index));
            }
            TempData["MSG_D"] = "Houve um problema!";
            return View();
        }

        public async Task<IActionResult> Atualizar(int id)
        {
            HttpResponseMessage getId = await Initialize().GetAsync($"api/v1/produto/Get/{id}");

            if (getId.IsSuccessStatusCode)
            {
                var EmpResponse = getId.Content.ReadAsStringAsync().Result;

                var ObjResposta = JsonConvert.DeserializeObject<ResultViewModel>(EmpResponse);
                ViewBag.Status = new Response(ObjResposta.Message, ObjResposta.Success);
                var objProduto = JsonConvert.SerializeObject(ObjResposta.Data);
                var produto = JsonConvert.DeserializeObject<UpdateViewModel>(objProduto);

                return View(produto);
            }

            TempData["MSG_D"] = "Houve um problema!";
            return View();
        }


        public async Task<IActionResult> Update([FromForm] UpdateViewModel produto)
        {

            HttpResponseMessage getId = await Initialize().PutAsJsonAsync($"api/v1/produto/update", produto);
           
            if (getId.IsSuccessStatusCode)
            {
                var ProdReponse = getId.Content.ReadAsStringAsync().Result;
                var ObjResposta = JsonConvert.DeserializeObject<ResultViewModel>(ProdReponse);
                ViewBag.Status = new Response(ObjResposta.Message, ObjResposta.Success);
                TempData["MSG_S"] = "Registro Alterado com sucesso!";
                return new RedirectResult(nameof(Index));
            }
            TempData["MSG_D"] = "Houve um problema!";
            return new RedirectResult(nameof(Atualizar));
        }





        //TODO
        public async Task<IActionResult> Detalhes(int id)
        {
            HttpResponseMessage getId = await Initialize().GetAsync($"api/v1/produto/Get/{id}");

            if (getId.IsSuccessStatusCode)
            {
                var EmpResponse = getId.Content.ReadAsStringAsync().Result;
                var ObjResposta = JsonConvert.DeserializeObject<ResultViewModel>(EmpResponse);
                ViewBag.Status = new Response(ObjResposta.Message, ObjResposta.Success);
                var objProduto = JsonConvert.SerializeObject(ObjResposta.Data);
                var produto = JsonConvert.DeserializeObject<Produto>(objProduto);

                return View(produto);
            }

            TempData["MSG_D"] = "Houve um problema!";
            return new RedirectResult(nameof(Index));
        }

        public async Task<bool> AdicionarDesconto(int id, double desconto)
        {
            HttpResponseMessage getId = await Initialize().PostAsJsonAsync($"api/v1/produto/AdiconarDesconto{id}", desconto);

            if (getId.IsSuccessStatusCode)
            {
                var EmpResponse = getId.Content.ReadAsStringAsync().Result;
                var ObjResposta = JsonConvert.DeserializeObject<ResultViewModel>(EmpResponse);
                ViewBag.Status = new Response(ObjResposta.Message, ObjResposta.Success);
                var objProduto = JsonConvert.SerializeObject(ObjResposta.Data);
                var produto = JsonConvert.DeserializeObject<Produto>(objProduto);

                return true ;
            }

            TempData["MSG_D"] = "Houve um problema!";
            return false;

        }




        public async Task<IActionResult> ExcluirAsync(int id)
        {
            HttpResponseMessage Res = await Initialize().DeleteAsync($"api/v1/produto/Remove/{id}");
            if (Res.IsSuccessStatusCode)
            {
                TempData["MSG_S"] = "Registro removido com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            TempData["MSG_D"] = "Houve um problema!";

            return RedirectToAction(nameof(Index));

        }

    }



}


