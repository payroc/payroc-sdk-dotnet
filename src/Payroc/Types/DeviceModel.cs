using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<DeviceModel>))]
public readonly record struct DeviceModel : IStringEnum
{
    public static readonly DeviceModel BbposChp = new(Values.BbposChp);

    public static readonly DeviceModel BbposChp2X = new(Values.BbposChp2X);

    public static readonly DeviceModel BbposChp3X = new(Values.BbposChp3X);

    public static readonly DeviceModel BbposRambler = new(Values.BbposRambler);

    public static readonly DeviceModel BbposWp = new(Values.BbposWp);

    public static readonly DeviceModel BbposWp2 = new(Values.BbposWp2);

    public static readonly DeviceModel BbposWp3 = new(Values.BbposWp3);

    public static readonly DeviceModel GenericCtlsMsr = new(Values.GenericCtlsMsr);

    public static readonly DeviceModel GenericMsr = new(Values.GenericMsr);

    public static readonly DeviceModel IdtechAugusta = new(Values.IdtechAugusta);

    public static readonly DeviceModel IdtechMinismart = new(Values.IdtechMinismart);

    public static readonly DeviceModel IdtechSredkey = new(Values.IdtechSredkey);

    public static readonly DeviceModel IdtechVp3300 = new(Values.IdtechVp3300);

    public static readonly DeviceModel IdtechVp5300 = new(Values.IdtechVp5300);

    public static readonly DeviceModel IdtechVp6300 = new(Values.IdtechVp6300);

    public static readonly DeviceModel IdtechVp6800 = new(Values.IdtechVp6800);

    public static readonly DeviceModel IngenicoAxiumDx4000 = new(Values.IngenicoAxiumDx4000);

    public static readonly DeviceModel IngenicoAxiumDx8000 = new(Values.IngenicoAxiumDx8000);

    public static readonly DeviceModel IngenicoAxiumEx8000 = new(Values.IngenicoAxiumEx8000);

    public static readonly DeviceModel IngenicoIct220 = new(Values.IngenicoIct220);

    public static readonly DeviceModel IngenicoIpp320 = new(Values.IngenicoIpp320);

    public static readonly DeviceModel IngenicoIpp350 = new(Values.IngenicoIpp350);

    public static readonly DeviceModel IngenicoIuc285 = new(Values.IngenicoIuc285);

    public static readonly DeviceModel IngenicoL3000 = new(Values.IngenicoL3000);

    public static readonly DeviceModel IngenicoL7000 = new(Values.IngenicoL7000);

    public static readonly DeviceModel IngenicoS2000 = new(Values.IngenicoS2000);

    public static readonly DeviceModel IngenicoS3000 = new(Values.IngenicoS3000);

    public static readonly DeviceModel IngenicoS4000 = new(Values.IngenicoS4000);

    public static readonly DeviceModel IngenicoS5000 = new(Values.IngenicoS5000);

    public static readonly DeviceModel IngenicoS7000 = new(Values.IngenicoS7000);

    public static readonly DeviceModel PaxA80 = new(Values.PaxA80);

    public static readonly DeviceModel PaxA920 = new(Values.PaxA920);

    public static readonly DeviceModel PaxA920Pro = new(Values.PaxA920Pro);

    public static readonly DeviceModel PaxE500 = new(Values.PaxE500);

    public static readonly DeviceModel PaxE700 = new(Values.PaxE700);

    public static readonly DeviceModel PaxE800 = new(Values.PaxE800);

    public static readonly DeviceModel PaxIm30 = new(Values.PaxIm30);

    public static readonly DeviceModel Uic680 = new(Values.Uic680);

    public static readonly DeviceModel UicBezel8 = new(Values.UicBezel8);

    public DeviceModel(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static DeviceModel FromCustom(string value)
    {
        return new DeviceModel(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(DeviceModel value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(DeviceModel value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(DeviceModel value) => value.Value;

    public static explicit operator DeviceModel(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string BbposChp = "bbposChp";

        public const string BbposChp2X = "bbposChp2x";

        public const string BbposChp3X = "bbposChp3x";

        public const string BbposRambler = "bbposRambler";

        public const string BbposWp = "bbposWp";

        public const string BbposWp2 = "bbposWp2";

        public const string BbposWp3 = "bbposWp3";

        public const string GenericCtlsMsr = "genericCtlsMsr";

        public const string GenericMsr = "genericMsr";

        public const string IdtechAugusta = "idtechAugusta";

        public const string IdtechMinismart = "idtechMinismart";

        public const string IdtechSredkey = "idtechSredkey";

        public const string IdtechVp3300 = "idtechVp3300";

        public const string IdtechVp5300 = "idtechVp5300";

        public const string IdtechVp6300 = "idtechVp6300";

        public const string IdtechVp6800 = "idtechVp6800";

        public const string IngenicoAxiumDx4000 = "ingenicoAxiumDx4000";

        public const string IngenicoAxiumDx8000 = "ingenicoAxiumDx8000";

        public const string IngenicoAxiumEx8000 = "ingenicoAxiumEx8000";

        public const string IngenicoIct220 = "ingenicoIct220";

        public const string IngenicoIpp320 = "ingenicoIpp320";

        public const string IngenicoIpp350 = "ingenicoIpp350";

        public const string IngenicoIuc285 = "ingenicoIuc285";

        public const string IngenicoL3000 = "ingenicoL3000";

        public const string IngenicoL7000 = "ingenicoL7000";

        public const string IngenicoS2000 = "ingenicoS2000";

        public const string IngenicoS3000 = "ingenicoS3000";

        public const string IngenicoS4000 = "ingenicoS4000";

        public const string IngenicoS5000 = "ingenicoS5000";

        public const string IngenicoS7000 = "ingenicoS7000";

        public const string PaxA80 = "paxA80";

        public const string PaxA920 = "paxA920";

        public const string PaxA920Pro = "paxA920Pro";

        public const string PaxE500 = "paxE500";

        public const string PaxE700 = "paxE700";

        public const string PaxE800 = "paxE800";

        public const string PaxIm30 = "paxIm30";

        public const string Uic680 = "uic680";

        public const string UicBezel8 = "uicBezel8";
    }
}
