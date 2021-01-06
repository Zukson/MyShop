using FluentValidation;
using MyShop.API.Contracts.V1.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.API.Validators
{
    public class PostProductRequestValidator : AbstractValidator<PostProductRequest>
    {
        public PostProductRequestValidator()
        {
            RuleFor(x => x.Description)
                .Length(20,120);
            RuleFor(x => x.Name)
                .NotNull()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
