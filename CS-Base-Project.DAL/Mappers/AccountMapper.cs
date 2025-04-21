﻿using AutoMapper;
using CS_Base_Project.DAL.Data.Entities;
using CS_Base_Project.DAL.Data.RequestDTO.Accounts;
using CS_Base_Project.DAL.Data.ResponseDTO.Accounts;

namespace CS_Base_Project.DAL.Mappers;

public class AccountMapper : Profile
{
    public AccountMapper()
    {
        // Request
        CreateMap<CreateAccountRequestDTO, AccountEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<UpdateAccountRequestDTO, AccountEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.RoleId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        
        // Response
        CreateMap<AccountEntity, GetAccountResponseDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RoleEntity.Name));
        
    }
}