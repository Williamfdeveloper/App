namespace App.Domain.Entities.TransacaoPedido
{
    public class ItemCarrinho
    {
        public int codigoProduto { get; set; }
        public int quantidade { get; set; }
        public Pedido pedido { get; set; }
    }
}
