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
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "To pole może zawierać tylko litery.")]
    public required string FirstName { get; set; }
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "To pole może zawierać tylko litery.")]
    public required string LastName { get; set; }
    public required ICollection<Kal> Kal { get; set; }
}

