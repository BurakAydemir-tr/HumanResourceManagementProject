using FluentValidation;
using FluentValidation.Results;

namespace WebAPI.Validation
{
    public static class ValidationHelper
    {
        public static void Validate(Type type, object[] items)
        {
            //verilen tip ile validator oluşturma durumu kontrol ediliyor.
            if (!typeof(IValidator).IsAssignableFrom(type))
            {
                throw new Exception("Hata: Validator tipi geçersiz!");
            }

            var validator = (IValidator)Activator.CreateInstance(type);

            //gelen tüm argumanlar için validasyon yapılacak.
            foreach (var item in items)
            {
                //arguman tipi valide edilebiliyor mu bakılıyor.
                if (validator.CanValidateInstancesOfType(item.GetType()))
                {
                    var result = validator.Validate(new ValidationContext<object>(item));

                    if (!result.IsValid)
                    {
                        throw new ValidationException(result.Errors);
                    }
                }
            }
        }

        public static ValidationResult Validate(Type type, Object item)
        {
            //verilen tip ile validator oluşturma durumu kontrol ediliyor.
            if (!typeof(IValidator).IsAssignableFrom(type))
                throw new Exception("Hata: Validator tipi geçersiz!");
            var validator = (IValidator)Activator.CreateInstance(type);

            //valid veya valid olmama durumunun tamamı result olarak dönülür.
            return validator.Validate(new ValidationContext<object>(item));
        }
    }
}
