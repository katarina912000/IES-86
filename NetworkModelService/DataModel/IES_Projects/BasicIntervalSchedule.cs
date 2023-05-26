using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        private DateTime startTime;
        private UnitMultiplier value1Multiplier;
        private UnitMultiplier value2Multiplier;
        private UnitSymbol value1Unit;
        private UnitSymbol value2Unit;
        public BasicIntervalSchedule(long globalId) : base(globalId)
        {

        }

        public DateTime StartDate { get => startTime; set => startTime = value; }
        public UnitMultiplier Value1Multiplier { get => value1Multiplier; set => value1Multiplier = value; }
        public UnitMultiplier Value2Multiplier { get => value2Multiplier; set => value2Multiplier = value; }
        public UnitSymbol Value1Unit { get => value1Unit; set => value1Unit = value; }
        public UnitSymbol Value2Unit { get => value2Unit; set => value2Unit = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                BasicIntervalSchedule x = (BasicIntervalSchedule)obj;
                return (x.startTime == this.startTime && x.value1Multiplier == this.value1Multiplier &&
                        x.value1Unit == this.value1Unit && x.value2Multiplier == this.value2Multiplier &&
                        x.value2Unit == this.value2Unit);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                case ModelCode.BASICINTERVALSCHEDULE_VAL1MULTI:
                case ModelCode.BASICINTERVALSCHEDULE_VAL1UNIT:
                case ModelCode.BASICINTERVALSCHEDULE_VAL2MULTI:
                case ModelCode.BASICINTERVALSCHEDULE_VAL2UNIT:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                    prop.SetValue(startTime);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL1MULTI:
                    prop.SetValue((short)value1Multiplier);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL1UNIT:
                    prop.SetValue((short)value1Unit);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL2MULTI:
                    prop.SetValue((short)value2Multiplier);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL2UNIT:
                    prop.SetValue((short)value2Unit);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                    startTime = property.AsDateTime();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL1MULTI:
                    value1Multiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL1UNIT:
                    value1Unit = (UnitSymbol)property.AsEnum();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL2MULTI:
                    value2Multiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VAL2UNIT:
                    value2Unit = (UnitSymbol)property.AsEnum();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}
