﻿

namespace PEI_ETL.Services.DTO
{
    public class ETLBatchSrcDTO
    {
        public string Batch_Name { get; set; }
        public string Batch_Type { get; set; }
        public string Source_Type { get; set; }
        public int Src_Extract_Seq { get; set; }

        public string Src_PK_String { get; set; }

        public string Source_Server { get; set; }
    }
}