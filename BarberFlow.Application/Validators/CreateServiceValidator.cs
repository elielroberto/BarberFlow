using BarberFlow.Application.DTOs.Service;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.Validators
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceDto>
    {
        public CreateServiceValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório").MaximumLength(100);

            RuleFor(x => x.SlotCount).GreaterThan(0).WithMessage("Quantidade de slots deve ser maior que 0");
            RuleFor(x => x.SlotCount).GreaterThan(0) .LessThanOrEqualTo(4).WithMessage("Serviço pode ter no máximo 4 slots");

            RuleFor(x => x.Price) .GreaterThan(0).WithMessage("Preço deve ser maior que 0");
        }
    }
}
