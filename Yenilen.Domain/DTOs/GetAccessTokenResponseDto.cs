using System.Text.Json.Serialization;

namespace Yenilen.Domain.DTOs;

public sealed class GetAccessTokenResponseDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;
}