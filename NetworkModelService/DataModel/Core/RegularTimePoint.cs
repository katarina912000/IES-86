using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class RegularTimePoint : IdentifiedObject
    {
        private int seqNum;
        private float val1;
        private float val2;
        private long intervalSchedule;
        public RegularTimePoint(long globalId) : base(globalId)
        {

        }

        public int SeqNum { get => seqNum; set => seqNum = value; }
        public float Val1 { get => val1; set => val1 = value; }
        public float Val2 { get => val2; set => val2 = value; }
        public long IntervalSchedule { get => intervalSchedule; set => intervalSchedule = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegularTimePoint x = (RegularTimePoint)obj;
                return (x.seqNum == this.seqNum && x.val1 == this.val1 && x.val2 == this.val2 &&
                        x.intervalSchedule == this.intervalSchedule);
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
                case ModelCode.REGULARTIMEPOINT_SEQNUM:
                case ModelCode.REGULARTIMEPOINT_VAL1:
                case ModelCode.REGULARTIMEPOINT_VAL2:
                case ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.REGULARTIMEPOINT_SEQNUM:
                    prop.SetValue(seqNum);
                    break;

                case ModelCode.REGULARTIMEPOINT_VAL1:
                    prop.SetValue(val1);
                    break;

                case ModelCode.REGULARTIMEPOINT_VAL2:
                    prop.SetValue(val2);
                    break;

                case ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE:
                    prop.SetValue(intervalSchedule);
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
                case ModelCode.REGULARTIMEPOINT_SEQNUM:
                    seqNum = property.AsInt();
                    break;

                case ModelCode.REGULARTIMEPOINT_VAL1:
                    val1 = property.AsFloat();
                    break;

                case ModelCode.REGULARTIMEPOINT_VAL2:
                    val2 = property.AsFloat();
                    break;

                case ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE:
                    intervalSchedule = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (intervalSchedule != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE] = new List<long>();
                references[ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE].Add(intervalSchedule);
            }
        }

        #endregion IReference implementation
    }
}
