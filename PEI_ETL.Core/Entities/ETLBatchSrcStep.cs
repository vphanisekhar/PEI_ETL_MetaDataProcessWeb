﻿namespace PEI_ETL.Core.Entities
{
    public class ETLBatchSrcStep: AuditColumns
    {
        public int Id { get; set; }
        public string? Batch_Name { get; set; }
        public string? Source_Id { get; set; }
        public string? Batch_Step { get; set; }

        public string? Src_Tgt_Type { get; set; }
        public string? Src_Tgt_Name { get; set; }

        public string? Source_Step { get; set; }

        public string? Source_Step_Prgm { get; set; }

        public bool IsActive { get; set; }

    }
}
