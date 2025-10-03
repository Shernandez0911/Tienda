using Hangfire.Storage.SQLite.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tienda.src.Domain.Models
{
    public enum CodeType
    {
        EmailVerification,
        PasswordReset,
        PasswordChange
    }
    public class VerificationCode
    {
        public int Id { get; set; }
        public required CodeType CodeType { get; set; }
        public required string Code { get; set; }
        public int AttemptCount { get; set; } = 0;
        public required DateTime ExpiryDate { get; set; }

        public required int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}