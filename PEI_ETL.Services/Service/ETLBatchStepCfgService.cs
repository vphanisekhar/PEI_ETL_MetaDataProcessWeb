using AutoMapper;
using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Interfaces;

namespace PEI_ETL.Services.Service
{

    public class ETLBatchStepCfgService: IETLBatchStepCfgService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        private readonly ILogger<ETLBatchStepCfgService> _logger;

        public ETLBatchStepCfgService(
            IUnitOfWork unitOfWork,
            IMapper mapper, ILogger<ETLBatchStepCfgService> logger
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ETLBatchStepCfgDTO>> GetETLBatchStepCfgAsync()
        {
            var eTLBatchStepCfgs = await _unitOfWork.ETLBatchStepCfg.GetAll();

            //Active records will in null or active
            var eTLBatchStepCfgsActive = eTLBatchStepCfgs.Where(x=>x.IsActive != false).ToList();

            return _mapper.Map<IEnumerable<ETLBatchStepCfgDTO>>(eTLBatchStepCfgsActive);
        }

        public async Task<bool> InsertAsync(ETLBatchStepCfgDTO eTLBatchStepCfgDTO)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(InsertAsync),
          eTLBatchStepCfgDTO);

            eTLBatchStepCfgDTO.CreatedDate = DateTime.UtcNow;
            var eTLBatchStepCfg = _mapper.Map<ETLBatchStepCfg>(eTLBatchStepCfgDTO);

            _logger.LogInformation($"Datetime for creating the object! is {eTLBatchStepCfgDTO.CreatedDate}");

            return await _unitOfWork.ETLBatchStepCfg.Add(eTLBatchStepCfg);
        }

        public async Task<bool> UpdateETLBatchStepCfg(ETLBatchStepCfgDTO eTLBatchStepCfgDTO)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(UpdateETLBatchStepCfg),
         eTLBatchStepCfgDTO);

            if (eTLBatchStepCfgDTO != null)
            {
                var eTLBatchStepCfgDetails = await _unitOfWork.ETLBatchStepCfg.GetById(eTLBatchStepCfgDTO.Id);
                if (eTLBatchStepCfgDetails != null)
                {
                    eTLBatchStepCfgDetails.Batch_Name = eTLBatchStepCfgDTO.Batch_Name;
                    eTLBatchStepCfgDetails.Batch_Step = eTLBatchStepCfgDTO.Batch_Step;
                    eTLBatchStepCfgDetails.Src_Tgt_Type = eTLBatchStepCfgDTO.Src_Tgt_Type;
                    eTLBatchStepCfgDetails.Parameter = eTLBatchStepCfgDTO.Parameter;
                    eTLBatchStepCfgDetails.Value = eTLBatchStepCfgDTO.Value;
                    
                    eTLBatchStepCfgDetails.UpdatedDate = DateTime.UtcNow;
                    eTLBatchStepCfgDetails.UpdatedBy = eTLBatchStepCfgDTO.UpdatedBy;
                 
                    _logger.LogInformation($"Datetime for updating the object! is {eTLBatchStepCfgDTO.UpdatedDate}");


                    _unitOfWork.ETLBatchStepCfg.Upsert(eTLBatchStepCfgDetails);
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
            
        public async Task<bool> DeleteETLBatchStepCfg(int Id)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(DeleteETLBatchStepCfg),
      Id);
            if (Id != 0)
            {
                var eTLBatchStepCfgDetails = await _unitOfWork.ETLBatchStepCfg.GetById(Id);
                if (eTLBatchStepCfgDetails != null)
                {                    
                    eTLBatchStepCfgDetails.IsActive = false;

                    _unitOfWork.ETLBatchStepCfg.Upsert(eTLBatchStepCfgDetails);
                    _logger.LogInformation($"Deleted the record!");


                    return true;

                    //var result = _unitOfWork.Save();

                    //if (result > 0)
                    //    return true;
                    //else
                    //    return false;
                }
            }
            _logger.LogInformation($"Failed while deleting the record!");
            return false;
        }

        public async Task<IEnumerable<ETLBatchStepCfgDTO>> GetETLBatchStepCfgFilterAsync(string batchName, string batchStep)
        {
            var eTLBatchStepCfgs = await _unitOfWork.ETLBatchStepCfg.GetAll();

            //Active records will in null or active
            var eTLBatchStepCfgsActive = eTLBatchStepCfgs.Where(x => x.IsActive != false && x.Batch_Name == batchName && x.Batch_Step == batchStep).ToList();

            return _mapper.Map<IEnumerable<ETLBatchStepCfgDTO>>(eTLBatchStepCfgsActive);
        }

        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }
}
