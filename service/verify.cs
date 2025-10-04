using Resend;
using entity.verify;
using System;
using  MyApp.Repository;
namespace service.verify;

public class Verification
{
    private readonly UserRepository _userRepository;
    private readonly IResend _resend;

    public Verification(UserRepository userRepository, IResend resend)
    {
        _userRepository = userRepository;
        _resend = resend;
    }


    public async Task VerifyEmail(user user)
    {
        string code = new Random().Next(100000, 999999).ToString();
        DateTime ExpireTime = DateTime.Now.AddMinutes(5);
        _userRepository.UpdateVerificationCode(user.email, code, ExpireTime);
        string judul = "Kode Verifikasi Email";
        string isi = $@"
            <h2>Halo {user.Username},</h2>
            <p>Kode verifikasi Anda adalah:</p>
            <h1>{code}</h1>
            <p>Berlaku sampai: {ExpireTime:dd MMM yyyy HH:mm} UTC</p>
        ";
        var message = new EmailMessage
        {
            From = "noreply@eraport.com",
            To = user.email,
            Subject = judul,
            HtmlBody = isi
        };
        await _resend.EmailSendAsync(message);
    }
    public bool ConfirmCode(Verify verify)
    {
        var email = _userRepository.GetbyEmail(verify.email);
        if (email == null)
        {
            throw new Exception("Email tidak ditemukan.");
        }
        if(email.code != verify.code)
        {
            throw new Exception("Kode verifikasi salah.");
        }
        if(email.code_expire_time < DateTime.Now)
        {
            throw new Exception("Kode verifikasi telah kedaluwarsa.");
        }
        return true;
     }
}
