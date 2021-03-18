using AutoMapper;
using Manager.Domain.Entities;
using Manager.Infra.Interface;
using Manager.Services.DTO;
using Manager.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Services.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IMapper _mapper;

        private readonly IProdutoRepository _produtoRepository;


        public ProdutoService(IMapper mapper, IProdutoRepository produtoRepository)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        //Create
        public async Task<ProdutoDTO> Create(ProdutoDTO produtoDTO)
        {
            var produtoExist = await _produtoRepository.GetByNome_produto(produtoDTO.Nome_produto);
            if (produtoExist != null)
            {
                throw new Exception("Já exixte um produto cadastrado com esse nome");
            }

            var produto = _mapper.Map<Produto>(produtoDTO);
            produto.Validate();


            var produtoCreate = await _produtoRepository.Create(produto);

            return _mapper.Map<ProdutoDTO>(produtoDTO);
        }

        //Update
        public async Task<ProdutoDTO> Update(ProdutoDTO produtoDTO)
        {
            var produtoExist = await _produtoRepository.Get(produtoDTO.Id);

            if (produtoExist == null)
            {
                throw new Exception("Produto não existe");

            }
            var produto = _mapper.Map<Produto>(produtoDTO);
            produto.Validate();
            produto.ChageName_Produto(produtoDTO.Nome_produto);

            var produtoUpdate = await _produtoRepository.Update(produto);

            return _mapper.Map<ProdutoDTO>(produtoUpdate);

        }


        //GET
        public async Task<ProdutoDTO> Get(long id)
        {
            var produto = await _produtoRepository.Get(id);

            return _mapper.Map<ProdutoDTO>(produto);


        }

        //GET-ALL 
        public async Task<List<ProdutoDTO>> GetAll()
        {
            var AllProduto = await _produtoRepository.GetAll();

            return _mapper.Map<List<ProdutoDTO>>(AllProduto);
        }


        //Remove
        public async Task Remove(long id)
        {
            await _produtoRepository.Remove(id);
        }


    }
}
