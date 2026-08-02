using System;
using System.Collections.Generic;

namespace AuthService.Models;

public partial class AuthCredential
{
    public int AuthCredentialId { get; set; }

    public long? UserId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime PasswordChangedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Email { get; set; } = null!;

    public Guid? DeviceId { get; set; }

    public string? DeviceName { get; set; }
}

