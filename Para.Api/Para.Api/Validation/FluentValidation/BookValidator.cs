using FluentValidation;
using Para.Api;


namespace Para.Api.Validations.FluentValidation
{
    public class BookValidatior:AbstractValidator<Book>
    {
        public BookValidatior()
        {
            RuleFor(x => x.Id).GreaterThan(1).LessThan(10000).NotNull().WithMessage("Birincil anahtar null geçilemez");
            RuleFor(x => x.Name).MinimumLength(5).MaximumLength(50).NotNull().WithMessage("Author alanı boş bırakılamaz !.");
            RuleFor(x => x.PageCount).GreaterThan(50).LessThan(400).WithMessage("Sayfa sayisi 50'den büyük 400'den küçük olmalı !.");
            RuleFor(x => x.Year).GreaterThan(1900).LessThan(2024).NotNull().WithMessage("Yıl 1900'den küçük 2024'den büyük olmalı!.");
        }
    }
}