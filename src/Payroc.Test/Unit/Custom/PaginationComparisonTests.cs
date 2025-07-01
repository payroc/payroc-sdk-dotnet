using NUnit.Framework;
using Payroc.Core;
using Payroc.Funding.FundingRecipients;
using Payroc.Payments;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;
using WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Types;
using WireMock.Util;

namespace Payroc.Test.Unit.Custom;

[TestFixture]
public class PaginationComparisonTests : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task SingleBlobFundingAccounts()
    {
        const string mockResponse = PaginationComparisonTestsData.ListFundingAccountsResponseSingleBlob;

        Server
            .Given(
                Request.Create()
                    .WithPath("/funding-recipients/1/funding-accounts")
                    .UsingGet()
            )
            .RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingRecipients.ListAccountsAsync(
            new ListFundingRecipientFundingAccountsRequest { RecipientId = 1 }
        );

        var ids = new List<int>();

        foreach (var fundingAccount in response)
        {
            var id = fundingAccount.FundingAccountId;
            ids.Add(id ?? 0);
        }

        Assert.That(ids.First() == 123);
        Assert.That(ids.Last() == 124);
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<IEnumerable<FundingAccount>>(mockResponse))
                .UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task SingleBlobFundingRecipients()
    {
        const string mockResponse = PaginationComparisonTestsData.ListFundingRecipientsResponseSingleBlob;

        Server
            .Given(
                Request.Create()
                    .WithPath("/funding-recipients")
                    .UsingGet()
            )
            .RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var pager = await Client.Funding.FundingRecipients.ListAsync(
            new ListFundingRecipientsRequest { }
        );

        var ids = new List<int>();

        await foreach (var fundingRecipient in pager)
        {
            var id = fundingRecipient.TaxId;
            ids.Add(int.Parse(id[^2..]));
        }

        Assert.That(ids.First() == 1);
        Assert.That(ids.Last() == 34);
        var recipients = JsonDataPropertyReader.DeserializeDataProperty<FundingRecipient>(mockResponse);
        var actualList = new List<FundingRecipient>();

        await foreach (var item in pager)
        {
            actualList.Add(item);
        }

        using var expectedEnumerator = recipients.GetEnumerator();
        using var actualEnumerator = actualList.GetEnumerator();

        while (expectedEnumerator.MoveNext() && actualEnumerator.MoveNext())
        {
            var actualJson = JsonUtils.Serialize(actualEnumerator.Current);
            var expectedJson = JsonUtils.Serialize(expectedEnumerator.Current);
            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }
    }

    [Test]
    public async global::System.Threading.Tasks.Task SingleBlobTestPayments()
    {
        // Data is one big page of 34 objects
        const string mockResponse = PaginationComparisonTestsData.ListPaymentsResponseSingleBlob;
        Server
            .Given(
                Request.Create()
                    .WithPath("/payments")
                    .UsingGet()
            )
            .RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var pager = await Client.Payments.ListAsync(new ListPaymentsRequest());

        var ids = new List<int>();

        foreach (var payment in pager.CurrentPage.Items)
        {
            var id = payment.PaymentId;
            ids.Add(int.Parse(id[^2..]));
        }

        Assert.That(ids.First(), Is.EqualTo(1));
        Assert.That(ids.Last(), Is.EqualTo(34));

        var expected = JsonDataPropertyReader
            .DeserializeDataProperty<Payment>(mockResponse)
            .ToList();

        var actual = pager.CurrentPage.Items.ToList();

        Assert.That(actual.Count, Is.EqualTo(expected.Count));

        for (var i = 0; i < expected.Count; i++)
        {
            var expectedJson = JsonUtils.Serialize(expected[i]);
            var actualJson = JsonUtils.Serialize(actual[i]);
            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }
    }

    [Test]
    public async global::System.Threading.Tasks.Task PaginatedTestPayments()
    {
        // Data is 4 pages of up to 10 records each, ids 1-34
        // We test that the foreach can cross pages, not just iterate elements in the first response
        int callCount = 0;

        Server
            .Given(
                Request.Create()
                    .WithPath("/payments")
                    .UsingGet()
            )
            .RespondWith(
                Response.Create()
                    .WithCallback(request =>
                    {
                        callCount++;
                        var url = Server.Urls[0];
                        var body = callCount switch
                        {
                            1 => PaginationComparisonTestsData.ListPaymentsResponsePage1.Replace("{Server.Urls[0]}", Server.Urls[0]),
                            2 => PaginationComparisonTestsData.ListPaymentsResponsePage2.Replace("{Server.Urls[0]}", Server.Urls[0]),
                            3 => PaginationComparisonTestsData.ListPaymentsResponsePage3.Replace("{Server.Urls[0]}", Server.Urls[0]),
                            4 => PaginationComparisonTestsData.ListPaymentsResponsePage4.Replace("{Server.Urls[0]}", Server.Urls[0]),
                            _ => "{}"
                        };

                        return new ResponseMessage
                        {
                            StatusCode = 200,
                            BodyData = new BodyData
                            {
                                BodyAsString = body,
                                DetectedBodyType = BodyType.String
                            },
                            Headers = new Dictionary<string, WireMockList<string>>
                            {
                                { "Content-Type", new WireMockList<string>("application/json") }
                            }
                        };
                    })
            );

        var pager = await Client.Payments.ListAsync(new ListPaymentsRequest());

        var ids = new List<int>();
        var actual = new List<Payment>();

        await foreach (var payment in pager)
        {
            var id = payment.PaymentId;
            var idInt = int.Parse(id[^2..]);

            if (idInt == 10 || idInt == 20 || idInt == 30)
            {
                int paginationBreaksWhenAccessingSecondPage = 1;
            }

            ids.Add(idInt);
            actual.Add(payment);
        }

        Assert.That(ids.First(), Is.EqualTo(1));
        Assert.That(ids.Last(), Is.EqualTo(34));

        var expected = new List<Payment>();
        expected.AddRange(JsonDataPropertyReader.DeserializeDataProperty<Payment>(PaginationComparisonTestsData.ListPaymentsResponsePage1));
        expected.AddRange(JsonDataPropertyReader.DeserializeDataProperty<Payment>(PaginationComparisonTestsData.ListPaymentsResponsePage2));
        expected.AddRange(JsonDataPropertyReader.DeserializeDataProperty<Payment>(PaginationComparisonTestsData.ListPaymentsResponsePage3));
        expected.AddRange(JsonDataPropertyReader.DeserializeDataProperty<Payment>(PaginationComparisonTestsData.ListPaymentsResponsePage4));

        Assert.That(actual.Count, Is.EqualTo(expected.Count));

        for (var i = 0; i < expected.Count; i++)
        {
            var expectedJson = JsonUtils.Serialize(expected[i]);
            var actualJson = JsonUtils.Serialize(actual[i]);

            Assert.That(actualJson, Is.EqualTo(expectedJson), $"Mismatch at index {i}");
        }
    }
}
