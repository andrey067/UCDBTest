using AutoMapper;
using FrontEnd.Models;
using Manager.API.Ultilities;
using Manager.Core.Exceptions;
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
                List<Produto> AllProdutos = new List<Produto>();
                ViewBag.Status = new Response(ObjResposta.message, ObjResposta.success);
                foreach (var obj in ObjResposta.data)
                {
                    Produto produto = new Produto(obj.id, obj.nome_produto, obj.valor, obj.data_vencimento, obj.color);
                    AllProdutos.Add(produto);
                }



                return View(AllProdutos);



            }

            return View();
        }
    }

}

