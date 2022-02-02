using System;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
    public class MaintenanceTaskSearch
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public DateTime IssueDateFrom { get; set; }
        public DateTime IssueDateTo { get; set; }
        public string DescriptionIncludes { get; set; }
        public ImportanceLevel ImportanceMin { get; set; }
        public ImportanceLevel ImportanceMax { get; set; }
        public bool? Closed { get; set; }
    }
}