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
        public async Task<ActionResult> Cadastrar([FromForm]CreateViewModel createView)
        {
            HttpResponseMessage Res = await Initialize().PostAsJsonAsync($"api/v1/produto/create", createView);
            if (Res.IsSuccessStatusCode)
            {
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                var ObjResposta = JsonConvert.DeserializeObject<Response>(EmpResponse);
                ViewBag.Status = new Response(ObjResposta.message, ObjResposta.success);
                TempData["MSG_S"] = "Registro salvo com sucesso!";
                return View();
            }
            TempData["MSG_D"] = "Houve um problema!";
            return View();
        }



        public async Task<IActionResult> Atualizar(int id)
        {

            HttpResponseMessage getId = await Initialize().GetAsync($"api/v1/produto/Get/{id}");
            if (getId.IsSuccessStatusCode)
            {
                var ProdReponse = getId.Content.ReadAsStringAsync().Result;
                var ObjResposta = JsonConvert.DeserializeObject<Response>(ProdReponse);


                //foreach (var obj in ObjResposta.data)
                //{
                //    if (obj.id == id)
                //    {
                //        HttpResponseMessage Res = await Initialize().PutAsJsonAsync($"api/v1/produto/update", obj);
                //        if (Res.IsSuccessStatusCode)
                //        {
                //            return View();
                //        }
                //        //TODO - page error
                //        return View();
                //    }

                //}
            }
            //TODO - obj não encontrado
            return View();
        }


        //TODO
        public async Task<IActionResult> Detalhes(int id)
        {
            HttpResponseMessage getId = await Initialize().GetAsync($"api/v1/produto/Get/{id}");

            if (getId.IsSuccessStatusCode)
            {
                var EmpResponse = getId.Content.ReadAsStreamAsync().Result;
                //var teste = EmpResponse

                //var teste = JsonConvert.DeserializeObject<Produto>(EmpResponse);

                //var ObjResposta = JsonConvert.DeserializeObject<Response>(EmpResponse);


                //Produto p; 
                //foreach (var obj in ObjResposta.data)
                //{
                //    p.nome_produto = obj.nome_produto;
                //    p.valor = obj.valor;
                //    p.data_vencimento = obj.data_vencimento;
                //}

                return View();
            }


            return View();
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


