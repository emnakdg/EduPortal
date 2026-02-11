using EduPortal.Application.Common.Interfaces;
using EduPortal.Application.DTOs;
using EduPortal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Features.Students.Queries;

public record GetAllStudentsQuery() : IRequest<List<StudentDto>>;

