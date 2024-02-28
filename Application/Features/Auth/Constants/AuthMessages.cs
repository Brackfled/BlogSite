using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Constants
{
    public static class AuthMessages
    {

        public static string UserDontExists = "Kullanıcı Mevcut Değil!";
        public static string UserMailAlreadyExists = "Kullanıcı Emaili Mevcut Değil";
        public static string PasswordDontMatched = "Hatalı Şifre!";
        public static string RefreshDontExists = "Refresh Token Boş Olamaz!";
        public static string InvalidRefreshToken = "Tanımlanamayan RefreshToken!";
    }
}
