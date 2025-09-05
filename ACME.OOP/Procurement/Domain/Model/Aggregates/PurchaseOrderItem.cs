using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;
/// <summary>
/// Represents an item within a purchase order, including product details, quantity, and pricing.
/// </summary>
/// <param name="productId">The <see cref="ProductId"/>of the product being ordered. Cannot be null./></param>
/// <param name="quantity">The quantity of the product being ordered. Must be greater than zero.</param>
/// <param name="unitPrice">The <see cref="Money"/>representing the unit price of the product. Cannot be </param>

public class PurchaseOrderItem(ProductId productId, int quantity, Money unitPrice)
{
    public ProductId ProductId { get; } = productId ?? throw new ArgumentNullException(nameof(productId), "productId cannot be null");
    public int Quantity { get; } = quantity > 0 ? quantity : throw new ArgumentOutOfRangeException(nameof(quantity), "quantity must be greater than zero");
    public Money UnitPrice { get; } = unitPrice ?? throw new ArgumentNullException(nameof(unitPrice), "unitPrice cannot be null");
    public Money CalculateItemTotal() => new(amount:UnitPrice.Amount * Quantity, UnitPrice.Currency);
}