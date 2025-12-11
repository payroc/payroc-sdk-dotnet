using Payroc.CardPayments.Payments;

namespace Payroc.TestFunctional.Payments;

[TestFixture, Category("CardPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class PayloadTests
{
    private static string NewKey() => Guid.NewGuid().ToString();

    [Test]
#if !WINDOWS
    [Ignore("Windows only")]
#endif
    public async Task Payments_ICC_Payload()
    {
        var client = GlobalFixture.Payments;

            // create a payment - AVS disabled
            var paymentIccResponse = await client.CardPayments.Payments.CreateAsync(
                new PaymentRequest
                {

                    IdempotencyKey = NewKey(),
                    Channel = PaymentRequestChannel.Web,
                    ProcessingTerminalId = GlobalFixture.TerminalIdNoAvs,
                    Operator = "SDK Test",

                    Order = new PaymentOrderRequest
                    {
                        OrderId = "OrderRef6543",
                        Description = "Large Pepperoni Pizza",
                        Amount = 4999,
                        Currency = Currency.Usd,
                    },

                    Customer = new Customer
                    {
                        FirstName = "Sarah",
                        LastName = "Hopper",
                        BillingAddress = new Address
                        {
                            Address1 = "1 Example Ave.",
                            Address2 = "Example Address Line 2",
                            Address3 = "Example Address Line 3",
                            City = "Chicago",
                            State = "Illinois",
                            Country = "US",
                            PostalCode = "60056",
                        },
                        ShippingAddress = new Shipping
                        {
                            RecipientName = "Sarah Hopper",
                            Address = new Address
                            {
                                Address1 = "1 Example Ave.",
                                Address2 = "Example Address Line 2",
                                Address3 = "Example Address Line 3",
                                City = "Chicago",
                                State = "Illinois",
                                Country = "US",
                                PostalCode = "60056",
                            },
                        },
                    },

                    PaymentMethod = new PaymentRequestPaymentMethod(
                        new PaymentRequestPaymentMethod.Card(
                            new CardPayload
                            {
                                CardDetails = new CardPayloadCardDetails(
                                    new CardPayloadCardDetails.Icc(
                                        new IccCardDetails
                                        {
                                            IccData =
                                                "DF7903312e305F2000DF780788888A33000039C00A88888833000043E0001CC408374245FFFFF1006FC28201C031D2154933C9B4F738CC4C0C4D5D6871C4A50CA66E612D2EFEB6E231E3F82BA53B903B9A859123ED0197CAC5CB4591E69665FC16908876597A2B35E4D615EF97FF82E2FE5621D4ACF34046D6C7EFEC3104FCA06E4DB28883D692E078A0AC26A9EF01E537442B2DD5FBC41B3DD9386961F00ADFE39C61FC215D414B52C7A27D085DFE3A29D11F984C7D8C17ACAC6575A5EE7184129677ED1051677A66049DEA7B9E3E6A400C923BEDCFD4B1358A12210BE9C4753D81EE2FC882A742093B331B0C09D125598A453A0C12E0AC745C09D99B7439EA9E58673AEB98ECEBBB477B6DE0FB27CD59D6DCE6282EF47ECA6DA66E9B22CAB3EDCB59F8C2330C3A1F61220267086D7D08A82742406337D0BE48B8B1110125DFFD9C4372C76143FE3817172A780792E8D691287712D316E0FBF3E8CB492B5250F8C88391FA214EFCC15ABA684BFC7BAB8E58A6ED47F4712C36EB2C6F2E28459FA77D71B876E629A2E53A759371C3DE82B4BEF9ADFC4B1AD04DDAA6B56545A9C17B80A8365B325001E49E6AD66D8E7222660D5DB31E4A21E2FD01B5C260E1509689E133D242C0D78189E8CBEC12C8184EAE9D0EE4FF1F88B1D6C3607ED4EAE48A5EDF5E514F4F0CDE4515F8C783DF080A88888933000040E00021DC084A9D49147B3102AFC8187DA6F0954286B9BACEE9CF08480663ED7BB6CFEDCA12465CC70A88888B33000042E000214F08A0000000250104025F24032412315F2A02084082021C80950580000080009A032106099B0268009C01009F02060000000005689F03060000000000009F0606A000000025019F100706020103A028009F1210416D65726963616E20457870726573739F1A0208409F1C0831313232333334349F21031256419F2608785A0795E6117CE39F2701809F330360F8C89F34031F02029F360201529F37044833A0A89F420208409F5301FFDF826E0F575053333233303332303030343532DF834F0F575053333233303332303030343532",
                                            Device = new EncryptionCapableDevice
                                            {
                                                Model = DeviceModel.BbposWp2,
                                                SerialNumber = "WPS323129000066",
                                                Config = new DeviceConfig {QuickChip = false},
                                                Category = DeviceCategory.Attended,
                                                FirmwareVersion = "4.01.00.29",
                                                DataKsn = "88888833000043E0001C"
                                            },
                                        }))
                            }))
                });
            
            // retrieve the payment
            var paymentIccDetailResponse = await client.CardPayments.Payments.RetrieveAsync(
                new RetrievePaymentsRequest
                {
                    PaymentId = paymentIccResponse.PaymentId
                });
        
            Assert.Multiple(() =>
            {
                Assert.That(paymentIccDetailResponse.TransactionResult.Status, Is.EqualTo("ready"));
                Assert.That(paymentIccDetailResponse.TransactionResult.ResponseMessage, Is.EqualTo("APPROVAL"));
                Assert.That(paymentIccDetailResponse.TransactionResult.ResponseCode, Is.EqualTo("A"));
                Assert.That(paymentIccDetailResponse.TransactionResult.ProcessorResponseCode, Is.EqualTo("00"));
            });
    }
    
    [Test]
    [Retry(3)]
    [Category("Payments")]
#if !WINDOWS
    [Ignore("Windows only")]
#endif
    public async Task Payments_Raw_Payload()
    {
        var client = GlobalFixture.Payments;

            // create a payment - AVS disabled
            var paymentRawResponse = await client.CardPayments.Payments.CreateAsync(
                new PaymentRequest
                {

                    IdempotencyKey = NewKey(),
                    Channel = PaymentRequestChannel.Web,
                    ProcessingTerminalId = GlobalFixture.TerminalIdNoAvs,
                    Operator = "SDK Test",

                    Order = new PaymentOrderRequest
                    {
                        OrderId = "OrderRef6543",
                        Description = "Large Pepperoni Pizza",
                        Amount = 4999,
                        Currency = Currency.Usd,
                    },

                    Customer = new Customer
                    {
                        FirstName = "Sarah",
                        LastName = "Hopper",
                        BillingAddress = new Address
                        {
                            Address1 = "1 Example Ave.",
                            Address2 = "Example Address Line 2",
                            Address3 = "Example Address Line 3",
                            City = "Chicago",
                            State = "Illinois",
                            Country = "US",
                            PostalCode = "60056",
                        },
                        ShippingAddress = new Shipping
                        {
                            RecipientName = "Sarah Hopper",
                            Address = new Address
                            {
                                Address1 = "1 Example Ave.",
                                Address2 = "Example Address Line 2",
                                Address3 = "Example Address Line 3",
                                City = "Chicago",
                                State = "Illinois",
                                Country = "US",
                                PostalCode = "60056",
                            },
                        },
                    },

                    PaymentMethod = new PaymentRequestPaymentMethod(
                        new PaymentRequestPaymentMethod.Card(
                            new CardPayload
                            {
                                CardDetails = new CardPayloadCardDetails(
                                    new CardPayloadCardDetails.Raw(
                                        new RawCardDetails
                                        {
                                            RawData =
                                                "C0FFEE120A62994900AB0008C001EDDFEE250200045781134761CCCCCCCC0119D2212201CCCCCCCCCCCCCC578118C568AF7D7CEF1EB672D3A6552688D49CC32645295A7B305FDFEE0400DFEE1200DFEE1300DFEE140086009F4E2231303732312057616C6B65722053742E20437970726573732C204341202C5553412E9F42009F4104000000029F4005F000F0A0019F3901059F3704031584C99F3602007A9F3501219F34035E03009F33036028C89F2701809F260880E4775B5CB93C649F21031640129F1E0838543134393831369F1C0838373635343332319F1A0208409F160F3030303030303030303030303030309F100706010A03A080029F0F05B870BC98009F0E0500000000009F0D05B850AC88009F090200969F0702FF009F0607A00000000310109F03060000000000009F02060000000012509F01009F1B04000000009C01009B02C8009A03210120950500000080008E100000000000000000420141035E031F038D178A029F02069F03069F1A0295055F2A029A039C019F37048C159F02069F03069F1A0295055F2A029A039C019F37048407A000000003101082023C004F07A00000000310105F3401015F300202015F2A0208405F25032005015F24032212315F201A554154205553412F5465737420436172642030322020202020205A81084761CCCCCCCC01195A81105924C7987282A9533CF1B5AA2F131E8A500B5649534120435245444954FFEE0104DF300101DFEE2601C0",
                                            Device = new Device
                                            {
                                                Model = DeviceModel.IdtechVp3300,
                                                SerialNumber = "0820619250",
                                                Category = DeviceCategory.Unattended
                                            },
                                        }))
                            }))
                });
            
            // retrieve the payment
            var paymentRawDetailResponse = await client.CardPayments.Payments.RetrieveAsync(
                new RetrievePaymentsRequest
                {
                    PaymentId = paymentRawResponse.PaymentId
                });
        
            Assert.Multiple(() =>
            {
                Assert.That(paymentRawDetailResponse.TransactionResult.Status, Is.EqualTo("ready"));
                Assert.That(paymentRawDetailResponse.TransactionResult.ResponseMessage, Is.EqualTo("APPROVAL"));
                Assert.That(paymentRawDetailResponse.TransactionResult.ResponseCode, Is.EqualTo("A"));
                Assert.That(paymentRawDetailResponse.TransactionResult.ProcessorResponseCode, Is.EqualTo("00"));
            });
    }
    
    [Test]
    [Retry(3)]
    [Category("Payments")]
#if !WINDOWS
    [Ignore("Windows only")]
#endif
    public async Task Payments_Swiped_Payload()
    {
        var client = GlobalFixture.Payments;

            // create a payment - AVS disabled
            var paymentSwipedResponse = await client.CardPayments.Payments.CreateAsync(
                new PaymentRequest
                {

                    IdempotencyKey = NewKey(),
                    Channel = PaymentRequestChannel.Web,
                    ProcessingTerminalId = GlobalFixture.TerminalIdNoAvs,
                    Operator = "SDK Test",

                    Order = new PaymentOrderRequest
                    {
                        OrderId = "OrderRef6543",
                        Description = "Large Pepperoni Pizza",
                        Amount = 4999,
                        Currency = Currency.Usd,
                    },

                    Customer = new Customer
                    {
                        FirstName = "Sarah",
                        LastName = "Hopper",
                        BillingAddress = new Address
                        {
                            Address1 = "1 Example Ave.",
                            Address2 = "Example Address Line 2",
                            Address3 = "Example Address Line 3",
                            City = "Chicago",
                            State = "Illinois",
                            Country = "US",
                            PostalCode = "60056",
                        },
                        ShippingAddress = new Shipping
                        {
                            RecipientName = "Sarah Hopper",
                            Address = new Address
                            {
                                Address1 = "1 Example Ave.",
                                Address2 = "Example Address Line 2",
                                Address3 = "Example Address Line 3",
                                City = "Chicago",
                                State = "Illinois",
                                Country = "US",
                                PostalCode = "60056",
                            },
                        },
                    },

                    PaymentMethod = new PaymentRequestPaymentMethod(
                        new PaymentRequestPaymentMethod.Card(
                            new CardPayload
                            {
                                CardDetails = new CardPayloadCardDetails(
                                    new CardPayloadCardDetails.Swiped(
                                        new SwipedCardDetails
                                        {
                                            SwipedData = new SwipedCardDetailsSwipedData.Encrypted(
                                                new EncryptedSwipedDataFormat()
                                                {
                                                    Device = new EncryptionCapableDevice
                                                    {
                                                        Model = DeviceModel.BbposWp2,
                                                        DataKsn = "88888B29100166E0028F",
                                                        FirmwareVersion = "4.01.00.11",
                                                        Category = DeviceCategory.Attended,
                                                        SerialNumber = "WPS323129000066"
                                                    },
                                                    EncryptedData = "A3770E6D6667F0B79A29561BF03C9029C3F0BC0D2BE1152A8D7BF7BE833B0F0AD68DC1E44520EEF2"
                                                }
                                                )   
                                        }
                                        ))
                            }))
                });
            
            // retrieve the payment
            var paymentSwipedDetailResponse = await client.CardPayments.Payments.RetrieveAsync(
                new RetrievePaymentsRequest
                {
                    PaymentId = paymentSwipedResponse.PaymentId
                });
        
            Assert.Multiple(() =>
            {
                Assert.That(paymentSwipedDetailResponse.TransactionResult.Status, Is.EqualTo("ready"));
                Assert.That(paymentSwipedDetailResponse.TransactionResult.ResponseMessage, Is.EqualTo("APPROVAL"));
                Assert.That(paymentSwipedDetailResponse.TransactionResult.ResponseCode, Is.EqualTo("A"));
                Assert.That(paymentSwipedDetailResponse.TransactionResult.ProcessorResponseCode, Is.EqualTo("00"));
            });
    }
}
