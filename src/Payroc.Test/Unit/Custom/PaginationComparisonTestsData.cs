namespace Payroc.Test.Unit.Custom;

public class PaginationComparisonTestsData
{
    public const string ListFundingAccountsResponseSingleBlob = """
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
    public const string ListFundingRecipientsResponseSingleBlob = """
            {
              "count": 1,
              "data": [
                {
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
                  "recipientType": "publicCorporation",
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
    public const string ListPaymentsResponseSingleBlob = """
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        				"expiryDate": "1230",
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
        			"href": "https://api.payroc.com/v1/payments?processingTerminalId=1234001&limit=2&before=E29U8OUQ01"
        		}
        	]
        }
        """;
    public const string ListPaymentsResponsePage1 = """
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        			"href": "{Server.Urls[0]}/payments?processingTerminalId=1234001&limit=2&after=E29U8OUQ11"
        		},
        		{
        			"rel": "previous",
        			"method": "get",
        			"href": "{Server.Urls[0]}/payments?processingTerminalId=1234001&limit=2&before=E29U8OUQ00"
        		}
        	]
        }
        """;
    public const string ListPaymentsResponsePage2 = """
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        			"href": "{Server.Urls[0]}/payments?processingTerminalId=1234001&limit=2&after=E29U8OUQ21"
        		},
        		{
        			"rel": "previous",
        			"method": "get",
        			"href": "{Server.Urls[0]}/payments?processingTerminalId=1234001&limit=2&before=E29U8OUQ10"
        		}
        	]
        }
        """;
    public const string ListPaymentsResponsePage3 = """
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        			"href": "{Server.Urls[0]}/payments?processingTerminalId=1234001&limit=2&after=E29U8OUQ31"
        		},
        		{
        			"rel": "previous",
        			"method": "get",
        			"href": "{Server.Urls[0]}/payments?processingTerminalId=1234001&limit=2&before=E29U8OUQ20"
        		}
        	]
        }
        """;
    public const string ListPaymentsResponsePage4 = """
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        				"expiryDate": "1230",
        				"cardholderName": "Sarah Hopper",
        				"secureToken": {
        					"secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
        					"customerName": "Sarah Hopper",
        					"token": "296753123456",
        					"status": "notValidated",
        					"link": {
        						"rel": "self",
        						"method": "GET",
        						"href": "{Server.Urls[0]}/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
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
        			"href": "{Server.Urls[0]}/payments?processingTerminalId=1234001&limit=2&after=E29U8OUQ35"
        		},
        		{
        			"rel": "previous",
        			"method": "get",
        			"href": "{Server.Urls[0]}/payments?processingTerminalId=1234001&limit=2&before=E29U8OUQ30"
        		}
        	]
        }
        """;
}
