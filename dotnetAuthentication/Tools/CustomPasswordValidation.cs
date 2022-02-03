using Microsoft.AspNetCore.Identity;

public class CustomPasswordValidation : IdentityErrorDescriber
{
    public override IdentityError PasswordTooShort(int length)
    {
        IdentityError error = new IdentityError();
        error.Code = "PasswordToShort";
        error.Description = $"şifreniz en az {length} karakter olmalı!";
        return error;
    }

    public override IdentityError DuplicateEmail(string email)
    {
        IdentityError error = new IdentityError();
        error.Code = "DuplicateEmail";
        error.Description = $"{email} daha önce kaydedilmiş!";
        return error;
    }

    public override IdentityError InvalidUserName(string userName)
    {
        IdentityError error = new IdentityError();
        error.Code = "InvalidUserName";
        error.Description = $"{userName} kullanıcı adı hatalı!";
        return error;
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        IdentityError error = new IdentityError();
        error.Code = "DuplicateUserName";
        error.Description = $"{userName} kullanıcı adı kaydedilmiş!";
        return error;
    }
}