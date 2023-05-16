using AutoMapper;
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
        public ETLBatchSrcStepService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            ETLBatchStepDTO.CreatedDate = DateTime.UtcNow;
            var ETLBatchStep = _mapper.Map<ETLBatchSrcStep>(ETLBatchStepDTO);
            return await _unitOfWork.ETLBatchSrcStep.Add(ETLBatchStep);
        }

        public async Task<bool> UpdateETLBatchSrcStep(ETLBatchSrcStepDTO ETLBatchStepDTO)
        {
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

                    _unitOfWork.ETLBatchSrcStep.Upsert(ETLBatchStepDetails);
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
            
        public async Task<bool> DeleteETLBatchSrcStep(int Id)
        {
            if (Id != 0)
            {
                var ETLBatchStepDetails = await _unitOfWork.ETLBatchSrcStep.GetById(Id);
                if (ETLBatchStepDetails != null)
                {                    
                    ETLBatchStepDetails.IsActive = false;

                    _unitOfWork.ETLBatchSrcStep.Upsert(ETLBatchStepDetails);
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
