namespace MS.WebCore
{
    public class SiteSetting
    {
        public long WorkerId { get; set; }
        public long DataCenterId { get; set; }
        public int LoginFailedCountLimits { get; set; }
        public int LoginLockedTimeout { get; set; }
    }
}
