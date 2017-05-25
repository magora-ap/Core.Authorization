using Core.Authorization.Common.Models.Request.Timezone;

namespace Core.Authorization.Common.Models.Request.Meta
{
    public class MetaInfo
    {
        public string DeviceId { get; set; }
        public string VersionApp { get; set; }
        public string PushDeviceId { get; set; }
        public string Platform { get; set; }

        public TimeOffsetRequestModel TimeOffset { get; set; }
    }
}
