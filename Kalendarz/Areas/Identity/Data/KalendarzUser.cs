using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Kalendarz.Models;
using Microsoft.AspNetCore.Identity;

namespace Kalendarz.Areas.Identity.Data;

// Add profile data for application users by adding properties to the KalendarzUser class
public class KalendarzUser : IdentityUser<int>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required ICollection<Kal> Kal { get; set; }
}

