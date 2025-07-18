export const formatCurrency = (amount: number, locale = 'es-MX', currency = 'MXN') => {
  return new Intl.NumberFormat(locale, {
    style: 'currency',
    currency: currency,
  }).format(amount);
};