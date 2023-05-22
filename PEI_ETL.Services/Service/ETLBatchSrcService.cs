using AutoMapper;
using Microsoft.Extensions.Logging;
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
        
        private readonly ILogger<ETLBatchSrcService> _logger;

        public ETLBatchSrcService(
            IUnitOfWork unitOfWork,
            IMapper mapper, ILogger<ETLBatchSrcService> logger
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
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
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(InsertAsync),
          eTLBatchSrcDTO);

            eTLBatchSrcDTO.CreatedDate = DateTime.UtcNow;
            var eTLBatchSrc = _mapper.Map<ETLBatchSrc>(eTLBatchSrcDTO);

            _logger.LogInformation($"Datetime for creating the object! is {eTLBatchSrcDTO.CreatedDate}");

            return await _unitOfWork.ETLBatchSrc.Add(eTLBatchSrc);
        }

        public async Task<bool> UpdateETLBatchSrc(ETLBatchSrcDTO eTLBatchSrcDTO)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(UpdateETLBatchSrc),
         eTLBatchSrcDTO);

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
                    eTLBatchSrcDetails.Source_Id = eTLBatchSrcDTO.Source_Id;
                    eTLBatchSrcDetails.Src_PK_String = eTLBatchSrcDTO.Src_PK_String;
                    eTLBatchSrcDetails.UpdatedDate = DateTime.UtcNow;
                    eTLBatchSrcDetails.UpdatedBy = eTLBatchSrcDTO.UpdatedBy;
                    //eTLBatchSrcDetails.IsActive = eTLBatchSrc.IsActive;

                    _logger.LogInformation($"Datetime for updating the object! is {eTLBatchSrcDTO.UpdatedDate}");


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
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(DeleteETLBatchSrc),
      Id);
            if (Id != 0)
            {
                var eTLBatchSrcDetails = await _unitOfWork.ETLBatchSrc.GetById(Id);
                if (eTLBatchSrcDetails != null)
                {                    
                    eTLBatchSrcDetails.IsActive = false;

                    _unitOfWork.ETLBatchSrc.Upsert(eTLBatchSrcDetails);
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
