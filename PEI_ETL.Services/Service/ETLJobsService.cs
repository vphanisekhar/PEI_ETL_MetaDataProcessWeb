using AutoMapper;
using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Interfaces;

namespace PEI_ETL.Services.Service
{

    public class ETLJobsService: IETLJobsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        private readonly ILogger<ETLJobsService> _logger;

        public ETLJobsService(
            IUnitOfWork unitOfWork,
            IMapper mapper, ILogger<ETLJobsService> logger
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ETLJobsDTO>> GetETLJobsAsync()
        {
            var eTLJobs = await _unitOfWork.ETLJobs.GetAll();

            //Active records will in null or active
            var eTLJobsActive = eTLJobs.Where(x=>x.Is_Step_Active.ToString().ToLower() != "n").ToList();

            return _mapper.Map<IEnumerable<ETLJobsDTO>>(eTLJobsActive);
        }

        public async Task<bool> InsertAsync(ETLJobsDTO eTLJobsDTO)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(InsertAsync),
          eTLJobsDTO);

            eTLJobsDTO.CreatedDate = DateTime.UtcNow;
            var eTLJobs = _mapper.Map<ETLJobs>(eTLJobsDTO);

            _logger.LogInformation($"Datetime for creating the object! is {eTLJobsDTO.CreatedDate}");

            return await _unitOfWork.ETLJobs.Add(eTLJobs);
        }

        public async Task<bool> UpdateETLJobs(ETLJobsDTO eTLJobsDTO)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(UpdateETLJobs),
         eTLJobsDTO);

            if (eTLJobsDTO != null)
            {
                var eTLJobsDetails = await _unitOfWork.ETLJobs.GetById(eTLJobsDTO.Id);
                if (eTLJobsDetails != null)
                {
                    eTLJobsDetails.Batch_Name = eTLJobsDTO.Batch_Name;
                    eTLJobsDetails.Job_Name = eTLJobsDTO.Job_Name;
                    eTLJobsDetails.Job_Stage = eTLJobsDTO.Job_Stage;
                    eTLJobsDetails.Job_Step_Seq_No = eTLJobsDTO.Job_Step_Seq_No;
                    eTLJobsDetails.Batch_Step = eTLJobsDTO.Batch_Step;
                    eTLJobsDetails.Is_Step_Active = eTLJobsDTO.Is_Step_Active;
               
                    eTLJobsDetails.UpdatedDate = DateTime.UtcNow;
                    eTLJobsDetails.UpdatedBy = eTLJobsDTO.UpdatedBy;
                    
                    _logger.LogInformation($"Datetime for updating the object! is {eTLJobsDTO.UpdatedDate}");


                    _unitOfWork.ETLJobs.Upsert(eTLJobsDetails);
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
            
        public async Task<bool> DeleteETLJobs(int Id)
        {
            _logger.LogDebug("Executing {Action} {Parameters}", nameof(DeleteETLJobs),
      Id);
            if (Id != 0)
            {
                var eTLJobsDetails = await _unitOfWork.ETLJobs.GetById(Id);
                if (eTLJobsDetails != null)
                {                    
                    eTLJobsDetails.Is_Step_Active = 'N';

                    _unitOfWork.ETLJobs.Upsert(eTLJobsDetails);
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
               
        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }
}
