using FoodDlvProject2.Models.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodDlvProject2.Models.Services.Interfaces
{
    public interface IDeliveryCancellationRecordRepository
	{
        Task<string> EditAsync(DriverCancellationRecordDTO model);
        Task<List<DriverCancellationRecordDTO>> GetCancellationRecordsAsync();
        Task<DriverCancellationRecordDTO> GetEditAsync(int? id);
        Task<SelectList> GetListAsync(int? CancellationId = 1);
        Task<List<DriverCancellationRecordDTO>> GetPersonalCancellationRecordsAsync(int? id);
    }
}