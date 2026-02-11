using EduPortal.Application.DTOs;
using MediatR;

namespace EduPortal.Application.Features.Dashboard.Queries;

public record GetAdminDashboardQuery() : IRequest<AdminDashboardDto>;