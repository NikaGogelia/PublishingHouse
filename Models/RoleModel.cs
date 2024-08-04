using PublishingHouse.Identity.Models;

namespace PublishingHouse.Models;

public class RoleModel
{
	[ValidRole]
	public string Role { get; set; } = string.Empty;
}
