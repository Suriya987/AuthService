using System;
using System.Collections.Generic;

namespace AuthService.Models;

public partial class RefreshToken
{
    public long RefreshTokenId { get; set; }

    public long UserId { get; set; }

    public string TokenHash { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? RevokedAt { get; set; }
}
