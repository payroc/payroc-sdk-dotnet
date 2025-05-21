namespace Payroc.TestHarness.Debugging;

public static class DebugPayload
{
    public static string DataUnderTest =
        """
        {
        	"id": 1602,
        	"createdDate": "2025-05-21T09:05:14.13+00:00",
        	"lastUpdatedDate": "2025-05-21T09:05:14.13+00:00",
        	"status": "pendingReview",
        	"country": "US",
        	"base": {
        		"addressVerification": 5,
        		"annualFee": {
        			"billInMonth": "december",
        			"amount": 100
        		},
        		"regulatoryAssistanceProgram": 15,
        		"pciNonCompliance": 3995,
        		"merchantAdvantage": 10,
        		"batch": 5,
        		"earlyTermination": 19500,
        		"platinumSecurity": {
        			"billingFrequency": "annual",
        			"amount": 15540
        		},
        		"maintenance": 1995,
        		"minimum": 100,
        		"voiceAuthorization": 45,
        		"chargeback": 500,
        		"retrieval": 500
        	},
        	"processor": {
        		"card": {
        			"planType": "tiered4",
        			"fees": {
        				"mastercardVisaDiscover": {
        					"qualifiedRate": {
        						"volume": 4.0,
        						"transaction": 40
        					},
        					"midQualRate": {
        						"volume": 4.0,
        						"transaction": 40
        					},
        					"nonQualRate": {
        						"volume": 4.0,
        						"transaction": 40
        					},
        					"premiumRate": {
        						"volume": 4.0,
        						"transaction": 40
        					}
        				},
        				"amex": {
        					"type": "optBlue",
        					"qualifiedRate": {
        						"volume": 4.0,
        						"transaction": 40
        					},
        					"midQualRate": {
        						"volume": 4.0,
        						"transaction": 40
        					},
        					"nonQualRate": {
        						"volume": 4.0,
        						"transaction": 40
        					}
        				},
        				"pinDebit": {
        					"additionalDiscount": 0,
        					"transaction": 0,
        					"monthlyAccess": 5
        				},
        				"electronicBenefitsTransfer": {
        					"transaction": 70
        				},
        				"specialityCards": {
        					"transaction": 5
        				}
        			}
        		},
        		"ach": {
        			"fees": {
        				"transaction": 100,
        				"batch": 6000,
        				"returns": 400,
        				"unauthorizedReturn": 1999,
        				"statement": 800,
        				"monthlyMinimum": 1000,
        				"accountVerification": 2000,
        				"discountRateUnder10000": 5.0,
        				"discountRateAbove10000": 1.25
        			}
        		}
        	},
        	"services": [
        		{
        			"name": "hardwareAdvantagePlan",
        			"enabled": true
        		}
        	],
        	"key": "foo",
        	"version": "5.0"
        }
        """;
}
