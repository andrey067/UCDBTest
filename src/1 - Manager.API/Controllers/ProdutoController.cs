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


        [HttpGet]
        [Route("/api/v1/produto/get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var produtoObj = await _produtoService.Get(id);

                if (produtoObj == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum produto foi encontrado com o ID informado.",
                        Success = true,
                        Data = produtoObj
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Produto encontrado com sucesso!",
                    Success = true,
                    Data = produtoObj
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/produto/GetByNome_produto")]
        public async Task<IActionResult> GetByNome_produto(string nome_produto)
        {
            try
            {
                var produtoObj = await _produtoService.SearchByNome_Produto(nome_produto);

                if (produtoObj == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum produto foi encontrado com o ID informado.",
                        Success = true,
                        Data = produtoObj
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Produto encontrado com sucesso!",
                    Success = true,
                    Data = produtoObj
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/produto/GetData_vencimento")]
        public async Task<IActionResult> GetData_vencimento(DateTime data_vencimento)
        {
            try
            {
                var produtoObj = await _produtoService.SearchByData_vencimento(data_vencimento);

                if (produtoObj == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum produto foi encontrado com a data informada informado.",
                        Success = true,
                        Data = produtoObj
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Produto encontrado com sucesso!",
                    Success = true,
                    Data = produtoObj
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/produto/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allUsers = await _produtoService.GetAll();

                return Ok(new ResultViewModel
                {
                    Message = "Usuários encontrados com sucesso!",
                    Success = true,
                    Data = allUsers
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
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
        [HttpPut]
        [Route("/api/v1/produto/update")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel updateViewModel)
        {
            try
            {
                var produtoDTO = _mapper.Map<ProdutoDTO>(updateViewModel);

                var userUpdated = await _produtoService.Update(produtoDTO);

                return Ok(new ResultViewModel
                {
                    Message = "O produto foi atualizado com sucesso!",
                    Success = true,
                    Data = userUpdated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete]
        [Route("/api/v1/produto/Remove/{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                await _produtoService.Remove(id);

                return Ok(new ResultViewModel
                {
                    Message = "Usuário removido com sucesso!",
                    Success = true,
                    Data = null
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }





    }
}
