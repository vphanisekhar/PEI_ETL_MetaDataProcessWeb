namespace PEI_ETL.Core.Entities
{
    public class ETLBatchSrcStepCfg : AuditColumns
    {
        public int Id { get; set; }
        public string Batch_Name { get; set; }

        public string Source_Id { get; set; }
        public string Batch_Step { get; set; }
        public string Src_Tgt_Type { get; set; }

        public string Parameter { get; set; }
       
        public string? Value { get; set; }

        public bool IsActive { get; set; }

    }
}
