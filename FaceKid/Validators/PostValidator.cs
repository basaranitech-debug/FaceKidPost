using FaceKid.ViewModels;
using FluentValidation;

namespace FaceKid.Validators
{
    public class PostValidator : AbstractValidator<CreatePostViewModel>
    {
        public PostValidator() 
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Post içeriği boş olamaz!").MaximumLength(500).WithMessage("Post en fazla 500 karakterden oluşabilir.");

        }
    }
}
