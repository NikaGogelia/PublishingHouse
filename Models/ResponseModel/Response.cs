using PublishingHouse.Enums;

namespace PublishingHouse.Models.ResponseModel;

public class Response
{
	public Status Status { get; set; } = Status.Success;
	public string Message { get; set; } = "Successful request";
	public object? Result { get; set; }

	public Response(Status status, string message, object? result = null)
	{
		Status = status;
		Message = message;
		Result = result;
	}
}
