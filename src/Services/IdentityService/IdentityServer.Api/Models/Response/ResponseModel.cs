namespace IdentityServer.Api.Models.Response
{
    public class ResponseModel
    {
        public string Username { get; set; }
        public string Token { get; set; }

        public ResponseModel(string username, string token)
        {
            Token = token;
            Username = username;
        }
    }
}
