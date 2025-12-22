namespace OrdersService.Domain.Orders;

public enum OrderStatus
{
    /// <summary>
    /// New order, before Payment Service response
    /// </summary>
    New,

    /// <summary>
    /// If account has no money
    /// </summary>
    Cancelled,

    /// <summary>
    /// If payment was successful
    /// </summary>
    Finished
}
