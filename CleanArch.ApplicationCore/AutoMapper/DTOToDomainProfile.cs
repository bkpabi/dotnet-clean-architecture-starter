using AutoMapper;
using CleanArch.ApplicationCore.DTOs;
using CleanArch.Domain.Entities.BankAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ApplicationCore.AutoMapper;

public class DTOToDomainProfile : Profile
{
    public DTOToDomainProfile()
    {
        CreateMap<BankAccountDTO, BankAccount>();
    }
}
