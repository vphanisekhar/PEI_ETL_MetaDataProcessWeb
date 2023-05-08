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

            //Active records will in null or active
            var eTLBatchSrcsActive = eTLBatchSrcs.Where(x=>x.IsActive != false).ToList();

            return _mapper.Map<IEnumerable<ETLBatchSrcDTO>>(eTLBatchSrcsActive);
        }

        public async Task<bool> InsertAsync(ETLBatchSrcDTO eTLBatchSrcDTO)
        {
            var eTLBatchSrc = _mapper.Map<ETLBatchSrc>(eTLBatchSrcDTO);
            return await _unitOfWork.ETLBatchSrc.Add(eTLBatchSrc);
        }

        public async Task<bool> UpdateETLBatchSrc(ETLBatchSrc eTLBatchSrc)
        {
            if (eTLBatchSrc != null)
            {
                var eTLBatchSrcDetails = await _unitOfWork.ETLBatchSrc.GetById(eTLBatchSrc.Id);
                if (eTLBatchSrcDetails != null)
                {
                    eTLBatchSrcDetails.Batch_Name = eTLBatchSrc.Batch_Name;
                    eTLBatchSrcDetails.Batch_Type = eTLBatchSrc.Batch_Type;
                    eTLBatchSrcDetails.Source_Server = eTLBatchSrc.Source_Server;
                    eTLBatchSrcDetails.Src_Extract_Seq = eTLBatchSrc.Src_Extract_Seq;
                    eTLBatchSrcDetails.Source_Type = eTLBatchSrc.Source_Type;
                    eTLBatchSrcDetails.Source_Name = eTLBatchSrc.Source_Name;
                    eTLBatchSrcDetails.Src_PK_String = eTLBatchSrc.Src_PK_String;
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

        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }
}
