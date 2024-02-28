using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Constants
{
    public class SubjectMessages
    {
        public const string SubjectExists = "Konu Zaten Mevcut!";
        public const string SubjectDoesExists = "Konu Bulunamadı!";
        public const string UserIdDontMatch = "Sadece Kendi Konunu Güncelleyebilirsin!";
        public const string SubjectForUpdateIsNull = "Güncellenecek bir şey bulunamadı!";
    }
}
