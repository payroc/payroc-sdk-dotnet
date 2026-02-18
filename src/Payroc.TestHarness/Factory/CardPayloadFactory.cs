namespace Payroc.TestHarness.Factory;

public class CardPayloadFactory
{
    public static CardPayload Create(string processingTerminalId = "5984001")
        => new()
        {
            CardDetails = new CardPayloadCardDetails.Keyed(new KeyedCardDetails()
            {
                KeyedData = new KeyedCardDetailsKeyedData.PlainText(new PlainTextKeyedDataFormat()
                {
                    Device = new()
                    {
                        Model = DeviceModel.PaxA80,
                        SerialNumber = "WPC202833004712"
                    },
                    ExpiryDate = "1230",
                    CardNumber = "4539858876047062"
                })
            })
        };
}
