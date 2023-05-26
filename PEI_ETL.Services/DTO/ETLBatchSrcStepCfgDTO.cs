

using PEI_ETL.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace PEI_ETL.Services.DTO
{
    public class ETLBatchSrcStepCfgDTO : AuditColumns
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Batch Name is required!")]
        [MaxLength(255)]
        public string Batch_Name { get; set; }

        [Required(ErrorMessage = "Source Id is required!")]
        [MaxLength(50)]
        public string Source_Id { get; set; }


        [Required(ErrorMessage = "Batch Step is required!")]
        [MaxLength(20)]
        public string Batch_Step { get; set; }


        [Required(ErrorMessage = "Src_Tgt_Type is required!")]
        [MaxLength(50)]
        public string Src_Tgt_Type { get; set; }


        [Required(ErrorMessage = "Parameter is required!")]
        [MaxLength(50)]
        public string Parameter { get; set; }

        public string? Value { get; set; }

        public bool IsActive { get; set; }

    }

}

