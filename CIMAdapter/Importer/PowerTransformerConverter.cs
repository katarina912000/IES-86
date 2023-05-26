namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    using FTN.Common;

    /// <summary>
    /// PowerTransformerConverter has methods for populating
    /// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
    /// </summary>
    public static class PowerTransformerConverter
    {

        #region Populate ResourceDescription
        public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
        {
            if ((cimIdentifiedObject != null) && (rd != null))
            {
                if (cimIdentifiedObject.MRIDHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
                }
                if (cimIdentifiedObject.NameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
                }
                if (cimIdentifiedObject.AliasNameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_ALIAS, cimIdentifiedObject.AliasName));
                }
            }
        }

        public static void PopulateDayTypeProperties(FTN.DayType cimConnectivityNode, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimConnectivityNode != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimConnectivityNode, rd);
            }
        }

        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPowerSystemResource != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
            }
        }

        public static void PopulateRegularTimePointProperties(FTN.RegularTimePoint cimRegularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegularTimePoint != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimRegularTimePoint, rd);

                if (cimRegularTimePoint.SequenceNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_SEQNUM, cimRegularTimePoint.SequenceNumber));

                }
                if (cimRegularTimePoint.Value1HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VAL1, cimRegularTimePoint.Value1));

                }
                if (cimRegularTimePoint.Value2HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VAL2, cimRegularTimePoint.Value2));

                }
                if (cimRegularTimePoint.IntervalScheduleHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegularTimePoint.IntervalSchedule.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegularTimePoint.GetType().ToString()).Append(" rdfID = ").Append(cimRegularTimePoint.ID);
                        report.Report.Append(" - Failed to set reference to IntervalSchedule: rdfID ").Append(cimRegularTimePoint.IntervalSchedule.ID).AppendLine("is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE, gid));
                }
            }
        }

        public static void PopulateBasicIntervalScheduleProperties(FTN.BasicIntervalSchedule cimBasicIntervalSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimBasicIntervalSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);

                if (cimBasicIntervalSchedule.StartTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_STARTTIME, cimBasicIntervalSchedule.StartTime));
                }
                if (cimBasicIntervalSchedule.Value1MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VAL1MULTI, (short)GetUnitMultiplier(cimBasicIntervalSchedule.Value1Multiplier)));
                }
                if (cimBasicIntervalSchedule.Value1UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VAL1UNIT, (short)GetUnitSymbol(cimBasicIntervalSchedule.Value1Unit)));
                }
                if (cimBasicIntervalSchedule.Value2MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VAL2MULTI, (short)GetUnitMultiplier(cimBasicIntervalSchedule.Value2Multiplier)));
                }
                if (cimBasicIntervalSchedule.Value2UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VAL2UNIT, (short)GetUnitSymbol(cimBasicIntervalSchedule.Value2Unit)));
                }

            }
        }

        public static void PopulateRegularIntervalScheduleProperties(FTN.RegularIntervalSchedule cimRegularIntervalSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegularIntervalSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateBasicIntervalScheduleProperties(cimRegularIntervalSchedule, rd, importHelper, report);
            }
        }

        public static void PopulateSeasonDayTypeScheduleProperties(FTN.SeasonDayTypeSchedule cim, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cim != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateRegularIntervalScheduleProperties(cim, rd, importHelper, report);
                if (cim.DayTypeHasValue)
                {
                    long gid = importHelper.GetMappedGID(cim.DayType.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cim.GetType().ToString()).Append(" rdfID = ").Append(cim.ID);
                        report.Report.Append("- Failed to set reference to DayType: rdfID ").Append(cim.DayType.ID).AppendLine(" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE, gid));
                }
            }
        }

        public static void PopulateRegulationScheduleProperties(FTN.RegulationSchedule cimRegulationSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegulationSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateSeasonDayTypeScheduleProperties(cimRegulationSchedule, rd, importHelper, report);
                if (cimRegulationSchedule.RegulatingControlHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegulationSchedule.RegulatingControl.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegulationSchedule.GetType().ToString()).Append(" rdfID = ").Append(cimRegulationSchedule.ID);
                        report.Report.Append("- Failed to set reference to RegulationControl: rdfID ").Append(cimRegulationSchedule.RegulatingControl.ID).AppendLine(" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGULATIONSCHEDULE_RC, gid));
                }
            }
        }

        public static void PopulateTapScheduleProperties(FTN.TapSchedule cimTapSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTapSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateSeasonDayTypeScheduleProperties(cimTapSchedule, rd, importHelper, report);
                if (cimTapSchedule.TapChangerHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTapSchedule.TapChanger.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTapSchedule.GetType().ToString()).Append(" rdfID = ").Append(cimTapSchedule.ID);
                        report.Report.Append("- Failed to set reference to TapChanger: rdfID ").Append(cimTapSchedule.TapChanger.ID).AppendLine(" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TAPSCHEDULE_TAPCHANGER, gid));
                }
            }
        }

        public static void PopulateTapChangerProperties(FTN.TapChanger cimTapChanger, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTapChanger != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimTapChanger, rd, importHelper, report);
            }
        }
        public static void PopulateRegulatingControlProperties(FTN.RegulatingControl cimRegulatingControl, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegulatingControl != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimRegulatingControl, rd, importHelper, report);

                if (cimRegulatingControl.DiscreteHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_DISCRETE, cimRegulatingControl.Discrete));

                }
                if (cimRegulatingControl.ModeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_MODE, (short)GetModeKind(cimRegulatingControl.Mode)));

                }
                if (cimRegulatingControl.MonitoredPhaseHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_MONITOREDPHASE, (short)GetDMSPhaseCode(cimRegulatingControl.MonitoredPhase)));

                }
                if (cimRegulatingControl.TargetRangeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TARGETRANGE, cimRegulatingControl.TargetRange));

                }
                if (cimRegulatingControl.TargetValueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULATINGCONTROL_TARGETVALUE, cimRegulatingControl.TargetValue));

                }
            }
        }

        public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);
                if (cimEquipment.AggregateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));

                }
                if (cimEquipment.NormallyInServiceHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYINSERVICE, cimEquipment.NormallyInService));

                }
            }
        }

        public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConduct, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimConduct != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateEquipmentProperties(cimConduct, rd, importHelper, report);
            }
        }

        public static void PopulateSwitchProperties(FTN.Switch cimSwitch, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSwitch != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSwitch, rd, importHelper, report);

                if (cimSwitch.NormalOpen)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_NORMALOPEN, cimSwitch.NormalOpen));

                }
                if (cimSwitch.RetainedHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_RETAINED, cimSwitch.Retained));

                }
                if (cimSwitch.SwitchOnCountHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONCOUNT, cimSwitch.SwitchOnCount));

                }
                if (cimSwitch.SwitchOnDateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONDATE, cimSwitch.SwitchOnDate));

                }
            }
        }

        #endregion Populate ResourceDescription

        #region Enums convert
        public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
        {
            switch (phases)
            {
                case FTN.PhaseCode.A:
                    return PhaseCode.A;
                case FTN.PhaseCode.AB:
                    return PhaseCode.AB;
                case FTN.PhaseCode.ABC:
                    return PhaseCode.ABC;
                case FTN.PhaseCode.ABCN:
                    return PhaseCode.ABCN;
                case FTN.PhaseCode.ABN:
                    return PhaseCode.ABN;
                case FTN.PhaseCode.AC:
                    return PhaseCode.AC;
                case FTN.PhaseCode.ACN:
                    return PhaseCode.ACN;
                case FTN.PhaseCode.AN:
                    return PhaseCode.AN;
                case FTN.PhaseCode.B:
                    return PhaseCode.B;
                case FTN.PhaseCode.BC:
                    return PhaseCode.BC;
                case FTN.PhaseCode.BCN:
                    return PhaseCode.BCN;
                case FTN.PhaseCode.BN:
                    return PhaseCode.BN;
                case FTN.PhaseCode.C:
                    return PhaseCode.C;
                case FTN.PhaseCode.CN:
                    return PhaseCode.CN;
                case FTN.PhaseCode.N:
                    return PhaseCode.N;
                case FTN.PhaseCode.s12N:
                    return PhaseCode.ABN;
                case FTN.PhaseCode.s1N:
                    return PhaseCode.AN;
                case FTN.PhaseCode.s2N:
                    return PhaseCode.BN;
                default: return PhaseCode.Unknown;
            }
        }

        public static UnitMultiplier GetUnitMultiplier(FTN.UnitMultiplier multiplier)
        {
            switch (multiplier)
            {
                case FTN.UnitMultiplier.G:
                    return UnitMultiplier.G;
                case FTN.UnitMultiplier.M:
                    return UnitMultiplier.M;
                case FTN.UnitMultiplier.T:
                    return UnitMultiplier.T;
                case FTN.UnitMultiplier.c:
                    return UnitMultiplier.c;
                case FTN.UnitMultiplier.d:
                    return UnitMultiplier.d;
                case FTN.UnitMultiplier.k:
                    return UnitMultiplier.m;
                case FTN.UnitMultiplier.micro:
                    return UnitMultiplier.micro;
                case FTN.UnitMultiplier.n:
                    return UnitMultiplier.n;
                case FTN.UnitMultiplier.p:
                    return UnitMultiplier.p;
                case FTN.UnitMultiplier.none:
                    return UnitMultiplier.none;
                default: return UnitMultiplier.none;
            }
        }

        public static UnitSymbol GetUnitSymbol(FTN.UnitSymbol symbol)
        {
            switch (symbol)
            {
                case FTN.UnitSymbol.A:
                    return UnitSymbol.A;

                case FTN.UnitSymbol.deg:
                    return UnitSymbol.deg;

                case FTN.UnitSymbol.degC:
                    return UnitSymbol.degC;

                case FTN.UnitSymbol.F:
                    return UnitSymbol.F;

                case FTN.UnitSymbol.g:
                    return UnitSymbol.g;

                case FTN.UnitSymbol.h:
                    return UnitSymbol.h;

                case FTN.UnitSymbol.H:
                    return UnitSymbol.H;

                case FTN.UnitSymbol.Hz:
                    return UnitSymbol.Hz;

                case FTN.UnitSymbol.J:
                    return UnitSymbol.J;

                case FTN.UnitSymbol.m:
                    return UnitSymbol.m;

                case FTN.UnitSymbol.m2:
                    return UnitSymbol.m2;

                case FTN.UnitSymbol.m3:
                    return UnitSymbol.m3;

                case FTN.UnitSymbol.min:
                    return UnitSymbol.min;

                case FTN.UnitSymbol.N:
                    return UnitSymbol.N;

                case FTN.UnitSymbol.none:
                    return UnitSymbol.none;

                case FTN.UnitSymbol.ohm:
                    return UnitSymbol.ohm;

                case FTN.UnitSymbol.Pa:
                    return UnitSymbol.Pa;

                case FTN.UnitSymbol.rad:
                    return UnitSymbol.rad;

                case FTN.UnitSymbol.s:
                    return UnitSymbol.s;

                case FTN.UnitSymbol.S:
                    return UnitSymbol.S;

                case FTN.UnitSymbol.V:
                    return UnitSymbol.V;

                case FTN.UnitSymbol.VA:
                    return UnitSymbol.VA;

                case FTN.UnitSymbol.VAh:
                    return UnitSymbol.VAh;

                case FTN.UnitSymbol.VAr:
                    return UnitSymbol.VAr;

                case FTN.UnitSymbol.VArh:
                    return UnitSymbol.VArh;

                case FTN.UnitSymbol.W:
                    return UnitSymbol.W;

                case FTN.UnitSymbol.Wh:
                    return UnitSymbol.Wh;
                default:
                    return UnitSymbol.none;
            }
        }
        public static RegulatingControlModeKind GetModeKind(FTN.RegulatingControlModeKind modeKind)
        {
            switch (modeKind)
            {
                case RegulatingControlModeKind.activePower:
                    return RegulatingControlModeKind.activePower;

                case RegulatingControlModeKind.admittance:
                    return RegulatingControlModeKind.admittance;

                case RegulatingControlModeKind.currentFlow:
                    return RegulatingControlModeKind.currentFlow;

               
                case RegulatingControlModeKind.powerFactor:
                    return RegulatingControlModeKind.powerFactor;

                case RegulatingControlModeKind.reactivePower:
                    return RegulatingControlModeKind.reactivePower;

                case RegulatingControlModeKind.temperature:
                    return RegulatingControlModeKind.temperature;

                case RegulatingControlModeKind.timeScheduled:
                    return RegulatingControlModeKind.timeScheduled;

                case RegulatingControlModeKind.voltage:
                    return RegulatingControlModeKind.voltage;

                default:
                    return RegulatingControlModeKind.voltage;

            }
        }

        #endregion Enums convert
    }
}
