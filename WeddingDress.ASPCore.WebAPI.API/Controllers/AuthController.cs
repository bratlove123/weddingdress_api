using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models;
using AutoMapper;
using System.Security.Claims;
using WeddingDress.ASPCore.WebAPI.Services.Interfaces;
using Newtonsoft.Json;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using System;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WeddingDress.ASPCore.WebAPI.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly FrontEndOptions _frontEndOptions;
        private readonly SecretOptions _secretOptions;
        private static readonly HttpClient Client = new HttpClient();

        public AuthController(UserManager<ApplicationUser> userManager, IAuthService authService, IEmailService emailService, IMapper mapper, IOptions<FrontEndOptions> frontEndOptions, IOptions<SecretOptions> secretOptions)
        {
            _userManager = userManager;
            _authService = authService;
            _emailService = emailService;
            _mapper = mapper;
            _frontEndOptions = frontEndOptions.Value;
            _secretOptions = secretOptions.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel regisModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appUser = _mapper.Map<ApplicationUser>(regisModel);

            var result = await _userManager.CreateAsync(appUser, regisModel.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Message", result.Errors.FirstOrDefault().Description);
                return BadRequest(ModelState);
            }

            string code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(code);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            var callbackUrl = _frontEndOptions.Url + "admin/verify?code=" + codeEncoded + "&userId=" + appUser.Id;

            await _emailService.SendEmailAsync(regisModel.Email, "Please verify your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return Ok();
        }

        [HttpGet]
        [Route("confirm")]
        public async Task<IActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("Message", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ModelState.AddModelError("Message", "Cannot find the user");
                return BadRequest(ModelState);
            }

            var codeDecodedBytes = WebEncoders.Base64UrlDecode(code);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Message", result.Errors.FirstOrDefault().Description);
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(vm.Username, vm.Password);

            if (identity == null)
            {
                ModelState.AddModelError("Message", "Username or password is not valid.");
                ModelState.AddModelError("ErrorCode", "1100");
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(vm.Username);
            var checkEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!checkEmailConfirmed)
            {
                ModelState.AddModelError("Message", "Email not verified.");
                ModelState.AddModelError("ErrorCode", "1101");
                ModelState.AddModelError("Email", user.Email);
                return BadRequest(ModelState);
            }
            var roles = await _userManager.GetRolesAsync(user);
            var jwt = await _authService.GenerateJwt(identity, vm.Username, new JsonSerializerSettings { Formatting = Formatting.Indented }, roles);
            var jwtObj = JsonConvert.DeserializeObject<LoginReturnViewModel>(jwt);
            jwtObj.Role= roles.FirstOrDefault();
            jwtObj.UserName = user.UserName;
            jwtObj.FullName = user.FirstName + " " + user.LastName;
            jwtObj.Email = user.Email;
            jwtObj.Avatar = user.PictureUrl;
            return Ok(jwtObj);
        }

        [HttpPost("forgot")]
        public async Task<IActionResult> Forgot([FromBody]ForgotViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(vm.Email);

            if (user == null)
            {
                ModelState.AddModelError("Message", "Cannot find the user");
                return BadRequest(ModelState);
            }

            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(code);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            var callbackUrl = _frontEndOptions.Url + "admin/reset?code=" + codeEncoded + "&userId=" + user.Id;

            await _emailService.SendEmailAsync(vm.Email, "Reset password", $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return Ok();
        }

        [HttpPost("reset")]
        public async Task<IActionResult> Reset([FromBody]ResetViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Code) || string.IsNullOrWhiteSpace(vm.UserId) || string.IsNullOrEmpty(vm.Password))
            {
                ModelState.AddModelError("Message", "User Id, Code and Password are required");
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(vm.UserId);
            if (user == null)
            {
                ModelState.AddModelError("Message", "Cannot find the user");
                return BadRequest(ModelState);
            }

            var codeDecodedBytes = WebEncoders.Base64UrlDecode(vm.Code);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, codeDecoded, vm.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Message", result.Errors.FirstOrDefault().Description);
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpGet("check")]
        [Authorize(Policy = "ApiUser")]
        public IActionResult CheckLogon()
        {
            return Ok();
        }

        [HttpPost("facebook")]
        public async Task<IActionResult> Facebook([FromBody]FacebookAuthViewModel model)
        {
            // 1.generate an app access token
            var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_secretOptions.FacebookAppId}&client_secret={_secretOptions.FacebookAppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
            // 2. validate the user access token
            var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                ModelState.AddModelError("Message", "Invalid facebook token.");
                return BadRequest(ModelState);
            }

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new ApplicationUser
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    FacebookId = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    PictureUrl = userInfo.Picture.Data.Url
                };

                var result = await _userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("Message", "Something wrong when login with facebook.");
                    return BadRequest(ModelState);
                }
            }

            // generate the jwt for the local user...
            ApplicationUser localUser = await _userManager.FindByNameAsync(userInfo.Email);

            if (localUser == null)
            {
                ModelState.AddModelError("Message", "Failed to create local user account.");
                return BadRequest(ModelState);
            }
            var identity = _authService.GenerateClaimsIdentity(localUser.UserName, localUser.Id);
            var roles = await _userManager.GetRolesAsync(user);
            var jwt = await _authService.GenerateJwt(identity, localUser.UserName, new JsonSerializerSettings { Formatting = Formatting.Indented }, roles);

            return new OkObjectResult(jwt);
        }


        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            var userToVerify = await _userManager.FindByNameAsync(userName);
            if (userToVerify == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_authService.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}