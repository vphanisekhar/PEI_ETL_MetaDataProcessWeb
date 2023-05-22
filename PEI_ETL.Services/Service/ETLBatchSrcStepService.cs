using AutoMapper;
using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Interfaces;

namespace PEI_ETL.Services.Service
{

    public class ETLBatchSrcStepService:IETLBatchSrcStepService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ETLBatchSrcStepService> _logger;
        public ETLBatchSrcStepService(
            IUnitOfWork unitOfWork,
            IMapper mapper, ILogger<ETLBatchSrcStepService> logger
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ETLBatchSrcStepDTO>> GetETLBatchSrcStepAsync()
        {
            var ETLBatchStep = await _unitOfWork.ETLBatchSrcStep.GetAll();

            //Active records will in null or active
            var ETLBatchStepActive = ETLBatchStep.Where(x=>x.IsActive != false).ToList();

            return _mapper.Map<IEnumerable<ETLBatchSrcStepDTO>>(ETLBatchStepActive);
        }

        public async Task<bool> InsertAsync(ETLBatchSrcStepDTO ETLBatchStepDTO)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(InsertAsync),
         ETLBatchStepDTO);

            ETLBatchStepDTO.CreatedDate = DateTime.UtcNow;
            var ETLBatchStep = _mapper.Map<ETLBatchSrcStep>(ETLBatchStepDTO);

            _logger.LogInformation($"Datetime for creating the object! is {ETLBatchStepDTO.CreatedDate}");


            return await _unitOfWork.ETLBatchSrcStep.Add(ETLBatchStep);
        }

        public async Task<bool> UpdateETLBatchSrcStep(ETLBatchSrcStepDTO ETLBatchStepDTO)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(UpdateETLBatchSrcStep),
         ETLBatchStepDTO);

            if (ETLBatchStepDTO != null)
            {
                var ETLBatchStepDetails = await _unitOfWork.ETLBatchSrcStep.GetById(ETLBatchStepDTO.Id);
                if (ETLBatchStepDetails != null)
                {
                    ETLBatchStepDetails.Batch_Name = ETLBatchStepDTO.Batch_Name;
                    ETLBatchStepDetails.Batch_Step = ETLBatchStepDTO.Batch_Step;
                    ETLBatchStepDetails.Source_Id = ETLBatchStepDTO.Source_Id;
                    ETLBatchStepDetails.Src_Tgt_Name = ETLBatchStepDTO.Src_Tgt_Name;
                    ETLBatchStepDetails.Src_Tgt_Type = ETLBatchStepDTO.Src_Tgt_Type;
                    ETLBatchStepDetails.Source_Step = ETLBatchStepDTO.Source_Step;
                    ETLBatchStepDetails.Source_Step_Prgm = ETLBatchStepDTO.Source_Step_Prgm;
                    ETLBatchStepDetails.UpdatedDate = DateTime.UtcNow;
                    ETLBatchStepDetails.UpdatedBy = ETLBatchStepDTO.UpdatedBy;
                    //ETLBatchStepSrcDetails.IsActive = ETLBatchStepSrc.IsActive;

                    _logger.LogInformation($"Datetime for updating the object! is {ETLBatchStepDetails.UpdatedDate}");


                    _unitOfWork.ETLBatchSrcStep.Upsert(ETLBatchStepDetails);
                    return true;
                                        
                }
            }
            return false;
        }
            
        public async Task<bool> DeleteETLBatchSrcStep(int Id)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(DeleteETLBatchSrcStep),
      Id);
            if (Id != 0)
            {
                var ETLBatchStepDetails = await _unitOfWork.ETLBatchSrcStep.GetById(Id);
                if (ETLBatchStepDetails != null)
                {                    
                    ETLBatchStepDetails.IsActive = false;

                    _unitOfWork.ETLBatchSrcStep.Upsert(ETLBatchStepDetails);
                    _logger.LogInformation($"Deleted the record!");


                    return true;
                                        
                }
            }
            _logger.LogInformation($"Failed while deleting the record!");
            return false;
        }

        public async Task<IEnumerable<ETLBatchSrcStepDTO>> GetETLBatchSrcStepFilterAsync(string batchName,string sourceId)
        {
            var eTLBatchSrcSteps = await _unitOfWork.ETLBatchSrcStep.GetAll();

            //Active records will in null or active
            var eTLBatchSrcStepsActive = eTLBatchSrcSteps.Where(x => x.IsActive != false && x.Batch_Name == batchName && x.Source_Id == sourceId).ToList();

            return _mapper.Map<IEnumerable<ETLBatchSrcStepDTO>>(eTLBatchSrcStepsActive);
        }

        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }
}
