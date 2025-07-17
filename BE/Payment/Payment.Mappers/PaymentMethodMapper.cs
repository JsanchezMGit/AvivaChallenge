using Payment.Enterprice.Enums;

namespace Payment.Mappers;

public static class PaymentMethodMapper
{
    private static readonly Dictionary<OrderProvider, Dictionary<OrderPaymentMethod, string>> ProviderMethodMappings = 
        new()
        {
            {
                OrderProvider.PagaFacil, new()
                {
                    [OrderPaymentMethod.Cash] = "Cash",
                    [OrderPaymentMethod.CreditCard] = "Card",
                    [OrderPaymentMethod.Transfer] = "Transfer"
                }
            },
            {
                OrderProvider.CazaPagos, new()
                {
                    [OrderPaymentMethod.CreditCard] = "CreditCard",
                    [OrderPaymentMethod.Transfer] = "Transfer"
                }
            }
        };

    public static string ToProviderMethod(OrderProvider provider, OrderPaymentMethod method)
    {
        if (ProviderMethodMappings.TryGetValue(provider, out var providerMethods) &&
            providerMethods.TryGetValue(method, out var mappedMethod))
        {
            return mappedMethod;
        }
        throw new ArgumentException($"El proveedor {provider} no soporta el método de pago {method}");
    }
}