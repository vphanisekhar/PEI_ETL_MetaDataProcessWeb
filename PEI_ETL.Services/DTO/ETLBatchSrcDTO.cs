

using PEI_ETL.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace PEI_ETL.Services.DTO
{
    public class ETLBatchSrcDTO : AuditColumns
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Batch name is required!")]
        [MaxLength(255)]
        public string? Batch_Name { get; set; }
        public string? Batch_Type { get; set; }
        public string? Source_Type { get; set; }

        [MaxLength(1000)]
        public string? Source_Name { get; set; }

        public string? Source_Id { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number for Src_Extract_Seq!")]
        public int Src_Extract_Seq { get; set; }

        [MaxLength(2000)]
        public string? Src_PK_String { get; set; }

        public string? Source_Server { get; set; }

        public bool IsActive { get; set; }
    }
}
