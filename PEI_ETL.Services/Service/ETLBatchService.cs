using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ETLBatchService> _logger;
 

        public ETLBatchService(
            IUnitOfWork unitOfWork,
            IMapper mapper, ILogger<ETLBatchService> logger
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
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
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(InsertAsync),
          eTLBatchDTO);


            eTLBatchDTO.CreatedDate = DateTime.UtcNow;
            var eTLBatch = _mapper.Map<ETLBatch>(eTLBatchDTO);
            _logger.LogInformation($"Datetime for creating the object! is {eTLBatchDTO.CreatedDate}");
            return await _unitOfWork.ETLBatch.Add(eTLBatch);
        }

        public async Task<bool> UpdateETLBatch(ETLBatchDTO eTLBatchDTO)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(UpdateETLBatch),
         eTLBatchDTO);

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

                    _logger.LogInformation($"Datetime for updating the object! is {eTLBatchDetails.UpdatedDate}");

                    _unitOfWork.ETLBatch.Upsert(eTLBatchDetails);
                    return true;

                }
            }
            return false;
        }
            
        public async Task<bool> DeleteETLBatch(int Id)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(DeleteETLBatch),
       Id);

            if (Id != 0)
            {
                var eTLBatchDetails = await _unitOfWork.ETLBatch.GetById(Id);
                if (eTLBatchDetails != null)
                {                    
                    eTLBatchDetails.IsActive = false;

                    _unitOfWork.ETLBatch.Upsert(eTLBatchDetails);
                    _logger.LogInformation($"Deleted the record!");


                    return true;

                }
            }
            _logger.LogInformation($"Failed while deleting the record!");
            return false;
        }

        public async Task<IEnumerable<ETLBatchDTO>> GetETLBatchByBatchNameAsync(string batchName)
        {
            var eTLBatch = await _unitOfWork.ETLBatch.GetAll();

            //Active records will in null or active
            var eTLBatchSrcsActive = eTLBatch.Where(x => x.IsActive != false && x.Batch_Name == batchName).ToList();

            return _mapper.Map<IEnumerable<ETLBatchDTO>>(eTLBatchSrcsActive);
        }

        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }
}
