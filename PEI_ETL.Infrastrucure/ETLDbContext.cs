using Microsoft.EntityFrameworkCore;
using PEI_ETL.Core.Entities;


namespace PEI_ETL.Infrastrucure
{

    public class ETLDbContext : DbContext 
    {
        public ETLDbContext(DbContextOptions<ETLDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<ProductDetails> Products { get; set; }

        public virtual DbSet<ETLBatchSrc> ETL_BATCH_SRC { get; set; }

        public virtual DbSet<ETLBatch> ETL_BATCH { get; set; }

        public virtual DbSet<ETLBatchSrcStep> ETL_BATCH_SRC_STEP { get; set; }

        public virtual DbSet<ETLBatchStepCfg> ETL_BATCH_STEP_CFG { get; set; }

        public virtual DbSet<ETLJobs> ETL_JOBS { get; set; }

        public virtual DbSet<ETLBatchSrcStepCfg> ETL_BATCH_SRC_STEP_CFG { get; set; }
    }
}