using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interface;
using Manager.Services.DTO;
using Manager.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //TODO
            var produtoExist = await _produtoRepository.GetByNome(produtoDTO.Nome_produto);
            if (produtoExist != null)
            {
                throw new DomainException("Já exixte um produto cadastrado com esse nome");
            }

            var produto = _mapper.Map<Produto>(produtoDTO);
            produto.Validate();


            var produtoCreate = await _produtoRepository.Create(produto);

            return _mapper.Map<ProdutoDTO>(produtoCreate);
        }

        //Update
        public async Task<ProdutoDTO> Update(ProdutoDTO produtoDTO)
        {
            var produtoExist = await _produtoRepository.Get(produtoDTO.Id);

            if (produtoExist == null)
            {
                throw new DomainException("Produto não existe");

            }
            var produto = _mapper.Map<Produto>(produtoDTO);
            produto.Validate();
            produto.ChageName_Produto(produtoDTO.Nome_produto);
            produto.ChangeData_vencimento(produtoDTO.Data_vencimento);
            produto.ChangeValor(produtoDTO.Valor);
            var produtoUpdate = await _produtoRepository.Update(produto);

            return _mapper.Map<ProdutoDTO>(produtoUpdate);

        }

        //Adicionar desconto
        public async Task<ProdutoDTO> AdicionarDesconto(long id,decimal porcentagem)
        {
            var produtoExist = await _produtoRepository.Get(id);

            if (produtoExist == null)
            {
                throw new DomainException("Produto não existe");

            }

            //Validação data_vencimento
            if (produtoExist.Data_vencimento <= DateTime.Now)
            {
                throw new DomainException("Produto não pode ser alterado fora da data de validade");
            }
            var resultado = produtoExist.Valor - (produtoExist.Valor * porcentagem) / 100;

            var produto = _mapper.Map<Produto>(produtoExist);
            produto.ChangeValor(resultado);
            produto.Validate();
            var produtoUpdate = await _produtoRepository.Update(produto);

            return _mapper.Map<ProdutoDTO>(produtoUpdate);

        }

        //Busca todas as datas e atualiza a cor
        public async Task<List<ProdutoDTO>> GetAllDate()
        {
            var produtoExist = await _produtoRepository.GetAll();



            if (produtoExist == null)
            {
                throw new DomainException("Não há produtos cadastrados");
            }

            var produto = _mapper.Map<List<Produto>>(produtoExist);

            foreach (var obj in produto)
            {
                if (obj.Data_vencimento.Date.Ticks >= DateTime.Now.Date.Ticks)
                {
                    obj.ChangeColor("F46D69");
                    var produtoUpdate = await _produtoRepository.Update(obj);
                }
                if (obj.Data_vencimento.Date.Ticks >= DateTime.Now.AddDays(3).Date.Ticks)
                {
                    obj.ChangeColor("E5D33F");
                    var produtoUpdate = await _produtoRepository.Update(obj);

                }
                if (obj.Data_vencimento.Date.Ticks > DateTime.Now.AddDays(3).Date.Ticks)
                {
                    obj.ChangeColor("AFF05D");
                    var produtoUpdate = await _produtoRepository.Update(obj);

                }

            }




            return _mapper.Map<List<ProdutoDTO>>(produto);
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

            if (AllProduto == null)
            {
                throw new DomainException("Não há produtos cadastrados");
            }

            var produto = _mapper.Map<List<Produto>>(AllProduto);

            foreach (var obj in produto)
            {
                if (obj.Data_vencimento.Date.Ticks >= DateTime.Now.Date.Ticks)
                {
                    obj.ChangeColor("ed");
                    var produtoUpdate = await _produtoRepository.Update(obj);
                }
                if (obj.Data_vencimento.Date.Ticks >= DateTime.Now.AddDays(3).Date.Ticks)
                {
                    obj.ChangeColor("yellow");
                    var produtoUpdate = await _produtoRepository.Update(obj);

                }
                if (obj.Data_vencimento.Date.Ticks > DateTime.Now.AddDays(3).Date.Ticks)
                {
                    obj.ChangeColor("green");
                    var produtoUpdate = await _produtoRepository.Update(obj);

                }

            }
            return _mapper.Map<List<ProdutoDTO>>(AllProduto);
        }

        //Remove
        public async Task Remove(long id)
        {
            await _produtoRepository.Remove(id);
        }

        //Busca por Nome
        public async Task<List<ProdutoDTO>> SearchByNome(string nome_produto)
        {
            var produtoNome = await _produtoRepository.SearchByNome(nome_produto);
            return _mapper.Map<List<ProdutoDTO>>(produtoNome);

        }
        //Busca por valor
        public async Task<List<ProdutoDTO>> SearchByValor(decimal valor)
        {
            var produtoValor = await _produtoRepository.SearchByValor(valor);

            return _mapper.Map<List<ProdutoDTO>>(produtoValor);
        }


        //Busca Por data
        public async Task<List<ProdutoDTO>> SearchByData_vencimento(DateTime data_vencimento)
        {
            var allDatas = await _produtoRepository.SearchByData_vencimento(data_vencimento);


            return _mapper.Map<List<ProdutoDTO>>(allDatas);


        }


    }
}
