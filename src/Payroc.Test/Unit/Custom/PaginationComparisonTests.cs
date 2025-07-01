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
    #region Test Data
    private const string ListFundingAccountsResponseSingleBlob = """
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
    private const string ListFundingRecipientsResponseSingleBlob = """
            {
              "count": 1,
              "data": [
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456701",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456702",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456703",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456704",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456705",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456706",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456707",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456708",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456709",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456710",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456711",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456712",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456713",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456714",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456715",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456716",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456717",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456718",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456719",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456720",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456721",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456722",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456723",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456724",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456725",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456726",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456727",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456728",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456729",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456730",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456731",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456732",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456733",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                },
                {
                  "recipientType": "privateCorporation",
                  "taxId": "12-3456734",
                  "doingBusinessAs": "Pizza Doe",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "contactMethods": [
                    {
                      "type": "phone",
                      "value": "2025550164"
                    }
                  ],
                  "owners": [
                    {
                      "ownerId": 4564,
                      "link": {
                        "rel": "owner",
                        "href": "https://api.payroc.com/v1/owners/4564",
                        "method": "get"
                      }
                    }
                  ],
                  "fundingAccounts": [
                    {
                      "fundingAccountId": 123,
                      "status": "approved",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/123",
                        "method": "get"
                      }
                    },
                    {
                      "fundingAccountId": 124,
                      "status": "hold",
                      "link": {
                        "rel": "fundingAccount",
                        "href": "https://api.payroc.com/v1/funding-accounts/124",
                        "method": "get"
                      }
                    }
                  ],
                  "recipientId": 234,
                  "status": "approved",
                  "createdDate": "2024-07-02T15:30:00Z",
                  "lastModifiedDate": "2024-07-02T15:30:00Z",
                  "metadata": {
                    "yourCustomField": "abc123"
                  }
                }
              ],
              "hasMore": false,
              "limit": 10,
              "links": [
                {
                  "rel": "previous",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/funding-recipients?before=234&limit=10"
                }
              ]
            }
            """;
    private const string ListPaymentsResponseSingleBlob = """
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
    private const string ListPaymentsResponsePage1 = """
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        			"href": "{Server.Urls[0]}/v1/payments?processingTerminalId=1234001&limit=2&after=E29U8OUQ11"
        		},
        		{
        			"rel": "previous",
        			"method": "get",
        			"href": "{Server.Urls[0]}/v1/payments?processingTerminalId=1234001&limit=2&beforeE29U8OUQ00"
        		}
        	]
        }
        """;
    private const string ListPaymentsResponsePage2 = """
        {
        	"limit": 2,
        	"count": 2,
        	"hasMore": true,
        	"data": [
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        			"href": "{Server.Urls[0]}/v1/payments?processingTerminalId=1234001&limit=2&after=E29U8OUQ21"
        		},
        		{
        			"rel": "previous",
        			"method": "get",
        			"href": "{Server.Urls[0]}/v1/payments?processingTerminalId=1234001&limit=2&beforeE29U8OUQ10"
        		}
        	]
        }
        """;
    private const string ListPaymentsResponsePage3 = """
        {
        	"limit": 2,
        	"count": 2,
        	"hasMore": true,
        	"data": [
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        			"href": "{Server.Urls[0]}/v1/payments?processingTerminalId=1234001&limit=2&after=E29U8OUQ31"
        		},
        		{
        			"rel": "previous",
        			"method": "get",
        			"href": "{Server.Urls[0]}/v1/payments?processingTerminalId=1234001&limit=2&beforeE29U8OUQ20"
        		}
        	]
        }
        """;
    private const string ListPaymentsResponsePage4 = """
        {
        	"limit": 2,
        	"count": 2,
        	"hasMore": true,
        	"data": [
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        						"href": "{Server.Urls[0]}/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        			"href": "{Server.Urls[0]}/v1/payments?processingTerminalId=1234001&limit=2&after=E29U8OUQ35"
        		},
        		{
        			"rel": "previous",
        			"method": "get",
        			"href": "{Server.Urls[0]}/v1/payments?processingTerminalId=1234001&limit=2&beforeE29U8OUQ30"
        		}
        	]
        }
        """;
    #endregion

    [Test]
    public async global::System.Threading.Tasks.Task SingleBlobFundingAccounts()
    {
        const string mockResponse = ListFundingAccountsResponseSingleBlob;

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
        const string mockResponse = ListFundingRecipientsResponseSingleBlob;

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
        const string mockResponse = ListPaymentsResponseSingleBlob;
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
                            1 => ListPaymentsResponsePage1.Replace("{Server.Urls[0]}", Server.Urls[0]),
                            2 => ListPaymentsResponsePage2.Replace("{Server.Urls[0]}", Server.Urls[0]),
                            3 => ListPaymentsResponsePage3.Replace("{Server.Urls[0]}", Server.Urls[0]),
                            4 => ListPaymentsResponsePage4.Replace("{Server.Urls[0]}", Server.Urls[0]),
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
        expected.AddRange(JsonDataPropertyReader.DeserializeDataProperty<Payment>(ListPaymentsResponsePage1));
        expected.AddRange(JsonDataPropertyReader.DeserializeDataProperty<Payment>(ListPaymentsResponsePage2));
        expected.AddRange(JsonDataPropertyReader.DeserializeDataProperty<Payment>(ListPaymentsResponsePage3));
        expected.AddRange(JsonDataPropertyReader.DeserializeDataProperty<Payment>(ListPaymentsResponsePage4));

        Assert.That(actual.Count, Is.EqualTo(expected.Count));

        for (var i = 0; i < expected.Count; i++)
        {
            var expectedJson = JsonUtils.Serialize(expected[i]);
            var actualJson = JsonUtils.Serialize(actual[i]);

            Assert.That(actualJson, Is.EqualTo(expectedJson), $"Mismatch at index {i}");
        }
    }
}
