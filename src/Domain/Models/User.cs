using Hangfire.Storage.SQLite.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.src.Domain.Models
{
    public enum Gender
    {
        Masculino, Femenino, Otro
    }
    public class User : IdentityUser<int>
    {
        public required string Rut { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public required Gender Gender { get; set; }
        public required DateTime Birthday { get; set; }

        public ICollection<VerificationCode> VerificationCodes { get; set; } = new List<VerificationCode>();

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}