using AutoMapper;
using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Interfaces;

namespace PEI_ETL.Services.Service
{

    public class ETLBatchSrcStepCfgService: IETLBatchSrcStepCfgService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        private readonly ILogger<ETLBatchSrcStepCfgService> _logger;

        public ETLBatchSrcStepCfgService(
            IUnitOfWork unitOfWork,
            IMapper mapper, ILogger<ETLBatchSrcStepCfgService> logger
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ETLBatchSrcStepCfgDTO>> GetETLBatchSrcStepCfgAsync()
        {
            var eTLBatchSrcStepCfgs = await _unitOfWork.ETLBatchSrcStepCfg.GetAll();

            //Active records will in null or active
            var eTLBatchSrcStepCfgsActive = eTLBatchSrcStepCfgs.Where(x=>x.IsActive != false).ToList();

            return _mapper.Map<IEnumerable<ETLBatchSrcStepCfgDTO>>(eTLBatchSrcStepCfgsActive);
        }

        public async Task<bool> InsertAsync(ETLBatchSrcStepCfgDTO eTLBatchSrcStepCfgDTO)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(InsertAsync),
          eTLBatchSrcStepCfgDTO);

            eTLBatchSrcStepCfgDTO.CreatedDate = DateTime.UtcNow;
            var eTLBatchSrcStepCfg = _mapper.Map<ETLBatchSrcStepCfg>(eTLBatchSrcStepCfgDTO);

            _logger.LogInformation($"Datetime for creating the object! is {eTLBatchSrcStepCfgDTO.CreatedDate}");

            return await _unitOfWork.ETLBatchSrcStepCfg.Add(eTLBatchSrcStepCfg);
        }

        public async Task<bool> UpdateETLBatchSrcStepCfg(ETLBatchSrcStepCfgDTO eTLBatchSrcStepCfgDTO)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(UpdateETLBatchSrcStepCfg),
         eTLBatchSrcStepCfgDTO);

            if (eTLBatchSrcStepCfgDTO != null)
            {
                var eTLBatchSrcStepCfgDetails = await _unitOfWork.ETLBatchSrcStepCfg.GetById(eTLBatchSrcStepCfgDTO.Id);
                if (eTLBatchSrcStepCfgDetails != null)
                {
                    eTLBatchSrcStepCfgDetails.Batch_Name = eTLBatchSrcStepCfgDTO.Batch_Name;
                    eTLBatchSrcStepCfgDetails.Source_Id = eTLBatchSrcStepCfgDTO.Source_Id;
                    eTLBatchSrcStepCfgDetails.Batch_Step = eTLBatchSrcStepCfgDTO.Batch_Step;
                    eTLBatchSrcStepCfgDetails.Src_Tgt_Type = eTLBatchSrcStepCfgDTO.Src_Tgt_Type;
                    eTLBatchSrcStepCfgDetails.Parameter = eTLBatchSrcStepCfgDTO.Parameter;
                    eTLBatchSrcStepCfgDetails.Value = eTLBatchSrcStepCfgDTO.Value;
                    
                    eTLBatchSrcStepCfgDetails.UpdatedDate = DateTime.UtcNow;
                    eTLBatchSrcStepCfgDetails.UpdatedBy = eTLBatchSrcStepCfgDTO.UpdatedBy;
                 
                    _logger.LogInformation($"Datetime for updating the object! is {eTLBatchSrcStepCfgDTO.UpdatedDate}");


                    _unitOfWork.ETLBatchSrcStepCfg.Upsert(eTLBatchSrcStepCfgDetails);
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
            
        public async Task<bool> DeleteETLBatchSrcStepCfg(int Id)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(DeleteETLBatchSrcStepCfg),
      Id);
            if (Id != 0)
            {
                var eTLBatchSrcStepCfgDetails = await _unitOfWork.ETLBatchSrcStepCfg.GetById(Id);
                if (eTLBatchSrcStepCfgDetails != null)
                {                    
                    eTLBatchSrcStepCfgDetails.IsActive = false;

                    _unitOfWork.ETLBatchSrcStepCfg.Upsert(eTLBatchSrcStepCfgDetails);
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

        public async Task<IEnumerable<ETLBatchSrcStepCfgDTO>> GetETLBatchSrcStepCfgFilterAsync(string batchName, string batchStep)
        {
            var eTLBatchSrcStepCfgs = await _unitOfWork.ETLBatchSrcStepCfg.GetAll();

            //Active records will in null or active
            var eTLBatchSrcStepCfgsActive = eTLBatchSrcStepCfgs.Where(x => x.IsActive != false && x.Batch_Name == batchName && x.Batch_Step == batchStep).ToList();

            return _mapper.Map<IEnumerable<ETLBatchSrcStepCfgDTO>>(eTLBatchSrcStepCfgsActive);
        }

        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }
}
