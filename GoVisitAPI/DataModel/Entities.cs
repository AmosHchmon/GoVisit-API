using GoVisitAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoVisitAPI.DataModel
{
    public class PublicOrganization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrganizationId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
        public string? Website { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Department> Departments { get; set; }
    }

    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }

        [Required]
        public int OrganizationId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(OrganizationId))]
        public PublicOrganization Organization { get; set; }

        public ICollection<Service> Services { get; set; }
        public ICollection<StaffMember> StaffMembers { get; set; }
        public ICollection<Calendar> Calendars { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }

    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public int DurationMinutes { get; set; } = 20;
        public bool RequiresDocuments { get; set; }
        public string? RequiredDocuments { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

        public ICollection<TimeSlot> TimeSlots { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }

    public class StaffMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Role { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

        public ICollection<Calendar> Calendars { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }

    public class Invitee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InviteeId { get; set; }

        [Required]
        public string FullName { get; set; }

        public string? Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public string? IdNumber { get; set; }
        public string? PreferredLanguage { get; set; }

        public bool NotificationsEnabled { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }

    public class Calendar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CalendarId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public int? ServiceId { get; set; }
        public int? StaffId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool IsClosed { get; set; } = false;
        public string? Notes { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public Service? Service { get; set; }

        [ForeignKey(nameof(StaffId))]
        public StaffMember? Staff { get; set; }

        public ICollection<TimeSlot> TimeSlots { get; set; }
    }

    public class TimeSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimeSlotId { get; set; }

        [Required]
        public int CalendarId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int? AppointmentId { get; set; }

        [ForeignKey(nameof(CalendarId))]
        public Calendar Calendar { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public Service Service { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }

    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }

        [Required]
        public int InviteeId { get; set; }

        public int? AppointmentId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime? SentAt { get; set; }
        public string? Status { get; set; }

        [ForeignKey(nameof(InviteeId))]
        public Invitee Invitee { get; set; }
    }

    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }

        [Required]
        public int InviteeId { get; set; }

        [Required]
        public int TimeSlotId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public int? StaffId { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
        public DateTime ScheduledFor { get; set; }

        [ForeignKey(nameof(InviteeId))]
        public Invitee Invitee { get; set; }

        [ForeignKey(nameof(TimeSlotId))]
        public TimeSlot TimeSlot { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public Service Service { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }

        [ForeignKey(nameof(StaffId))]
        public StaffMember? Staff { get; set; }
    }



}
