using AutoMapper;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Interfaces;

namespace PEI_ETL.Services.Service
{

    public class ETLBatchSrcService: IETLBatchSrcService
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

            //Active records will in null or active
            var eTLBatchSrcsActive = eTLBatchSrcs.Where(x=>x.IsActive != false).ToList();

            return _mapper.Map<IEnumerable<ETLBatchSrcDTO>>(eTLBatchSrcsActive);
        }

        public async Task<bool> InsertAsync(ETLBatchSrcDTO eTLBatchSrcDTO)
        {
            eTLBatchSrcDTO.CreatedDate = DateTime.UtcNow;
            var eTLBatchSrc = _mapper.Map<ETLBatchSrc>(eTLBatchSrcDTO);
            return await _unitOfWork.ETLBatchSrc.Add(eTLBatchSrc);
        }

        public async Task<bool> UpdateETLBatchSrc(ETLBatchSrcDTO eTLBatchSrcDTO)
        {
            if (eTLBatchSrcDTO != null)
            {
                var eTLBatchSrcDetails = await _unitOfWork.ETLBatchSrc.GetById(eTLBatchSrcDTO.Id);
                if (eTLBatchSrcDetails != null)
                {
                    eTLBatchSrcDetails.Batch_Name = eTLBatchSrcDTO.Batch_Name;
                    eTLBatchSrcDetails.Batch_Type = eTLBatchSrcDTO.Batch_Type;
                    eTLBatchSrcDetails.Source_Server = eTLBatchSrcDTO.Source_Server;
                    eTLBatchSrcDetails.Src_Extract_Seq = eTLBatchSrcDTO.Src_Extract_Seq;
                    eTLBatchSrcDetails.Source_Type = eTLBatchSrcDTO.Source_Type;
                    eTLBatchSrcDetails.Source_Name = eTLBatchSrcDTO.Source_Name;
                    eTLBatchSrcDetails.Src_PK_String = eTLBatchSrcDTO.Src_PK_String;
                    eTLBatchSrcDetails.UpdatedDate = DateTime.UtcNow;
                    eTLBatchSrcDetails.UpdatedBy = eTLBatchSrcDTO.UpdatedBy;
                    //eTLBatchSrcDetails.IsActive = eTLBatchSrc.IsActive;

                    _unitOfWork.ETLBatchSrc.Upsert(eTLBatchSrcDetails);
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
            
        public async Task<bool> DeleteETLBatchSrc(int Id)
        {
            if (Id != 0)
            {
                var eTLBatchSrcDetails = await _unitOfWork.ETLBatchSrc.GetById(Id);
                if (eTLBatchSrcDetails != null)
                {                    
                    eTLBatchSrcDetails.IsActive = false;

                    _unitOfWork.ETLBatchSrc.Upsert(eTLBatchSrcDetails);
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

        public async Task<IEnumerable<ETLBatchSrcDTO>> GetETLBatchSrcByBatchNameAsync(string batchName)
        {
            var eTLBatchSrcs = await _unitOfWork.ETLBatchSrc.GetAll();

            //Active records will in null or active
            var eTLBatchSrcsActive = eTLBatchSrcs.Where(x => x.IsActive != false && x.Batch_Name == batchName).ToList();

            return _mapper.Map<IEnumerable<ETLBatchSrcDTO>>(eTLBatchSrcsActive);
        }

        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }
}
