using System;

namespace FTN.Common
{
    public enum UnitMultiplier : short
    {
        G,
        M,
        T,
        c,
        d,
        k,
        m,
        micro,
        n,
        none,
        p
    }
    public enum PhaseCode : short
    {
        Unknown = 0x0,
        N = 0x1,
        C = 0x2,
        CN = 0x3,
        B = 0x4,
        BN = 0x5,
        BC = 0x6,
        BCN = 0x7,
        A = 0x8,
        AN = 0x9,
        AC = 0xA,
        ACN = 0xB,
        AB = 0xC,
        ABN = 0xD,
        ABC = 0xE,
        ABCN = 0xF
    }
    public enum UnitSymbol : short
    {
        A,
        F,
        H,
        Hz,
        J,
        N,
        Pa,
        S,
        V,
        VA,
        VAh,
        VAr,
        VArh,
        W,
        Wh,
        deg,
        degC,
        g,
        h,
        m,
        m2,
        m3,
        min,
        none,
        ohm,
        rad,
        s
    }

    public enum RegulatingControlModelKind : short
    {
        activePower,
        admittance,
        currentFlow,
        Fixed,
        powerFactor,
        reactivePower,
        temperature,
        timeScheduled,
        voltage
    }

}
