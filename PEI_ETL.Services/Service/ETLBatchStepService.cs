using AutoMapper;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Interfaces;

namespace PEI_ETL.Services.Service
{

    public class ETLBatchStepService:IETLBatchStepService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ETLBatchStepService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ETLBatchStepDTO>> GetETLBatchStepAsync()
        {
            var ETLBatchStep = await _unitOfWork.ETLBatchStep.GetAll();

            //Active records will in null or active
            var ETLBatchStepActive = ETLBatchStep.Where(x=>x.IsActive != false).ToList();

            return _mapper.Map<IEnumerable<ETLBatchStepDTO>>(ETLBatchStepActive);
        }

        public async Task<bool> InsertAsync(ETLBatchStepDTO ETLBatchStepDTO)
        {
            ETLBatchStepDTO.CreatedDate = DateTime.UtcNow;
            var ETLBatchStep = _mapper.Map<ETLBatchStep>(ETLBatchStepDTO);
            return await _unitOfWork.ETLBatchStep.Add(ETLBatchStep);
        }

        public async Task<bool> UpdateETLBatchStep(ETLBatchStepDTO ETLBatchStepDTO)
        {
            if (ETLBatchStepDTO != null)
            {
                var ETLBatchStepDetails = await _unitOfWork.ETLBatchStep.GetById(ETLBatchStepDTO.Id);
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

                    _unitOfWork.ETLBatchStep.Upsert(ETLBatchStepDetails);
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
            
        public async Task<bool> DeleteETLBatchStep(int Id)
        {
            if (Id != 0)
            {
                var ETLBatchStepDetails = await _unitOfWork.ETLBatchStep.GetById(Id);
                if (ETLBatchStepDetails != null)
                {                    
                    ETLBatchStepDetails.IsActive = false;

                    _unitOfWork.ETLBatchStep.Upsert(ETLBatchStepDetails);
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
