using System.ComponentModel.DataAnnotations;

namespace PEI_ETL.Core.Entities
{
    public class ETLJobs : AuditColumns
    {
        public int Id { get; set; }

        public string Job_Name { get; set; }
        public string Batch_Name { get; set; }
        public string Batch_Step { get; set; }

        public decimal Job_Step_Seq_No { get; set; }

        public decimal Job_Stage { get; set; }

        public char Is_Step_Active { get; set; }
    }
}
