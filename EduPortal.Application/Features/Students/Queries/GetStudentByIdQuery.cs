using EduPortal.Application.DTOs;
using MediatR;

namespace EduPortal.Application.Features.Students.Queries;

public record GetStudentByIdQuery(int Id) : IRequest<StudentDto?>;