using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.Aggregates;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;




namespace ACME.OOP.Procurement.Domain.Model.Aggregates;
/// <summary>
/// Represents a Purchase Order aggregate root in the Procurement bounded context.
/// </summary>
/// <param name="orderNumber">The unique identifier for the purchase order.</param>
/// <param name="supplierID">The <see cref="SupplierID"/> Identifying the supplier.</param>
/// <param name="orderDate">The date the order was placed.</param>
/// <param name="currency">The currency code (ISO 4217) for the order.</param>
public class PurchaseOrder(string orderNumber, SupplierId supplierID, 
    DateTime orderDate, string currency)
{
        private List<PurchaseOrderItem> _items = new();
        
        public string OrderNumber { get; } = orderNumber ?? throw new ArgumentNullException(nameof(orderNumber));
        public SupplierId SupplierID { get; } = supplierID ?? throw new ArgumentNullException(nameof(supplierID));
        public DateTime OrderDate { get; } = orderDate;
        public string Currency { get; } = string.IsNullOrWhiteSpace(currency) || currency.Length != 3 ? throw new ArgumentNullException(nameof(currency)) : currency;

        public IReadOnlyList<PurchaseOrderItem> Items => _items.AsReadOnly();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <param name="unitPriceAmount"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void AddItem(ProductId productId, int quantity, decimal unitPriceAmount)
        {
            ArgumentNullException.ThrowIfNull(productId);
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
            if (unitPriceAmount <= 0) throw new ArgumentOutOfRangeException(nameof(unitPriceAmount), "Unit price must be greater than zero.");
            var unitPrice = new Money(unitPriceAmount, Currency);
            var item = new PurchaseOrderItem(productId, quantity, unitPrice);
            _items.Add(item);
        }
        /// <summary>
        /// Calculates the total amount of the purchase order by summing up the total of each item.
        /// 
        public Money CalculateOrderTotal()
        {
         var totalAmount = _items.Sum(item => item.CalculateItemTotal().Amount);
         return new Money(totalAmount, Currency);
        }
}