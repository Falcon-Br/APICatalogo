using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get() //Coleção de objetos do tipo produto
    {
        var produtos = _context.Produtos.ToList();
        if (produtos is null)
        {
            return NotFound("Nenhum Produto encontrado.");
        }
        return produtos;
    }

    [HttpGet("{id:int}", Name = "ObterProduto")] // o ":int" restringe o tipo que será recebido
    public ActionResult<Produto>Get(int id) 
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
      
        if (produto is null)
        {
            return NotFound("Produto não encontrado.");
        }

        return produto;
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        _context.Produtos.Add(produto); // inclui o produto no contexto
        _context.SaveChanges(); // persiste o produto no banco de dados

        // Retorna o código 201 e aciona a rota que tiver "ObterProduto"
        return new CreatedAtRouteResult("ObterProduto", 
            new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")] // o ":int" restringe o tipo que será recebido
    public ActionResult Put(int id, Produto produto)
    {
        if(id != produto.ProdutoId)
        {
            return BadRequest();
        }

        _context.Entry(produto).State = EntityState.Modified; //Informa ao contexto que a entidade Produto está em um estado modificado e precisa ser alterado
        _context.SaveChanges(); // Persiste no Banco de dados

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id) 
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        //var produto = _context.Produtos.Find(id); // O find tenta localizar primeiramente na memoria, mas o id tem que ser a chave primária 
        if (produto is null)
            return NotFound("Produto não localizado.");

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);
    }
}
