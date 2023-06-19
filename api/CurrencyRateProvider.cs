using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;

namespace CurrencyRateProvider;
public static class CurrencyRateProvider
{
    [FunctionName("CurrencyRateProvider")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string isoCode = req.Query["isoCode"];
        DateOnly transactionDate;

        log.LogInformation($"isoCode: {isoCode}");

        var isDateCorrect = DateOnly.TryParse(req.Query["transactionDate"], out transactionDate);

        log.LogInformation($"isDateCorrect: {isDateCorrect}, transactionDate: {transactionDate}");

        if (isoCode.Length != 3 || !isDateCorrect)
        {
            log.LogInformation($"isoCode: {isoCode.Length != 3}, isDateCorrect: {isDateCorrect}");
            return new BadRequestObjectResult("Request params are not valid.");
        }

        var dataSet = FetchCurrencyRateForDate(isoCode, transactionDate);

        if (dataSet.Tables[0].Rows.Count == 0)
        {
            return new OkObjectResult("{}");
        }

        var row = dataSet.Tables[0].Rows[0];
        var currency = MapToCurrency(row);

        var responseMessage = MapToJson(currency);

        return new OkObjectResult(responseMessage);
    }

    public static DataSet FetchCurrencyRateForDate(string isoCode, DateOnly transactionDate)
    {
        var dataSet = new DataSet();

        string connectionString = System.Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
        using var connection = new NpgsqlConnection(connectionString);

        connection.Open();

        var query = @"select * from currencies
                        inner join nbp_table_number ON currencies.rate_date = nbp_table_number.rate_date 
                        where iso_code = @IsoCode 
                        and currencies.rate_date < @TransactionDate 
                        order by currencies.rate_date asc 
                        limit 1";

        using var command = new NpgsqlCommand(query, connection);

        command.CommandType = CommandType.Text;
        command.Parameters.Add("@IsoCode", NpgsqlDbType.Char, 3).Value = isoCode;
        command.Parameters.Add("@TransactionDate", NpgsqlDbType.Date).Value = transactionDate;
        command.Prepare();

        var adapter = new NpgsqlDataAdapter();
        adapter.SelectCommand = command;
        adapter.Fill(dataSet);

        return dataSet;
    }

    public static Currency MapToCurrency(DataRow row)
    {
        var currency = new Currency();

        currency.Quantity = (Int16)row["quantity"];

        var rateDate = ((DateTime)row["rate_date"]);
        currency.RateDate = rateDate.Date.ToString("dd.MM.yyyy");

        currency.Rate = (decimal)row["rate"];

        var tableNumber = ((string)row["table_number"]).Replace('/', '-').ToLower();
        currency.Link = $"https://nbp.pl/archiwum-kursow/tabela-nr-{tableNumber}-z-dnia-{rateDate.Date.ToString("yyyy-MM-dd")}/";

        return currency;
    }

    public static string MapToJson(Currency currency) => JsonConvert.SerializeObject(currency);
}
