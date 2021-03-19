using AutoMapper;
using FrontEnd.Models;
using Manager.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public HomeController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }


        [Route("/api/v1/produto/GetAll")]
        public async Task<IActionResult> Index()
        {
            return View(await _produtoService.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
