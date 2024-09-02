﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Register
{
    public sealed class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail bilgisi boş olamaz");
            RuleFor(p => p.Email).NotNull().WithMessage("Mail bilgisi boş olamaz");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.");

            RuleFor(p => p.Username).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(p => p.Username).NotNull().WithMessage("Kullanıcı adı  boş olamaz");
            RuleFor(p => p.Username).MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakterolamaz");

            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre boş olamaz");
            RuleFor(p => p.Password).NotNull().WithMessage("Şifre  boş olamaz");
            RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Şifre en az 1 adet büyük harf içermelidir.");
            RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifre en az 1 adet küçük harf içermelidir.");
            RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Şifre en az 1 adet rakam içermelidir.");
            RuleFor(p => p.Password).Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az 1 adet özel karakter içermelidir.");



        }
    }
}
