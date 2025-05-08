using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<UnitOfMeasure>))]
public readonly record struct UnitOfMeasure : IStringEnum
{
    public static readonly UnitOfMeasure Acr = new(Values.Acr);

    public static readonly UnitOfMeasure Amh = new(Values.Amh);

    public static readonly UnitOfMeasure Amp = new(Values.Amp);

    public static readonly UnitOfMeasure Apz = new(Values.Apz);

    public static readonly UnitOfMeasure Are = new(Values.Are);

    public static readonly UnitOfMeasure Asm = new(Values.Asm);

    public static readonly UnitOfMeasure Asv = new(Values.Asv);

    public static readonly UnitOfMeasure Atm = new(Values.Atm);

    public static readonly UnitOfMeasure Att = new(Values.Att);

    public static readonly UnitOfMeasure Bar = new(Values.Bar);

    public static readonly UnitOfMeasure Bft = new(Values.Bft);

    public static readonly UnitOfMeasure Bhp = new(Values.Bhp);

    public static readonly UnitOfMeasure Bhx = new(Values.Bhx);

    public static readonly UnitOfMeasure Bil = new(Values.Bil);

    public static readonly UnitOfMeasure Bld = new(Values.Bld);

    public static readonly UnitOfMeasure Bll = new(Values.Bll);

    public static readonly UnitOfMeasure Bql = new(Values.Bql);

    public static readonly UnitOfMeasure Btu = new(Values.Btu);

    public static readonly UnitOfMeasure Bua = new(Values.Bua);

    public static readonly UnitOfMeasure Bui = new(Values.Bui);

    public static readonly UnitOfMeasure Bx = new(Values.Bx);

    public static readonly UnitOfMeasure Cct = new(Values.Cct);

    public static readonly UnitOfMeasure Cdl = new(Values.Cdl);

    public static readonly UnitOfMeasure Cel = new(Values.Cel);

    public static readonly UnitOfMeasure Cen = new(Values.Cen);

    public static readonly UnitOfMeasure Cgm = new(Values.Cgm);

    public static readonly UnitOfMeasure Ckg = new(Values.Ckg);

    public static readonly UnitOfMeasure Clf = new(Values.Clf);

    public static readonly UnitOfMeasure Clt = new(Values.Clt);

    public static readonly UnitOfMeasure Cmk = new(Values.Cmk);

    public static readonly UnitOfMeasure Cmt = new(Values.Cmt);

    public static readonly UnitOfMeasure Cnp = new(Values.Cnp);

    public static readonly UnitOfMeasure Cnt = new(Values.Cnt);

    public static readonly UnitOfMeasure Cou = new(Values.Cou);

    public static readonly UnitOfMeasure Cs = new(Values.Cs);

    public static readonly UnitOfMeasure Ctm = new(Values.Ctm);

    public static readonly UnitOfMeasure Cur = new(Values.Cur);

    public static readonly UnitOfMeasure Cwa = new(Values.Cwa);

    public static readonly UnitOfMeasure Daa = new(Values.Daa);

    public static readonly UnitOfMeasure Dad = new(Values.Dad);

    public static readonly UnitOfMeasure Day = new(Values.Day);

    public static readonly UnitOfMeasure Dec = new(Values.Dec);

    public static readonly UnitOfMeasure Dlt = new(Values.Dlt);

    public static readonly UnitOfMeasure Dmk = new(Values.Dmk);

    public static readonly UnitOfMeasure Dmq = new(Values.Dmq);

    public static readonly UnitOfMeasure Dmt = new(Values.Dmt);

    public static readonly UnitOfMeasure Dpc = new(Values.Dpc);

    public static readonly UnitOfMeasure Dpt = new(Values.Dpt);

    public static readonly UnitOfMeasure Dra = new(Values.Dra);

    public static readonly UnitOfMeasure Dri = new(Values.Dri);

    public static readonly UnitOfMeasure Drl = new(Values.Drl);

    public static readonly UnitOfMeasure Drm = new(Values.Drm);

    public static readonly UnitOfMeasure Dth = new(Values.Dth);

    public static readonly UnitOfMeasure Dtn = new(Values.Dtn);

    public static readonly UnitOfMeasure Dwt = new(Values.Dwt);

    public static readonly UnitOfMeasure Dzn = new(Values.Dzn);

    public static readonly UnitOfMeasure Dzp = new(Values.Dzp);

    public static readonly UnitOfMeasure Dzr = new(Values.Dzr);

    public static readonly UnitOfMeasure Ea = new(Values.Ea);

    public static readonly UnitOfMeasure Eac = new(Values.Eac);

    public static readonly UnitOfMeasure Fah = new(Values.Fah);

    public static readonly UnitOfMeasure Far = new(Values.Far);

    public static readonly UnitOfMeasure Fot = new(Values.Fot);

    public static readonly UnitOfMeasure Ftk = new(Values.Ftk);

    public static readonly UnitOfMeasure Ftq = new(Values.Ftq);

    public static readonly UnitOfMeasure Gbq = new(Values.Gbq);

    public static readonly UnitOfMeasure Gfi = new(Values.Gfi);

    public static readonly UnitOfMeasure Ggr = new(Values.Ggr);

    public static readonly UnitOfMeasure Gii = new(Values.Gii);

    public static readonly UnitOfMeasure Gld = new(Values.Gld);

    public static readonly UnitOfMeasure Gli = new(Values.Gli);

    public static readonly UnitOfMeasure Gll = new(Values.Gll);

    public static readonly UnitOfMeasure Grm = new(Values.Grm);

    public static readonly UnitOfMeasure Grn = new(Values.Grn);

    public static readonly UnitOfMeasure Gro = new(Values.Gro);

    public static readonly UnitOfMeasure Grt = new(Values.Grt);

    public static readonly UnitOfMeasure Gwh = new(Values.Gwh);

    public static readonly UnitOfMeasure Har = new(Values.Har);

    public static readonly UnitOfMeasure Hba = new(Values.Hba);

    public static readonly UnitOfMeasure Hgm = new(Values.Hgm);

    public static readonly UnitOfMeasure Hiu = new(Values.Hiu);

    public static readonly UnitOfMeasure Hlt = new(Values.Hlt);

    public static readonly UnitOfMeasure Hmq = new(Values.Hmq);

    public static readonly UnitOfMeasure Hmt = new(Values.Hmt);

    public static readonly UnitOfMeasure Hpa = new(Values.Hpa);

    public static readonly UnitOfMeasure Htz = new(Values.Htz);

    public static readonly UnitOfMeasure Hur = new(Values.Hur);

    public static readonly UnitOfMeasure Inh = new(Values.Inh);

    public static readonly UnitOfMeasure Ink = new(Values.Ink);

    public static readonly UnitOfMeasure Inq = new(Values.Inq);

    public static readonly UnitOfMeasure Itm = new(Values.Itm);

    public static readonly UnitOfMeasure Jou = new(Values.Jou);

    public static readonly UnitOfMeasure Kba = new(Values.Kba);

    public static readonly UnitOfMeasure Kel = new(Values.Kel);

    public static readonly UnitOfMeasure Kgm = new(Values.Kgm);

    public static readonly UnitOfMeasure Kgs = new(Values.Kgs);

    public static readonly UnitOfMeasure Khz = new(Values.Khz);

    public static readonly UnitOfMeasure Kjo = new(Values.Kjo);

    public static readonly UnitOfMeasure Kmh = new(Values.Kmh);

    public static readonly UnitOfMeasure Kmk = new(Values.Kmk);

    public static readonly UnitOfMeasure Kmq = new(Values.Kmq);

    public static readonly UnitOfMeasure Kmt = new(Values.Kmt);

    public static readonly UnitOfMeasure Kni = new(Values.Kni);

    public static readonly UnitOfMeasure Kns = new(Values.Kns);

    public static readonly UnitOfMeasure Knt = new(Values.Knt);

    public static readonly UnitOfMeasure Kpa = new(Values.Kpa);

    public static readonly UnitOfMeasure Kph = new(Values.Kph);

    public static readonly UnitOfMeasure Kpo = new(Values.Kpo);

    public static readonly UnitOfMeasure Kpp = new(Values.Kpp);

    public static readonly UnitOfMeasure Ksd = new(Values.Ksd);

    public static readonly UnitOfMeasure Ksh = new(Values.Ksh);

    public static readonly UnitOfMeasure Ktn = new(Values.Ktn);

    public static readonly UnitOfMeasure Kur = new(Values.Kur);

    public static readonly UnitOfMeasure Kva = new(Values.Kva);

    public static readonly UnitOfMeasure Kvr = new(Values.Kvr);

    public static readonly UnitOfMeasure Kvt = new(Values.Kvt);

    public static readonly UnitOfMeasure Kwh = new(Values.Kwh);

    public static readonly UnitOfMeasure Kwt = new(Values.Kwt);

    public static readonly UnitOfMeasure Lbr = new(Values.Lbr);

    public static readonly UnitOfMeasure Lbs = new(Values.Lbs);

    public static readonly UnitOfMeasure Lef = new(Values.Lef);

    public static readonly UnitOfMeasure Lpa = new(Values.Lpa);

    public static readonly UnitOfMeasure Ltn = new(Values.Ltn);

    public static readonly UnitOfMeasure Ltr = new(Values.Ltr);

    public static readonly UnitOfMeasure Lum = new(Values.Lum);

    public static readonly UnitOfMeasure Lux = new(Values.Lux);

    public static readonly UnitOfMeasure Mal = new(Values.Mal);

    public static readonly UnitOfMeasure Mam = new(Values.Mam);

    public static readonly UnitOfMeasure Maw = new(Values.Maw);

    public static readonly UnitOfMeasure Mbe = new(Values.Mbe);

    public static readonly UnitOfMeasure Mbf = new(Values.Mbf);

    public static readonly UnitOfMeasure Mbr = new(Values.Mbr);

    public static readonly UnitOfMeasure Mcu = new(Values.Mcu);

    public static readonly UnitOfMeasure Mgm = new(Values.Mgm);

    public static readonly UnitOfMeasure Mhz = new(Values.Mhz);

    public static readonly UnitOfMeasure Mik = new(Values.Mik);

    public static readonly UnitOfMeasure Mil = new(Values.Mil);

    public static readonly UnitOfMeasure Min = new(Values.Min);

    public static readonly UnitOfMeasure Mio = new(Values.Mio);

    public static readonly UnitOfMeasure Miu = new(Values.Miu);

    public static readonly UnitOfMeasure Mld = new(Values.Mld);

    public static readonly UnitOfMeasure Mlt = new(Values.Mlt);

    public static readonly UnitOfMeasure Mmk = new(Values.Mmk);

    public static readonly UnitOfMeasure Mmq = new(Values.Mmq);

    public static readonly UnitOfMeasure Mmt = new(Values.Mmt);

    public static readonly UnitOfMeasure Mon = new(Values.Mon);

    public static readonly UnitOfMeasure Mpa = new(Values.Mpa);

    public static readonly UnitOfMeasure Mqh = new(Values.Mqh);

    public static readonly UnitOfMeasure Mqs = new(Values.Mqs);

    public static readonly UnitOfMeasure Msk = new(Values.Msk);

    public static readonly UnitOfMeasure Mtk = new(Values.Mtk);

    public static readonly UnitOfMeasure Mtq = new(Values.Mtq);

    public static readonly UnitOfMeasure Mtr = new(Values.Mtr);

    public static readonly UnitOfMeasure Mts = new(Values.Mts);

    public static readonly UnitOfMeasure Mva = new(Values.Mva);

    public static readonly UnitOfMeasure Mwh = new(Values.Mwh);

    public static readonly UnitOfMeasure Nar = new(Values.Nar);

    public static readonly UnitOfMeasure Nbb = new(Values.Nbb);

    public static readonly UnitOfMeasure Ncl = new(Values.Ncl);

    public static readonly UnitOfMeasure New = new(Values.New);

    public static readonly UnitOfMeasure Niu = new(Values.Niu);

    public static readonly UnitOfMeasure Nmb = new(Values.Nmb);

    public static readonly UnitOfMeasure Nmi = new(Values.Nmi);

    public static readonly UnitOfMeasure Nmp = new(Values.Nmp);

    public static readonly UnitOfMeasure Nmr = new(Values.Nmr);

    public static readonly UnitOfMeasure Npl = new(Values.Npl);

    public static readonly UnitOfMeasure Npt = new(Values.Npt);

    public static readonly UnitOfMeasure Nrl = new(Values.Nrl);

    public static readonly UnitOfMeasure Ntt = new(Values.Ntt);

    public static readonly UnitOfMeasure Ohm = new(Values.Ohm);

    public static readonly UnitOfMeasure Onz = new(Values.Onz);

    public static readonly UnitOfMeasure Oza = new(Values.Oza);

    public static readonly UnitOfMeasure Ozi = new(Values.Ozi);

    public static readonly UnitOfMeasure Pal = new(Values.Pal);

    public static readonly UnitOfMeasure Pcb = new(Values.Pcb);

    public static readonly UnitOfMeasure Pce = new(Values.Pce);

    public static readonly UnitOfMeasure Pgl = new(Values.Pgl);

    public static readonly UnitOfMeasure Pk = new(Values.Pk);

    public static readonly UnitOfMeasure Psc = new(Values.Psc);

    public static readonly UnitOfMeasure Ptd = new(Values.Ptd);

    public static readonly UnitOfMeasure Pti = new(Values.Pti);

    public static readonly UnitOfMeasure Ptl = new(Values.Ptl);

    public static readonly UnitOfMeasure Qan = new(Values.Qan);

    public static readonly UnitOfMeasure Qtd = new(Values.Qtd);

    public static readonly UnitOfMeasure Qti = new(Values.Qti);

    public static readonly UnitOfMeasure Qtl = new(Values.Qtl);

    public static readonly UnitOfMeasure Qtr = new(Values.Qtr);

    public static readonly UnitOfMeasure Rpm = new(Values.Rpm);

    public static readonly UnitOfMeasure Rps = new(Values.Rps);

    public static readonly UnitOfMeasure San = new(Values.San);

    public static readonly UnitOfMeasure Sco = new(Values.Sco);

    public static readonly UnitOfMeasure Scr = new(Values.Scr);

    public static readonly UnitOfMeasure Sec = new(Values.Sec);

    public static readonly UnitOfMeasure Set = new(Values.Set);

    public static readonly UnitOfMeasure Sht = new(Values.Sht);

    public static readonly UnitOfMeasure Sie = new(Values.Sie);

    public static readonly UnitOfMeasure Smi = new(Values.Smi);

    public static readonly UnitOfMeasure Sst = new(Values.Sst);

    public static readonly UnitOfMeasure St = new(Values.St);

    public static readonly UnitOfMeasure Sti = new(Values.Sti);

    public static readonly UnitOfMeasure Tah = new(Values.Tah);

    public static readonly UnitOfMeasure Tne = new(Values.Tne);

    public static readonly UnitOfMeasure Tpr = new(Values.Tpr);

    public static readonly UnitOfMeasure Tqd = new(Values.Tqd);

    public static readonly UnitOfMeasure Trl = new(Values.Trl);

    public static readonly UnitOfMeasure Tsd = new(Values.Tsd);

    public static readonly UnitOfMeasure Tsh = new(Values.Tsh);

    public static readonly UnitOfMeasure Vlt = new(Values.Vlt);

    public static readonly UnitOfMeasure Wcd = new(Values.Wcd);

    public static readonly UnitOfMeasure Web = new(Values.Web);

    public static readonly UnitOfMeasure Wee = new(Values.Wee);

    public static readonly UnitOfMeasure Whr = new(Values.Whr);

    public static readonly UnitOfMeasure Wsd = new(Values.Wsd);

    public static readonly UnitOfMeasure Wtt = new(Values.Wtt);

    public static readonly UnitOfMeasure Ydk = new(Values.Ydk);

    public static readonly UnitOfMeasure Ydq = new(Values.Ydq);

    public UnitOfMeasure(string value)
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
    public static UnitOfMeasure FromCustom(string value)
    {
        return new UnitOfMeasure(value);
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

    public static bool operator ==(UnitOfMeasure value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(UnitOfMeasure value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(UnitOfMeasure value) => value.Value;

    public static explicit operator UnitOfMeasure(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Acr = "ACR";

        public const string Amh = "AMH";

        public const string Amp = "AMP";

        public const string Apz = "APZ";

        public const string Are = "ARE";

        public const string Asm = "ASM";

        public const string Asv = "ASV";

        public const string Atm = "ATM";

        public const string Att = "ATT";

        public const string Bar = "BAR";

        public const string Bft = "BFT";

        public const string Bhp = "BHP";

        public const string Bhx = "BHX";

        public const string Bil = "BIL";

        public const string Bld = "BLD";

        public const string Bll = "BLL";

        public const string Bql = "BQL";

        public const string Btu = "BTU";

        public const string Bua = "BUA";

        public const string Bui = "BUI";

        public const string Bx = "BX";

        public const string Cct = "CCT";

        public const string Cdl = "CDL";

        public const string Cel = "CEL";

        public const string Cen = "CEN";

        public const string Cgm = "CGM";

        public const string Ckg = "CKG";

        public const string Clf = "CLF";

        public const string Clt = "CLT";

        public const string Cmk = "CMK";

        public const string Cmt = "CMT";

        public const string Cnp = "CNP";

        public const string Cnt = "CNT";

        public const string Cou = "COU";

        public const string Cs = "CS";

        public const string Ctm = "CTM";

        public const string Cur = "CUR";

        public const string Cwa = "CWA";

        public const string Daa = "DAA";

        public const string Dad = "DAD";

        public const string Day = "DAY";

        public const string Dec = "DEC";

        public const string Dlt = "DLT";

        public const string Dmk = "DMK";

        public const string Dmq = "DMQ";

        public const string Dmt = "DMT";

        public const string Dpc = "DPC";

        public const string Dpt = "DPT";

        public const string Dra = "DRA";

        public const string Dri = "DRI";

        public const string Drl = "DRL";

        public const string Drm = "DRM";

        public const string Dth = "DTH";

        public const string Dtn = "DTN";

        public const string Dwt = "DWT";

        public const string Dzn = "DZN";

        public const string Dzp = "DZP";

        public const string Dzr = "DZR";

        public const string Ea = "EA";

        public const string Eac = "EAC";

        public const string Fah = "FAH";

        public const string Far = "FAR";

        public const string Fot = "FOT";

        public const string Ftk = "FTK";

        public const string Ftq = "FTQ";

        public const string Gbq = "GBQ";

        public const string Gfi = "GFI";

        public const string Ggr = "GGR";

        public const string Gii = "GII";

        public const string Gld = "GLD";

        public const string Gli = "GLI";

        public const string Gll = "GLL";

        public const string Grm = "GRM";

        public const string Grn = "GRN";

        public const string Gro = "GRO";

        public const string Grt = "GRT";

        public const string Gwh = "GWH";

        public const string Har = "HAR";

        public const string Hba = "HBA";

        public const string Hgm = "HGM";

        public const string Hiu = "HIU";

        public const string Hlt = "HLT";

        public const string Hmq = "HMQ";

        public const string Hmt = "HMT";

        public const string Hpa = "HPA";

        public const string Htz = "HTZ";

        public const string Hur = "HUR";

        public const string Inh = "INH";

        public const string Ink = "INK";

        public const string Inq = "INQ";

        public const string Itm = "ITM";

        public const string Jou = "JOU";

        public const string Kba = "KBA";

        public const string Kel = "KEL";

        public const string Kgm = "KGM";

        public const string Kgs = "KGS";

        public const string Khz = "KHZ";

        public const string Kjo = "KJO";

        public const string Kmh = "KMH";

        public const string Kmk = "KMK";

        public const string Kmq = "KMQ";

        public const string Kmt = "KMT";

        public const string Kni = "KNI";

        public const string Kns = "KNS";

        public const string Knt = "KNT";

        public const string Kpa = "KPA";

        public const string Kph = "KPH";

        public const string Kpo = "KPO";

        public const string Kpp = "KPP";

        public const string Ksd = "KSD";

        public const string Ksh = "KSH";

        public const string Ktn = "KTN";

        public const string Kur = "KUR";

        public const string Kva = "KVA";

        public const string Kvr = "KVR";

        public const string Kvt = "KVT";

        public const string Kwh = "KWH";

        public const string Kwt = "KWT";

        public const string Lbr = "LBR";

        public const string Lbs = "LBS";

        public const string Lef = "LEF";

        public const string Lpa = "LPA";

        public const string Ltn = "LTN";

        public const string Ltr = "LTR";

        public const string Lum = "LUM";

        public const string Lux = "LUX";

        public const string Mal = "MAL";

        public const string Mam = "MAM";

        public const string Maw = "MAW";

        public const string Mbe = "MBE";

        public const string Mbf = "MBF";

        public const string Mbr = "MBR";

        public const string Mcu = "MCU";

        public const string Mgm = "MGM";

        public const string Mhz = "MHZ";

        public const string Mik = "MIK";

        public const string Mil = "MIL";

        public const string Min = "MIN";

        public const string Mio = "MIO";

        public const string Miu = "MIU";

        public const string Mld = "MLD";

        public const string Mlt = "MLT";

        public const string Mmk = "MMK";

        public const string Mmq = "MMQ";

        public const string Mmt = "MMT";

        public const string Mon = "MON";

        public const string Mpa = "MPA";

        public const string Mqh = "MQH";

        public const string Mqs = "MQS";

        public const string Msk = "MSK";

        public const string Mtk = "MTK";

        public const string Mtq = "MTQ";

        public const string Mtr = "MTR";

        public const string Mts = "MTS";

        public const string Mva = "MVA";

        public const string Mwh = "MWH";

        public const string Nar = "NAR";

        public const string Nbb = "NBB";

        public const string Ncl = "NCL";

        public const string New = "NEW";

        public const string Niu = "NIU";

        public const string Nmb = "NMB";

        public const string Nmi = "NMI";

        public const string Nmp = "NMP";

        public const string Nmr = "NMR";

        public const string Npl = "NPL";

        public const string Npt = "NPT";

        public const string Nrl = "NRL";

        public const string Ntt = "NTT";

        public const string Ohm = "OHM";

        public const string Onz = "ONZ";

        public const string Oza = "OZA";

        public const string Ozi = "OZI";

        public const string Pal = "PAL";

        public const string Pcb = "PCB";

        public const string Pce = "PCE";

        public const string Pgl = "PGL";

        public const string Pk = "PK";

        public const string Psc = "PSC";

        public const string Ptd = "PTD";

        public const string Pti = "PTI";

        public const string Ptl = "PTL";

        public const string Qan = "QAN";

        public const string Qtd = "QTD";

        public const string Qti = "QTI";

        public const string Qtl = "QTL";

        public const string Qtr = "QTR";

        public const string Rpm = "RPM";

        public const string Rps = "RPS";

        public const string San = "SAN";

        public const string Sco = "SCO";

        public const string Scr = "SCR";

        public const string Sec = "SEC";

        public const string Set = "SET";

        public const string Sht = "SHT";

        public const string Sie = "SIE";

        public const string Smi = "SMI";

        public const string Sst = "SST";

        public const string St = "ST";

        public const string Sti = "STI";

        public const string Tah = "TAH";

        public const string Tne = "TNE";

        public const string Tpr = "TPR";

        public const string Tqd = "TQD";

        public const string Trl = "TRL";

        public const string Tsd = "TSD";

        public const string Tsh = "TSH";

        public const string Vlt = "VLT";

        public const string Wcd = "WCD";

        public const string Web = "WEB";

        public const string Wee = "WEE";

        public const string Whr = "WHR";

        public const string Wsd = "WSD";

        public const string Wtt = "WTT";

        public const string Ydk = "YDK";

        public const string Ydq = "YDQ";
    }
}
