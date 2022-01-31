using System;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
    public class MaintenanceTaskDto
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public DateTime IssueDate { get; set; }
        public string Description { get; set; }
        public ImportanceLevel Importance { get; set; }
        public bool Closed { get; set; }
    }
}