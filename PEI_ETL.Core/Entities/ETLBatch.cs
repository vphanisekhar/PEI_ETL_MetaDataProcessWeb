namespace PEI_ETL.Core.Entities
{
    public class ETLBatch: AuditColumns
    {
        public int Id { get; set; }
        public string Batch_Name { get; set; }
        public string? Batch_Type { get; set; }
        public string CDCPK_POINTER_TABLE { get; set; }

        public string? CDCPK_STRING { get; set; }
        //public int Src_Extract_Seq { get; set; }

        public string? CDCPK_EXT_PATH { get; set; }

        public string CDC_SERVICE_NAME { get; set; }
                
        public string? ORA_EXT_DIR { get; set; }

        public bool IsActive { get; set; }

    }
}
