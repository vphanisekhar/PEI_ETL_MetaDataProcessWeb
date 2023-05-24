

using PEI_ETL.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace PEI_ETL.Services.DTO
{
    public class ETLJobsDTO : AuditColumns
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Job name is required!")]
        [MaxLength(255)]
        public string Job_Name { get; set; }

        [Required(ErrorMessage = "Batch name is required!")]
        [MaxLength(255)]
        public string Batch_Name { get; set; }

        [Required(ErrorMessage = "Batch step is required!")]
        [MaxLength(20)]
        public string Batch_Step { get; set; }

        [Required(ErrorMessage = "Batch Step Seq no is required!")]
        public int Job_Step_Seq_No { get; set; }

        [Required(ErrorMessage = "Batch stage is required!")]
        public int Job_Stage { get; set; }

        [Required(ErrorMessage = "Step Active is required!")]
        public char Is_Step_Active { get; set; }
    }
}
