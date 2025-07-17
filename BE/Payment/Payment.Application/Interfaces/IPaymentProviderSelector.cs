using System;
using Payment.Application.DTOs;
using Payment.Enterprice.Enums;

namespace Payment.Application.Interfaces;

public interface IPaymentProviderSelector
{
    OrderProvider SelectBetsByLowerCost(OrderRequestDTO orderRequest);
}
