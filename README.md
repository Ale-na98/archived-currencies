# Archived Exchange Rates (under development)

Based on Azure Static Web App, this service allows to find out the exchange rates of currencies in relation to Polish Zloty (PLN), which are not available on the [official currency calculator](https://www.podatki.gov.pl/kalkulatory-podatkowe/kalkulator-walut/).

However, the exchange rates of such currencies can be found on the [Narodowy Bank Polski (NBP)](https://nbp.pl/statystyka-i-sprawozdawczosc/kursy/archiwum-kursow-srednich-tabela-b/) website in the section with archived exchange rates.

**Attention!** For these currencies, NBP publishes the average exchange rate not per day (as, for example, for the dollar or euro), but per week (4-5 rates per month).

## Disclaimer
> Archived Exchange Rates service is not registred investment, legal or tax advisor or a broker/dealer. All rate options were taken from the [Narodowy Bank Polski, Table B](https://nbp.pl/statystyka-i-sprawozdawczosc/kursy/archiwum-kursow-srednich-tabela-b/) and aggregated for personal purposes for a faster calculating the tax on foreign income. Although best efforts are made to ensure that all rates are accurate and up to date, occasionally unintended errors and misprints may occur.

Link to the service: [https://orange-ocean-0a7dad203.3.azurestaticapps.net](https://orange-ocean-0a7dad203.3.azurestaticapps.net).

## Current status

- Only 4 currencies are available in the selector (data is taken from test database).
- During the development, the official public Web API of NBP was discovered. Therefore, in the near future, the application API and database will be replaced by requests to the official API directly from the client.
