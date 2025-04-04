using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<Currency>))]
public readonly record struct Currency : IStringEnum
{
    public static readonly Currency Aed = Custom(Values.Aed);

    public static readonly Currency Afn = Custom(Values.Afn);

    public static readonly Currency All = Custom(Values.All);

    public static readonly Currency Amd = Custom(Values.Amd);

    public static readonly Currency Ang = Custom(Values.Ang);

    public static readonly Currency Aoa = Custom(Values.Aoa);

    public static readonly Currency Ars = Custom(Values.Ars);

    public static readonly Currency Aud = Custom(Values.Aud);

    public static readonly Currency Awg = Custom(Values.Awg);

    public static readonly Currency Azn = Custom(Values.Azn);

    public static readonly Currency Bam = Custom(Values.Bam);

    public static readonly Currency Bbd = Custom(Values.Bbd);

    public static readonly Currency Bdt = Custom(Values.Bdt);

    public static readonly Currency Bgn = Custom(Values.Bgn);

    public static readonly Currency Bhd = Custom(Values.Bhd);

    public static readonly Currency Bif = Custom(Values.Bif);

    public static readonly Currency Bmd = Custom(Values.Bmd);

    public static readonly Currency Bnd = Custom(Values.Bnd);

    public static readonly Currency Bob = Custom(Values.Bob);

    public static readonly Currency Bov = Custom(Values.Bov);

    public static readonly Currency Brl = Custom(Values.Brl);

    public static readonly Currency Bsd = Custom(Values.Bsd);

    public static readonly Currency Btn = Custom(Values.Btn);

    public static readonly Currency Bwp = Custom(Values.Bwp);

    public static readonly Currency Byr = Custom(Values.Byr);

    public static readonly Currency Bzd = Custom(Values.Bzd);

    public static readonly Currency Cad = Custom(Values.Cad);

    public static readonly Currency Cdf = Custom(Values.Cdf);

    public static readonly Currency Che = Custom(Values.Che);

    public static readonly Currency Chf = Custom(Values.Chf);

    public static readonly Currency Chw = Custom(Values.Chw);

    public static readonly Currency Clf = Custom(Values.Clf);

    public static readonly Currency Clp = Custom(Values.Clp);

    public static readonly Currency Cny = Custom(Values.Cny);

    public static readonly Currency Cop = Custom(Values.Cop);

    public static readonly Currency Cou = Custom(Values.Cou);

    public static readonly Currency Crc = Custom(Values.Crc);

    public static readonly Currency Cuc = Custom(Values.Cuc);

    public static readonly Currency Cup = Custom(Values.Cup);

    public static readonly Currency Cve = Custom(Values.Cve);

    public static readonly Currency Czk = Custom(Values.Czk);

    public static readonly Currency Djf = Custom(Values.Djf);

    public static readonly Currency Dkk = Custom(Values.Dkk);

    public static readonly Currency Dop = Custom(Values.Dop);

    public static readonly Currency Dzd = Custom(Values.Dzd);

    public static readonly Currency Egp = Custom(Values.Egp);

    public static readonly Currency Ern = Custom(Values.Ern);

    public static readonly Currency Etb = Custom(Values.Etb);

    public static readonly Currency Eur = Custom(Values.Eur);

    public static readonly Currency Fjd = Custom(Values.Fjd);

    public static readonly Currency Fkp = Custom(Values.Fkp);

    public static readonly Currency Gbp = Custom(Values.Gbp);

    public static readonly Currency Gel = Custom(Values.Gel);

    public static readonly Currency Ghs = Custom(Values.Ghs);

    public static readonly Currency Gip = Custom(Values.Gip);

    public static readonly Currency Gmd = Custom(Values.Gmd);

    public static readonly Currency Gnf = Custom(Values.Gnf);

    public static readonly Currency Gtq = Custom(Values.Gtq);

    public static readonly Currency Gyd = Custom(Values.Gyd);

    public static readonly Currency Hkd = Custom(Values.Hkd);

    public static readonly Currency Hnl = Custom(Values.Hnl);

    public static readonly Currency Hrk = Custom(Values.Hrk);

    public static readonly Currency Htg = Custom(Values.Htg);

    public static readonly Currency Huf = Custom(Values.Huf);

    public static readonly Currency Idr = Custom(Values.Idr);

    public static readonly Currency Ils = Custom(Values.Ils);

    public static readonly Currency Inr = Custom(Values.Inr);

    public static readonly Currency Iqd = Custom(Values.Iqd);

    public static readonly Currency Irr = Custom(Values.Irr);

    public static readonly Currency Isk = Custom(Values.Isk);

    public static readonly Currency Jmd = Custom(Values.Jmd);

    public static readonly Currency Jod = Custom(Values.Jod);

    public static readonly Currency Jpy = Custom(Values.Jpy);

    public static readonly Currency Kes = Custom(Values.Kes);

    public static readonly Currency Kgs = Custom(Values.Kgs);

    public static readonly Currency Khr = Custom(Values.Khr);

    public static readonly Currency Kmf = Custom(Values.Kmf);

    public static readonly Currency Kpw = Custom(Values.Kpw);

    public static readonly Currency Krw = Custom(Values.Krw);

    public static readonly Currency Kwd = Custom(Values.Kwd);

    public static readonly Currency Kyd = Custom(Values.Kyd);

    public static readonly Currency Kzt = Custom(Values.Kzt);

    public static readonly Currency Lak = Custom(Values.Lak);

    public static readonly Currency Lbp = Custom(Values.Lbp);

    public static readonly Currency Lkr = Custom(Values.Lkr);

    public static readonly Currency Lrd = Custom(Values.Lrd);

    public static readonly Currency Lsl = Custom(Values.Lsl);

    public static readonly Currency Ltl = Custom(Values.Ltl);

    public static readonly Currency Lvl = Custom(Values.Lvl);

    public static readonly Currency Lyd = Custom(Values.Lyd);

    public static readonly Currency Mad = Custom(Values.Mad);

    public static readonly Currency Mdl = Custom(Values.Mdl);

    public static readonly Currency Mga = Custom(Values.Mga);

    public static readonly Currency Mkd = Custom(Values.Mkd);

    public static readonly Currency Mmk = Custom(Values.Mmk);

    public static readonly Currency Mnt = Custom(Values.Mnt);

    public static readonly Currency Mop = Custom(Values.Mop);

    public static readonly Currency Mro = Custom(Values.Mro);

    public static readonly Currency Mru = Custom(Values.Mru);

    public static readonly Currency Mur = Custom(Values.Mur);

    public static readonly Currency Mvr = Custom(Values.Mvr);

    public static readonly Currency Mwk = Custom(Values.Mwk);

    public static readonly Currency Mxn = Custom(Values.Mxn);

    public static readonly Currency Mxv = Custom(Values.Mxv);

    public static readonly Currency Myr = Custom(Values.Myr);

    public static readonly Currency Mzn = Custom(Values.Mzn);

    public static readonly Currency Nad = Custom(Values.Nad);

    public static readonly Currency Ngn = Custom(Values.Ngn);

    public static readonly Currency Nio = Custom(Values.Nio);

    public static readonly Currency Nok = Custom(Values.Nok);

    public static readonly Currency Npr = Custom(Values.Npr);

    public static readonly Currency Nzd = Custom(Values.Nzd);

    public static readonly Currency Omr = Custom(Values.Omr);

    public static readonly Currency Pab = Custom(Values.Pab);

    public static readonly Currency Pen = Custom(Values.Pen);

    public static readonly Currency Pgk = Custom(Values.Pgk);

    public static readonly Currency Php = Custom(Values.Php);

    public static readonly Currency Pkr = Custom(Values.Pkr);

    public static readonly Currency Pln = Custom(Values.Pln);

    public static readonly Currency Pyg = Custom(Values.Pyg);

    public static readonly Currency Qar = Custom(Values.Qar);

    public static readonly Currency Ron = Custom(Values.Ron);

    public static readonly Currency Rsd = Custom(Values.Rsd);

    public static readonly Currency Rub = Custom(Values.Rub);

    public static readonly Currency Rwf = Custom(Values.Rwf);

    public static readonly Currency Sar = Custom(Values.Sar);

    public static readonly Currency Sbd = Custom(Values.Sbd);

    public static readonly Currency Scr = Custom(Values.Scr);

    public static readonly Currency Sdg = Custom(Values.Sdg);

    public static readonly Currency Sek = Custom(Values.Sek);

    public static readonly Currency Sgd = Custom(Values.Sgd);

    public static readonly Currency Shp = Custom(Values.Shp);

    public static readonly Currency Sll = Custom(Values.Sll);

    public static readonly Currency Sos = Custom(Values.Sos);

    public static readonly Currency Srd = Custom(Values.Srd);

    public static readonly Currency Ssp = Custom(Values.Ssp);

    public static readonly Currency Std = Custom(Values.Std);

    public static readonly Currency Stn = Custom(Values.Stn);

    public static readonly Currency Svc = Custom(Values.Svc);

    public static readonly Currency Syp = Custom(Values.Syp);

    public static readonly Currency Szl = Custom(Values.Szl);

    public static readonly Currency Thb = Custom(Values.Thb);

    public static readonly Currency Tjs = Custom(Values.Tjs);

    public static readonly Currency Tmt = Custom(Values.Tmt);

    public static readonly Currency Tnd = Custom(Values.Tnd);

    public static readonly Currency Top = Custom(Values.Top);

    public static readonly Currency Try = Custom(Values.Try);

    public static readonly Currency Ttd = Custom(Values.Ttd);

    public static readonly Currency Twd = Custom(Values.Twd);

    public static readonly Currency Tzs = Custom(Values.Tzs);

    public static readonly Currency Uah = Custom(Values.Uah);

    public static readonly Currency Ugx = Custom(Values.Ugx);

    public static readonly Currency Usd = Custom(Values.Usd);

    public static readonly Currency Usn = Custom(Values.Usn);

    public static readonly Currency Uss = Custom(Values.Uss);

    public static readonly Currency Uyi = Custom(Values.Uyi);

    public static readonly Currency Uyu = Custom(Values.Uyu);

    public static readonly Currency Uzs = Custom(Values.Uzs);

    public static readonly Currency Vef = Custom(Values.Vef);

    public static readonly Currency Ves = Custom(Values.Ves);

    public static readonly Currency Vnd = Custom(Values.Vnd);

    public static readonly Currency Vuv = Custom(Values.Vuv);

    public static readonly Currency Wst = Custom(Values.Wst);

    public static readonly Currency Xaf = Custom(Values.Xaf);

    public static readonly Currency Xcd = Custom(Values.Xcd);

    public static readonly Currency Xof = Custom(Values.Xof);

    public static readonly Currency Xpf = Custom(Values.Xpf);

    public static readonly Currency Yer = Custom(Values.Yer);

    public static readonly Currency Zar = Custom(Values.Zar);

    public static readonly Currency Zmw = Custom(Values.Zmw);

    public static readonly Currency Zwl = Custom(Values.Zwl);

    public Currency(string value)
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
    public static Currency Custom(string value)
    {
        return new Currency(value);
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

    public static bool operator ==(Currency value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(Currency value1, string value2) => !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Aed = "AED";

        public const string Afn = "AFN";

        public const string All = "ALL";

        public const string Amd = "AMD";

        public const string Ang = "ANG";

        public const string Aoa = "AOA";

        public const string Ars = "ARS";

        public const string Aud = "AUD";

        public const string Awg = "AWG";

        public const string Azn = "AZN";

        public const string Bam = "BAM";

        public const string Bbd = "BBD";

        public const string Bdt = "BDT";

        public const string Bgn = "BGN";

        public const string Bhd = "BHD";

        public const string Bif = "BIF";

        public const string Bmd = "BMD";

        public const string Bnd = "BND";

        public const string Bob = "BOB";

        public const string Bov = "BOV";

        public const string Brl = "BRL";

        public const string Bsd = "BSD";

        public const string Btn = "BTN";

        public const string Bwp = "BWP";

        public const string Byr = "BYR";

        public const string Bzd = "BZD";

        public const string Cad = "CAD";

        public const string Cdf = "CDF";

        public const string Che = "CHE";

        public const string Chf = "CHF";

        public const string Chw = "CHW";

        public const string Clf = "CLF";

        public const string Clp = "CLP";

        public const string Cny = "CNY";

        public const string Cop = "COP";

        public const string Cou = "COU";

        public const string Crc = "CRC";

        public const string Cuc = "CUC";

        public const string Cup = "CUP";

        public const string Cve = "CVE";

        public const string Czk = "CZK";

        public const string Djf = "DJF";

        public const string Dkk = "DKK";

        public const string Dop = "DOP";

        public const string Dzd = "DZD";

        public const string Egp = "EGP";

        public const string Ern = "ERN";

        public const string Etb = "ETB";

        public const string Eur = "EUR";

        public const string Fjd = "FJD";

        public const string Fkp = "FKP";

        public const string Gbp = "GBP";

        public const string Gel = "GEL";

        public const string Ghs = "GHS";

        public const string Gip = "GIP";

        public const string Gmd = "GMD";

        public const string Gnf = "GNF";

        public const string Gtq = "GTQ";

        public const string Gyd = "GYD";

        public const string Hkd = "HKD";

        public const string Hnl = "HNL";

        public const string Hrk = "HRK";

        public const string Htg = "HTG";

        public const string Huf = "HUF";

        public const string Idr = "IDR";

        public const string Ils = "ILS";

        public const string Inr = "INR";

        public const string Iqd = "IQD";

        public const string Irr = "IRR";

        public const string Isk = "ISK";

        public const string Jmd = "JMD";

        public const string Jod = "JOD";

        public const string Jpy = "JPY";

        public const string Kes = "KES";

        public const string Kgs = "KGS";

        public const string Khr = "KHR";

        public const string Kmf = "KMF";

        public const string Kpw = "KPW";

        public const string Krw = "KRW";

        public const string Kwd = "KWD";

        public const string Kyd = "KYD";

        public const string Kzt = "KZT";

        public const string Lak = "LAK";

        public const string Lbp = "LBP";

        public const string Lkr = "LKR";

        public const string Lrd = "LRD";

        public const string Lsl = "LSL";

        public const string Ltl = "LTL";

        public const string Lvl = "LVL";

        public const string Lyd = "LYD";

        public const string Mad = "MAD";

        public const string Mdl = "MDL";

        public const string Mga = "MGA";

        public const string Mkd = "MKD";

        public const string Mmk = "MMK";

        public const string Mnt = "MNT";

        public const string Mop = "MOP";

        public const string Mro = "MRO";

        public const string Mru = "MRU";

        public const string Mur = "MUR";

        public const string Mvr = "MVR";

        public const string Mwk = "MWK";

        public const string Mxn = "MXN";

        public const string Mxv = "MXV";

        public const string Myr = "MYR";

        public const string Mzn = "MZN";

        public const string Nad = "NAD";

        public const string Ngn = "NGN";

        public const string Nio = "NIO";

        public const string Nok = "NOK";

        public const string Npr = "NPR";

        public const string Nzd = "NZD";

        public const string Omr = "OMR";

        public const string Pab = "PAB";

        public const string Pen = "PEN";

        public const string Pgk = "PGK";

        public const string Php = "PHP";

        public const string Pkr = "PKR";

        public const string Pln = "PLN";

        public const string Pyg = "PYG";

        public const string Qar = "QAR";

        public const string Ron = "RON";

        public const string Rsd = "RSD";

        public const string Rub = "RUB";

        public const string Rwf = "RWF";

        public const string Sar = "SAR";

        public const string Sbd = "SBD";

        public const string Scr = "SCR";

        public const string Sdg = "SDG";

        public const string Sek = "SEK";

        public const string Sgd = "SGD";

        public const string Shp = "SHP";

        public const string Sll = "SLL";

        public const string Sos = "SOS";

        public const string Srd = "SRD";

        public const string Ssp = "SSP";

        public const string Std = "STD";

        public const string Stn = "STN";

        public const string Svc = "SVC";

        public const string Syp = "SYP";

        public const string Szl = "SZL";

        public const string Thb = "THB";

        public const string Tjs = "TJS";

        public const string Tmt = "TMT";

        public const string Tnd = "TND";

        public const string Top = "TOP";

        public const string Try = "TRY";

        public const string Ttd = "TTD";

        public const string Twd = "TWD";

        public const string Tzs = "TZS";

        public const string Uah = "UAH";

        public const string Ugx = "UGX";

        public const string Usd = "USD";

        public const string Usn = "USN";

        public const string Uss = "USS";

        public const string Uyi = "UYI";

        public const string Uyu = "UYU";

        public const string Uzs = "UZS";

        public const string Vef = "VEF";

        public const string Ves = "VES";

        public const string Vnd = "VND";

        public const string Vuv = "VUV";

        public const string Wst = "WST";

        public const string Xaf = "XAF";

        public const string Xcd = "XCD";

        public const string Xof = "XOF";

        public const string Xpf = "XPF";

        public const string Yer = "YER";

        public const string Zar = "ZAR";

        public const string Zmw = "ZMW";

        public const string Zwl = "ZWL";
    }
}
