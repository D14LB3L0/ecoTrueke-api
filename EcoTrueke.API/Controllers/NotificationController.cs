using EcoTrueke.API.Requests.Notification;
using EcoTrueke.API.Responses;
using EcoTrueke.API.Responses.Notification;
using EcoTrueke.Domain.Interfaces.UseCases.Notification;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcoTrueke.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private readonly INotificationUseCase _notificationUseCase;

        public NotificationController(INotificationUseCase notificationUseCase)
        {
            _notificationUseCase = notificationUseCase;
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetPaginatedNotifications([FromQuery] GetPaginatedNotificationsRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _notificationUseCase.GetPaginatedNotificationsExecute(request.Page, request.AmountPage, LoggedUserId);

                var paginatedNotificationResponse = JsonConvert.DeserializeObject<GetPaginatedNotificationsResponse>(result.Data!);

                var apiResponse = new ApiResponse<GetPaginatedNotificationsResponse>(paginatedNotificationResponse!, result.Message);

                return StatusCode(result.Code, apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("mark-read")]
        public async Task<IActionResult> MarkAsRead([FromBody] MarkAsReadRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _notificationUseCase.MarkAsReadExecute(request.NotificationIds);

                return StatusCode(result.Code, result); 
            }
            catch (Exception)
            {
                return BadRequest(500);
            }
        }
    }
}
