using ElectricityBillingSystemDemo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using ElectricityBillingSystemDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectricityBillingSystemDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ForgotPasswordController:ControllerBase
    {
        private readonly ElectricityDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly EmailRepository _emailRepository;

        public ForgotPasswordController(IConfiguration configuration, ElectricityDbContext dbContext, EmailRepository emailRepository)
        {
            _configuration = configuration;
            _context = dbContext;
            _emailRepository = emailRepository;
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(Password p)
        {
            if (string.IsNullOrEmpty(p.email))
            {
                return BadRequest(new { error = "Email is required." });
            }
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == p.email);
            if (user == null)
            {
                return NotFound(new { message = "Customer not found." });
            }

  
            var otp = GenerateOTP();

         
            user.PasswordResetOTP = otp;
            user.PasswordResetOTPCreationTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            SendOTPByEmail(user.Email, otp);

            return Ok(new { message = "OTP sent successfully.",otp, user.Email });
        }

        private string GenerateOTP()
        {
            var otp = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < 6; i++)
            {
                otp.Append(random.Next(0,9));
            }
            return otp.ToString();
        }

        private void SendOTPByEmail(string recipientEmail, string otp)
        {
            _emailRepository.SendEmail(recipientEmail, "OTP for password reset", $"Your otp is {otp}");
        }

        [HttpPost("ResetPasswordWithOTP")]
        public async Task<IActionResult> ResetPasswordWithOTP(ResetPassword r)
        {
            if (string.IsNullOrEmpty(r.email) || string.IsNullOrEmpty(r.otp) || string.IsNullOrEmpty(r.newPassword) || string.IsNullOrEmpty(r.confirmPassword))
            {
                return BadRequest(new { error = "Email, OTP, newPassword, and confirmPassword are required." });
            }

            var user =await _context.Users.FirstOrDefaultAsync(c => c.Email == r.email && c.PasswordResetOTP == r.otp);
            if (user == null)
            {
                return BadRequest(new { error = "Invalid OTP or email." });
            }

            
            if (DateTime.UtcNow.Subtract((DateTime)user.PasswordResetOTPCreationTime).TotalMinutes > 10)
            {
                return BadRequest("OTP has expired.");
            }
            if (r.newPassword != r.confirmPassword)
            {
                return BadRequest(new { error = "Passwords do not match." });
            }

            
            user.Password = r.newPassword; 
            user.PasswordResetOTP = null; 
            user.PasswordResetOTPCreationTime = null; 
            await _context.SaveChangesAsync();
            PasswordResetSuccessfulByEmail(r.email);
            return Ok(new { message = "Password reset successful." });
        }

        private void PasswordResetSuccessfulByEmail(string recipientEmail)
        {
            _emailRepository.SendEmail(recipientEmail, "Password Changed Successfully", "Your password has been changed successfully.");
        }

        public class Password
        {
            public string? email { get; set; }
        }

        public class ResetPassword
        {
            public string? email { get; set; }
            public string? otp { get; set; }
            public string? newPassword { get; set; }
            public string? confirmPassword { get; set; }
        }
        

    }
}

