using AutoMapper;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.Service
{

    public class ETLBatchSrcService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ETLBatchSrcService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ETLBatchSrcDTO>> GetETLBatchSrcAsync()
        {
            var eTLBatchSrcs = await _unitOfWork.ETLBatchSrc.GetAll();

            return _mapper.Map<IEnumerable<ETLBatchSrcDTO>>(eTLBatchSrcs);
        }

        public async Task<bool> InsertAsync(ETLBatchSrcDTO eTLBatchSrcDTO)
        {
            var eTLBatchSrc = _mapper.Map<ETLBatchSrc>(eTLBatchSrcDTO);
            return await _unitOfWork.ETLBatchSrc.Add(eTLBatchSrc);
        }

        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }
}
