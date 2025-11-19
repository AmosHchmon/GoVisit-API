using GoVisitAPI.DataModel;
using GoVisitAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoVisitAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController(GoVisitContext context) : ControllerBase
    {
        [HttpPost("signup")]
        public IActionResult SignUp(SignUpRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var existing = context.Invitees.FirstOrDefault(x => x.Phone == request.Phone);
            if (existing != null)
                return BadRequest("Phone already exists");

            var invitee = new Invitee
            {
                FullName = request.FullName,
                Phone = request.Phone,
                Email = request.Email,
                PreferredLanguage = request.PreferredLanguage
            };

            context.Invitees.Add(invitee);
            context.SaveChanges();

            return Ok(new { invitee.InviteeId, invitee.FullName });
        }

        [HttpPost("signin")]
        public IActionResult SignIn(SignInRequest request)
        {
            var user = context.Invitees.FirstOrDefault(x => x.Phone == request.Phone);
            if (user == null)
                return Unauthorized();

            return Ok(new { user.InviteeId, user.FullName });
        }
    }
}