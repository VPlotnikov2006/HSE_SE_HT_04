using OrdersService.Domain.Products;

namespace OrdersService.Application.DTOs.GetOrder;

public record OrderItemDto(
    string Name,
    decimal PriceOnOrder,
    uint Quantity,
    float Weight,
    ProductCategory Category
);
