using AutoMapper;
using Manager.API.Ultilities;
using Manager.API.ViewModels;
using Manager.Core.Exceptions;
using Manager.Services.DTO;
using Manager.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.API.Controllers
{
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/v1/produto/create")]
        public async Task<ActionResult> Create([FromBody] CreateViewModel createViewModel)
        {
            try
            {
                var produtoDTO = _mapper.Map<ProdutoDTO>(createViewModel);

                var produtoCreate = await _produtoService.Create(produtoDTO);


                return Ok(new ResultViewModel
                {
                    Message = "Produto criado com sucesso",
                    Success = true,
                    Data = produtoCreate
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }

        }


    }
}
