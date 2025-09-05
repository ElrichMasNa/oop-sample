using ACME.OOP.SCM.Domain.Model.Aggregates;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;

public class PurchaseOrder(string orderNumber, Supplier supplierID, 
    DateTime orderDate, string currency)
{
        private List<PurchaseOrderItem> _items = new();
        public Supplier Supplier { get; } = supplierID ?? throw new ArgumentNullException(nameof(supplierID), "supplierID cannot be null");
        public OrderNumber OrderNumber { get; } = orderNumber ?? throw new ArgumentNullException(nameof(orderNumber), "orderNumber cannot be null");
        public DateTime OrderDate { get; } = orderDate;
        public string Currency { get; } = string.IsNullOrWhiteSpace(currency) || currency.Length! = 3 ? throw new ArgumentNullException(nameof(currency)) : currency;

        public IReadOnlyList<PurchaseOrderItem> Items => _items.AsReadOnly();
}