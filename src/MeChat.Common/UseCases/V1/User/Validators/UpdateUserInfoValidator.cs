using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MeChat.Common.UseCases.V1.User.Validators;
public class UpdateUserInfoValidator : AbstractValidator<Command.UpdateUserInfo>
{
    public UpdateUserInfoValidator()
    {
        RuleFor(x => x.Fullname).NotEmpty();
        //RuleFor(x => x.Avatar)
        //    .NotNull().WithMessage("Please upload a file.")
        //    .Must(BeAValidFile).WithMessage("Invalid file type.")
        //    .Must(BeWithinFileSizeLimit).WithMessage("File size exceeds the 2MB limit.");
    }
    /*
    private bool BeAValidFile(IFormFile file)
    {
        if (file == null)
            return false;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
        var extension = Path.GetExtension(file.FileName)?.ToLower();
        return allowedExtensions.Contains(extension);
    }

    private bool BeWithinFileSizeLimit(IFormFile file)
    {
        const int maxFileSize = 2 * 1024 * 1024; // 2MB
        return file.Length <= maxFileSize;
    }
    */
}
