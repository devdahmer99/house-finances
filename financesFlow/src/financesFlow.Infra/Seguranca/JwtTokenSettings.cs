namespace financesFlow.Infra.Seguranca
{
    public class JwtTokenSettings
    {
        public string? SigningKey { get; set; }
        public uint ExpiresMinutes { get; set; }
    }
}
