using AutoMapper;
using FrontEnd.Models;
using Manager.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly string Baseurl = "https://localhost:44372/";

        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }
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
        public async Task<IActionResult> Index()
        {
            List<CreateViewModel> EquipInfo = new List<CreateViewModel>();

            HttpResponseMessage Res = await Initialize().GetAsync("/api/v1/produto/GetAll");

            if (Res.IsSuccessStatusCode)
            {
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                EquipInfo = JsonConvert.DeserializeObject<List<CreateViewModel>>(EmpResponse);
            }

            return View(EquipInfo);
        }
}
