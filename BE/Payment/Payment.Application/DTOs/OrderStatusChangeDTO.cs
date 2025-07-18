namespace Payment.Application.DTOs;

public class OrderStatusChangeDTO
{
    public required string OrderId { get; set; }
    public required string Status { get; set; }
}
