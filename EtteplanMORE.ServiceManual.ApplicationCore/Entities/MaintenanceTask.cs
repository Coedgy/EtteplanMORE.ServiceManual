﻿using System;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
    public class MaintenanceTask
    {
        public int Id { get; set; }
        public int Device { get; set; }
        public DateTime IssueDate { get; set; }
        public string Description { get; set; }
        public ImportanceLevel Importance { get; set; }
        public bool Closed { get; set; }
    }

    public enum ImportanceLevel
    {
        Mild,
        Important,
        Critical
    }
}