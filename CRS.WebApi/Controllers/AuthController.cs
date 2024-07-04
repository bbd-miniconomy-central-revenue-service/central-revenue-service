// // using Amazon;
// // using Amazon.CognitoIdentityProvider;
// // using Amazon.CognitoIdentityProvider.Model;
// // using Amazon.Runtime;
// // using Microsoft.AspNetCore.Authorization;
// // using Microsoft.AspNetCore.Mvc;
// // using CRS.WebApi.Models;
// // using CRS.WebApi.Models.Dtos;
// // using Task = System.Threading.Tasks.Task;

// // namespace server.Controllers
// // {
// //     [Route("api/auth")]
// //     [ApiController]
// //     [AllowAnonymous]
// //     public class AuthController() : ControllerBase
// //     {
// //         [HttpPost("signIn")]
// //         public async Task<IActionResult> SignIn([FromBody] AuthSignInDto authSignIn)
// //         {
            
// //         }
// //     }
// // }
// using Microsoft.AspNetCore.Mvc;
// using System.Net.Http.Headers;
// using System.Text.Json;
// using DocGen.Infrastructure.Data;
// using DocGen.Core.Entities;
// using Microsoft.EntityFrameworkCore;
// using CRS.WebApi.Models;

// [Route("api/v1")]
// [ApiController]
// public class AuthController : ControllerBase
// {
//     private readonly ApplicationDbContext _context;
//     private readonly IHttpClientFactory _httpClientFactory;
//     private readonly string _userInfoEndpoint = "https://dev-2f8sdpf6pls655l7.us.auth0.com/userinfo";

//     public AuthController(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
//     {
//         _context = context;
//         _httpClientFactory = httpClientFactory;
//     }

//     [HttpPost("user")]
//     public async Task<IActionResult> Login()
//     {
//         string accessToken = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
//         if (string.IsNullOrEmpty(accessToken))
//         {
//             return BadRequest("Missing access token.");
//         }
        
//         string userSub = User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
//         if (string.IsNullOrEmpty(userSub))
//         {
//             return StatusCode(401, "Unauthorized");
//         }

//         UserInfo userInfo = await GetUserInfoAsync(accessToken);
//         if (userInfo == null)
//         {
//             return BadRequest("Failed to retrieve user information.");
//         }

//         User user = await CheckAndCreateUserAsync(userInfo.Name, userInfo.Email, userSub);
//         return Ok(new { Message = "Login successful", User = user });
//     }

//     private async Task<UserInfo> GetUserInfoAsync(string accessToken)
//     {
//         HttpClient client = _httpClientFactory.CreateClient();
//         client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

//         HttpResponseMessage response = await client.GetAsync(_userInfoEndpoint);
//         if (!response.IsSuccessStatusCode)
//         {
//             return null;
//         }

//         string content = await response.Content.ReadAsStringAsync();
//         return JsonSerializer.Deserialize<UserInfo>(
//             content, 
//             new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
//             );
//     }

//     private async Task<User> CheckAndCreateUserAsync(string name, string email, string userSub)
//     {
//         User user = await _context.User.FirstOrDefaultAsync(user => user.UserSub == userSub);
//         if (user == null)
//         {
//             user = new User { UserName = name, UserEmail = email, UserSub = userSub };
//             _context.User.Add(user);
//         }
//         else
//         {

//             user.UserSub = userSub;
//             user.UserName = name;
//         }
//         await _context.SaveChangesAsync();
//         return user;
//     }
// }
