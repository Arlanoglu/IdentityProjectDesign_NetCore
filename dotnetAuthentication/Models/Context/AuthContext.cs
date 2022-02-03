
using dotnetAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
/*
İhtiyac duyulan kütüphaneler
//Microsoft.EntityFrameworkCore
//Microsoft.AspNetCore.Identity.EntityframeworkCore
//Microsoft.EntityFrameworkCore.SqlServer

dotnet ef komutunu çalıştırmak için aşağıdaki kütüphanein kurulu olması gerekmektedir

dotnet tool install --global dotnet-ef

Sertifika oluşturmak için;
dotnet dev-certs https --trust

*/
public class AuthContext : IdentityDbContext<IdentityUser>
{
    public AuthContext(DbContextOptions<AuthContext> options) : base(options)
    {

    }

}