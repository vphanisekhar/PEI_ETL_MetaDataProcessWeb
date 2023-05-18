

using PEI_ETL.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace PEI_ETL.Services.DTO
{
    public class ETLBatchSrcStepDTO : AuditColumns
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Batch name is required!")]
        [MaxLength(255)]
        public string? Batch_Name { get; set; }

        [Required(ErrorMessage = "Source Id is required!")]
        [MaxLength(50)]
        public string? Source_Id { get; set; }

        [Required(ErrorMessage = "Batch Step is required!")]
        [MaxLength(20)]
        public string? Batch_Step { get; set; }

        [Required(ErrorMessage = "Src_Tgt_Type is required!")]
        [MaxLength(50)]
        public string? Src_Tgt_Type { get; set; }

        [Required(ErrorMessage = "Src_Tgt_Name is required!")]
        [MaxLength(200)]
        public string? Src_Tgt_Name { get; set; }

        [Required(ErrorMessage = "Source Step is required!")]
        [MaxLength(50)]
        public string? Source_Step { get; set; }

        public string? Source_Step_Prgm { get; set; }

        public bool IsActive { get; set; }


    }
}
