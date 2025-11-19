using GoVisitAPI.Helpers;
using System.ComponentModel.DataAnnotations;

namespace GoVisitAPI.ViewModel
{
    public class SignUpRequest
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Phone { get; set; }

        public string? Email { get; set; }
        public string? PreferredLanguage { get; set; }
    }

    public class SignInRequest
    {
        [Required]
        public string Phone { get; set; }
    }

    public class CreateAppointmentRequest
    {
        [Required]
        public int InviteeId { get; set; }

        [Required]
        public int TimeSlotId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public int? StaffId { get; set; }
        public string? Notes { get; set; }
    }

    public class UpdateAppointmentRequest
    {
        public string? Notes { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; }
    }


}
