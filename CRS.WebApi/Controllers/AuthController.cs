using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CRS.WebApi.Models;
using CRS.WebApi.Models.Dtos;
using Task = System.Threading.Tasks.Task;

namespace server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController() : ControllerBase
    {
        
        private static readonly string? ClientId = Environment.GetEnvironmentVariable("COGNITO_CLIENTID");
        private static readonly BasicAWSCredentials Credentials = new(Environment.GetEnvironmentVariable("AWS_ACCESS_ID"), Environment.GetEnvironmentVariable("AWS_ACCESS_SECRET"));
        private readonly AmazonCognitoIdentityProviderClient _provider = new(Credentials, RegionEndpoint.EUWest1);

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn([FromBody] AuthSignInDto authSignIn)
        {
            try
            {
                var authRequest = new InitiateAuthRequest
                {
                    AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                    ClientId = ClientId,
                    AuthParameters = new Dictionary<string, string>
                    {
                        { "USERNAME", authSignIn.username },
                        { "PASSWORD", authSignIn.password }
                    }
                };
                var authResponse = await _provider.InitiateAuthAsync(authRequest);
                if (authResponse.AuthenticationResult == null) return Unauthorized("Invalid username or password");
                Console.WriteLine("Sign in successful");
                return Ok(authResponse);

            }
            catch (Exception e)
            {
                return Unauthorized("Invalid username or password");
            }
        }
    }
}
