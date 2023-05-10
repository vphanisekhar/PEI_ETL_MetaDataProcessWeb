using AutoMapper;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Interfaces;

namespace PEI_ETL.Services.Service
{

    public class ETLBatchService:IETLBatchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ETLBatchService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ETLBatchDTO>> GetETLBatchAsync()
        {
            var eTLBatch = await _unitOfWork.ETLBatch.GetAll();

            //Active records will in null or active
            var eTLBatchActive = eTLBatch.Where(x=>x.IsActive != false).ToList();

            return _mapper.Map<IEnumerable<ETLBatchDTO>>(eTLBatchActive);
        }

        public async Task<bool> InsertAsync(ETLBatchDTO eTLBatchDTO)
        {
            eTLBatchDTO.CreatedDate = DateTime.UtcNow;
            var eTLBatch = _mapper.Map<ETLBatch>(eTLBatchDTO);
            return await _unitOfWork.ETLBatch.Add(eTLBatch);
        }

        public async Task<bool> UpdateETLBatch(ETLBatchDTO eTLBatchDTO)
        {
            if (eTLBatchDTO != null)
            {
                var eTLBatchDetails = await _unitOfWork.ETLBatch.GetById(eTLBatchDTO.Id);
                if (eTLBatchDetails != null)
                {
                    eTLBatchDetails.Batch_Name = eTLBatchDTO.Batch_Name;
                    eTLBatchDetails.Batch_Type = eTLBatchDTO.Batch_Type;
                    eTLBatchDetails.CDCPK_EXT_PATH = eTLBatchDTO.CDCPK_EXT_PATH;
                    eTLBatchDetails.ORA_EXT_DIR = eTLBatchDTO.ORA_EXT_DIR;
                    eTLBatchDetails.CDCPK_POINTER_TABLE = eTLBatchDTO.CDCPK_POINTER_TABLE;
                    eTLBatchDetails.CDC_SERVICE_NAME = eTLBatchDTO.CDC_SERVICE_NAME;
                    eTLBatchDetails.CDCPK_STRING = eTLBatchDTO.CDCPK_STRING;
                    eTLBatchDetails.UpdatedDate = DateTime.UtcNow;
                    eTLBatchDetails.UpdatedBy = eTLBatchDTO.UpdatedBy;
                    //eTLBatchSrcDetails.IsActive = eTLBatchSrc.IsActive;

                    _unitOfWork.ETLBatch.Upsert(eTLBatchDetails);
                    return true;

                    //var result = _unitOfWork.Save();

                    //if (result > 0)
                    //    return true;
                    //else
                    //    return false;
                }
            }
            return false;
        }
            
        public async Task<bool> DeleteETLBatch(int Id)
        {
            if (Id != 0)
            {
                var eTLBatchDetails = await _unitOfWork.ETLBatch.GetById(Id);
                if (eTLBatchDetails != null)
                {                    
                    eTLBatchDetails.IsActive = false;

                    _unitOfWork.ETLBatch.Upsert(eTLBatchDetails);
                    return true;

                    //var result = _unitOfWork.Save();

                    //if (result > 0)
                    //    return true;
                    //else
                    //    return false;
                }
            }
            return false;
        }

        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }
}
