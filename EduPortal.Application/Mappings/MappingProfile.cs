using AutoMapper;
using EduPortal.Application.DTOs;
using EduPortal.Domain.Entities;
using EduPortal.Domain.Enums;

namespace EduPortal.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Teacher, TeacherDto>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.User.FullName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email ?? ""))
                .ForMember(d => d.ClassCount, opt => opt.MapFrom(s => s.Classes.Count))
                .ForMember(d => d.StudentCount, opt => opt.MapFrom(s => s.Classes.SelectMany(c => c.Students).Count()));

            CreateMap<Student, StudentDto>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.User.FullName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email ?? ""))
                .ForMember(d => d.ClassName, opt => opt.MapFrom(s => s.Class != null ? s.Class.Name : null))
                .ForMember(d => d.CompletedAssignments, opt => opt.MapFrom(s => s.StudentAssignments.Count(sa => sa.Status == AssignmentStatus.Completed)))
                .ForMember(d => d.PendingAssignments, opt => opt.MapFrom(s => s.StudentAssignments.Count(sa => sa.Status == AssignmentStatus.Pending)))
                .ForMember(d => d.SuccessRate, opt => opt.MapFrom(s =>
                    s.StudentAssignments.Any(sa => sa.Score.HasValue)
                        ? s.StudentAssignments.Where(sa => sa.Score.HasValue).Average(sa => sa.Score!.Value)
                        : 0));

            CreateMap<Class, ClassDto>()
                .ForMember(d => d.TeacherName, opt => opt.MapFrom(s => s.Teacher.User.FullName))
                .ForMember(d => d.StudentCount, opt => opt.MapFrom(s => s.Students.Count));

            CreateMap<Assignment, AssignmentDto>()
                .ForMember(d => d.TopicName, opt => opt.MapFrom(s => s.Topic.Name))
                .ForMember(d => d.SubjectName, opt => opt.MapFrom(s => s.Topic.Unit.Subject.Name))
                .ForMember(d => d.TeacherName, opt => opt.MapFrom(s => s.Teacher.User.FullName))
                .ForMember(d => d.AssignedStudentCount, opt => opt.MapFrom(s => s.StudentAssignments.Count));

            CreateMap<Subject, SubjectDto>()
                .ForMember(d => d.UnitCount, opt => opt.MapFrom(s => s.Units.Count))
                .ForMember(d => d.TopicCount, opt => opt.MapFrom(s => s.Units.SelectMany(u => u.Topics).Count()))
                .ForMember(d => d.Units, opt => opt.MapFrom(s => s.Units.OrderBy(u => u.OrderIndex)));

            CreateMap<Unit, UnitDto>()
                .ForMember(d => d.Topics, opt => opt.MapFrom(s => s.Topics.OrderBy(t => t.OrderIndex)));

            CreateMap<Topic, TopicDto>();
        }
    }
}
