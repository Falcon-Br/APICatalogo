using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Categorias")]
public class Categoria
{
    // Quando é definido uma coleção em uma classe, é uma boa prática inicializar a coleção
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }

    [Key]
    public int CategoriaId { get; set; }
    
    [Required]
    [StringLength(80)] //Tamanho em Bytes
    public string? Nome { get; set; }

    [Required]
    [StringLength (300)]
    public string? ImagemURL { get; set; }

    //Relaciona com tabela produto, uma categoria tem vários produtos
    public ICollection<Produto>? Produtos { get; set; }
}
