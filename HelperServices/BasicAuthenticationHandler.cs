using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;
using QuestionnaireManagerPOC.Services;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

//public class BasicAuthAttribute : Attribute, IAuthorizationFilter, ILoginService
//{
//    private readonly IUserService _userService;

//    public BasicAuthAttribute(UserService userService)
//    {
//        _userService = userService;
//    }



//    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
//    {
//        string authHeader = context.HttpContext.Request.Headers["Authorization"];

//        if (authHeader != null && authHeader.StartsWith("Basic "))
//        {
//            // Extract credentials
//            string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
//            Encoding encoding = Encoding.GetEncoding("UTF-8");
//            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
//            int separatorIndex = usernamePassword.IndexOf(':');
//            string username = usernamePassword.Substring(0, separatorIndex);
//            string password = usernamePassword.Substring(separatorIndex + 1);

//            var data = _userService.GetUsers();
//            // TODO: Validate credentials
//            if(data.Count() > 0 && data.Where(x=>x.Email == username && x.Password == password).Count() > 0)
//            {

//            //}
//            //if (username == "mani@gmail.com" && password == "password")
//            //{
//                // Credentials are valid, set the user email as a claim
//                context.HttpContext.User = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity(new[] { new System.Security.Claims.Claim("email", "user@example.com") }, "basic"));
//            }
//            else
//            {
//                // Credentials are invalid, return unauthorized status code
//                context.Result = new UnauthorizedResult();
//                return;
//            }
//        }
//        else
//        {
//            // No authorization header found, return unauthorized status code
//            context.Result = new UnauthorizedResult();
//            return;
//        }
//    }
//    public void OnAuthorization(AuthorizationFilterContext context)
//    {
//        string authHeader = context.HttpContext.Request.Headers["Authorization"];

//        if (authHeader != null && authHeader.StartsWith("Basic "))
//        {
//            // Extract credentials
//            string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
//            Encoding encoding = Encoding.GetEncoding("UTF-8");
//            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
//            int separatorIndex = usernamePassword.IndexOf(':');
//            string username = usernamePassword.Substring(0, separatorIndex);
//            string password = usernamePassword.Substring(separatorIndex + 1);

//            var data = _userService.GetUsers();
//            // TODO: Validate credentials
//            if (Login(username, password))
//            {
//                // Credentials are valid, set the user email as a claim
//                context.HttpContext.User = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity(new[] { new System.Security.Claims.Claim("email", username) }, "basic"));
//            }
//            else
//            {
//                // Credentials are invalid, return unauthorized status code
//                context.Result = new UnauthorizedResult();
//                return;
//            }
//        }
//        else
//        {
//            // No authorization header found, return unauthorized status code
//            context.Result = new UnauthorizedResult();
//            return;
//        }
//    }

//    public bool Login(string userName, string password)
//    {
//        throw new NotImplementedException();
//    }
//}


public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly ILoginService _loginService;
    private readonly IUserService _userService;

    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ILoginService loginService, IUserService userService)
        : base(options, logger, encoder, clock)
    {
        _loginService = loginService;
        _userService = userService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Missing Authorization Header");
        }

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
            var username = credentials[0];
            var password = credentials[1];
            bool IsLoginSuccess = _loginService.Login(username, password);
            if (!IsLoginSuccess)
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }

            UserModel userData = _userService.GetUser(username);

            var claims = new[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, userData.Role) ,
                new Claim(ClaimTypes.UserData, userData.UserId.ToString())
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
    }
}
