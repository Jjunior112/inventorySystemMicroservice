namespace Contracts.Events
{
    public class OperationEvent
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public OperationEvent(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;

        }
    }
}