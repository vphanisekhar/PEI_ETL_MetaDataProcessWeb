namespace PEI_ETL.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }

        IProductRepository Products { get; }

        IETLBatchSrcRepository ETLBatchSrc { get; }

        IETLBatchRepository ETLBatch { get; }

        IETLBatchSrcStepRepository ETLBatchSrcStep { get; }

        Task<int> CompletedAsync();
    }
}
