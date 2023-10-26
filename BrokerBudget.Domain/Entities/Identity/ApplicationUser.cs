using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace BrokerBudget.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Photo { get; set; }

    // public virtual ICollection<Game>? Games { get; set; }
    // public virtual ICollection<Order>? Orders { get; set; }
}
