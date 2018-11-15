namespace citizen.Models.Api
{
    public class AuthenticationResponse
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}