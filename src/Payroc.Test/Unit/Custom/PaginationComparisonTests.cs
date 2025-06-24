using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Funding.FundingRecipients;
using Payroc.Payments;
using Payroc.Payments.Refunds;

namespace Payroc.Test.Unit.MockServer;

[TestFixture]
public class PaginationComparisonTests : BaseMockServerTest
{
    public const string MockBaseUrl = "https://localhost:9876";
    public const string MockProxy = "127.0.0.1:9876";
    public const string MockToken = "mock token";

    private const string ListPaymentsResponse = """
        {
        	"limit": 2,
        	"count": 2,
        	"hasMore": true,
        	"data": [
        		{
        			"paymentId": "E29U8OUQ01",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ02",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ03",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ04",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ05",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ06",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ07",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ08",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ09",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ10",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ11",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ12",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ13",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ14",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ15",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ16",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ17",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ18",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ19",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ20",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ21",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ22",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ23",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ24",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ25",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ26",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ27",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ28",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ29",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ30",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ31",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ32",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ33",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		},
        		{
        			"paymentId": "E29U8OUQ34",
        			"processingTerminalId": "1234001",
        			"order": {
        				"orderId": "OrderRef7654",
        				"amount": 4999,
        				"currency": "USD",
        				"dateTime": "2024-07-02T15:30:00Z",
        				"description": "Monthly Premium Club subscription"
        			},
        			"card": {
        				"type": "Visa Debit",
        				"entryMethod": "icc",
        				"cardNumber": "453985******7062",
        				"expiryDate": "1225",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
        					}
        				},
        				"securityChecks": {
        					"cvvResult": "M",
        					"avsResult": "X"
        				}
        			},
        			"transactionResult": {
        				"type": "sale",
        				"status": "ready",
        				"responseCode": "A",
        				"responseMessage": "APPROVAL",
        				"approvalCode": "475318",
        				"authorizedAmount": 1000,
        				"currency": "EUR"
        			},
        			"operator": "Automatic Payment",
        			"supportedOperations": [
        				"fullyReverse",
        				"setAsPending"
        			],
        			"customFields": [
        				{
        					"name": "yourCustomField",
        					"value": "abc123"
        				}
        			]
        		}
        	],
        	"links": [
        		{
        			"rel": "next",
        			"method": "get",
        			"href": "https://api.payroc.com/v1/payments?processingTerminalId=1234001&limit=2&after=E29U8OUQ34"
        		},
        		{
        			"rel": "previous",
        			"method": "get",
        			"href": "https://api.payroc.com/v1/payments?processingTerminalId=1234001&limit=2&beforeE29U8OUQ01"
        		}
        	]
        }
        """;

    [Test]
    public async global::System.Threading.Tasks.Task PaginatedTestPayments()
    {
        const string mockResponse = ListPaymentsResponse;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/payments")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Payments.ListAsync(
            new ListPaymentsRequest()
        );

        var ids = new List<int>();

        foreach (var payment in response.CurrentPage.Items)
        {
            var id = payment.PaymentId;
            ids.Add(int.Parse(id[^2..]));
        }

        Assert.That(ids.First() == 1);
        Assert.That(ids.Last() == 34);
        //Assert.That(
        //    response.CurrentPage.Items,
        //    Is.EqualTo(JsonUtils.Deserialize<IReadOnlyList<Payment>>(mockResponse))
        //        .UsingDefaults()
        //);
    }

    [Test]
    public async global::System.Threading.Tasks.Task PaginatedTestFundingAccounts()
    {
        const string mockResponse = """
            [
              {
                "fundingAccountId": 123,
                "createdDate": "2024-07-02T15:30:00.000Z",
                "lastModifiedDate": "2024-07-02T15:30:00.000Z",
                "status": "approved",
                "type": "checking",
                "use": "credit",
                "nameOnAccount": "Jane Doe",
                "paymentMethods": [
                  {
                    "value": {
                      "routingNumber": "123456789",
                      "accountNumber": "1234567890"
                    },
                    "type": "ach"
                  }
                ],
                "metadata": {
                  "yourCustomField": "abc123"
                },
                "links": [
                  {
                    "rel": "parent",
                    "method": "get",
                    "href": "https://api.payroc.com/v1/funding-recipient/234"
                  }
                ]
              },
              {
                "fundingAccountId": 124,
                "createdDate": "2024-07-02T15:30:00.000Z",
                "lastModifiedDate": "2024-07-02T15:30:00.000Z",
                "status": "pending",
                "type": "checking",
                "use": "debit",
                "nameOnAccount": "Jane Doe",
                "paymentMethods": [
                  {
                    "value": {
                      "routingNumber": "123456789",
                      "accountNumber": "1234567890"
                    },
                    "type": "ach"
                  }
                ],
                "metadata": {
                  "yourCustomField": "abc123"
                },
                "links": [
                  {
                    "rel": "parent",
                    "method": "get",
                    "href": "https://api.payroc.com/v1/funding-recipient/235"
                  }
                ]
              }
            ]
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-recipients/1/funding-accounts")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
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
}
