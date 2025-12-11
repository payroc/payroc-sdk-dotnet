namespace Payroc.TestFunctional.Factories;

public class OwnerFactory
{
    public static Owner Create(bool isControlProng = false, string value = "987-65-4320") => new()
    {
        FirstName = "Jane",
        LastName = "Doe",
        DateOfBirth = new(1964, 03, 22),
        Address = AddressFactory.CreateAddress(),
        Identifiers =
        [
            new()
            {
                Type = IdentifierType.NationalId,
                Value = "987-65-4320"
            }
        ],
        Relationship = new()
        {
            IsControlProng = isControlProng,
            EquityPercentage = 35.4f,
            Title = "Owner",
            IsAuthorizedSignatory = false
        },
        ContactMethods =
        [
            new ContactMethod.Email(new() { Value = "jane.doe@example.com" })
        ],
    };
}