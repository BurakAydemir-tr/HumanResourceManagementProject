using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string NationalIdAlreadyExists = "Bu TC Kimlik numaralı biri bulunmaktadır.";
        public static string EmailAlreadyExists="Bu email adresi bulunmaktadır.";
        public static string CandidateAdded="Aday eklendi.";
        public static string PhoneNumberAlreadyExists="Bu telefon numarası bulunmaktadır.";
        public static string PositionAlreadyExists="Bu iş pozisyonu bulunmaktadır.";
        public static string EmailAndWebsiteDifferent="Mail ile web site domainleri farklı";
        public static string WebAddressNotSuitable="Web adresi uygun değil.";
        public static string JobAdvertisementAdded="İş ilanı eklendi";
        public static string JobAdvertisementDeleted="İş ilanı silindi";
        public static string JobAdvertisementListed="İş ilanları listelendi";
        public static string JobAdvertisementUpdated="İş ilanı güncellendi";
        public static string JobAdvertisementGet="İş ilanı getirildi";
        public static string UniversityAdded="Universite eklendi";
        public static string UniversityDeleted="Universite silindi";
        public static string UniversityUpdated="Universite güncellendi";
        public static string UniversityListedByCandidate="Adayın okuduğu okullar listelendi";
        public static string JobExperienceAdded="İş tecrübesi eklendi";
        public static string JobExperienceUpdated="İş tecrübesi güncellendi";
        public static string JobExperienceListedByCandidate="Adayın iş tecrübeleri listelendi.";
        public static string JobExperienceDeleted="İş tecrübesi silindi";
        public static string CVAdded="Özgeçmiş eklendi";
        public static string CVDeleted="Özgeçmiş silindi";
        public static string CVUpdated="Özgeçmiş güncellendi";
        public static string TechnologyAdded="Teknoloji eklendi";
        public static string TechnologyUpdated="Teknoloji güncellendi";
        public static string TechnologyListedByCV="Teknoloji listelendi";
        public static string TechnologyDeleted="Teknoloji silindi";
        public static string InvalidFileExtension="Geçersiz dosya uzantısı";
        public static string UserAlreadyExists="Kullanıcı mevcut";
        public static string CandidateNotFound="Aday bulunamadı";
        public static string PasswordError="Hatalı şifre";
        public static string SuccessfulLogin="Giriş başarılı";
        public static string EmployerNotFound="İşveren bulunamadı";
        public static string EmployerAdded="İş veren eklendi";
        public static string UserNotFound="Kullanıcı bulunamadı";
        public static string UserUpdate="Kullanıcı güncellendi";
        public static string ResetPassword="Şifrenizi size gönderilen mail üzerinden yenileyebilirsiniz.";
        public static string InvalidToken="Geçersiz Token";
        public static string PasswordUpdate="Şifreniz yenilendi.";
        public static string OperationClaimAdded="Operation Claim Eklendi";
        public static string OperationClaimUpdated = "Operation Claim Güncellendi";
        public static string OperationClaimDeleted = "Operation Claim Silindi";
        public static string OperationClaimAlreadyExists = "Operation Claim bulunmaktadır.";
    }
}
