using GoVisitAPI.DataModel;
using GoVisitAPI.Helpers;
using GoVisitAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoVisitAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController(GoVisitContext context) : ControllerBase
    {
        [HttpGet("my/{inviteeId}")]
        public IActionResult GetMy(int inviteeId)
        {
            var list = context.Appointments
                .Where(x => x.InviteeId == inviteeId)
                .OrderBy(x => x.ScheduledFor)
                .ToList();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = context.Appointments.FirstOrDefault(x => x.AppointmentId == id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(CreateAppointmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var invitee = context.Invitees.FirstOrDefault(x => x.InviteeId == request.InviteeId);
            if (invitee == null)
                return BadRequest("Invitee not found");

            var service = context.Services.FirstOrDefault(x => x.ServiceId == request.ServiceId);
            if (service == null)
                return BadRequest("Service not found");

            var department = context.Departments.FirstOrDefault(x => x.DepartmentId == request.DepartmentId);
            if (department == null)
                return BadRequest("Department not found");

            var timeSlot = context.TimeSlots.FirstOrDefault(x => x.TimeSlotId == request.TimeSlotId);
            if (timeSlot == null || !timeSlot.IsAvailable)
                return BadRequest("TimeSlot not available");

            var appointment = new Appointment
            {
                InviteeId = request.InviteeId,
                TimeSlotId = request.TimeSlotId,
                ServiceId = request.ServiceId,
                DepartmentId = request.DepartmentId,
                StaffId = request.StaffId,
                Notes = request.Notes,
                ScheduledFor = timeSlot.StartTime,
                Status = AppointmentStatus.Pending
            };

            timeSlot.IsAvailable = false;

            context.Appointments.Add(appointment);
            context.SaveChanges();

            if (appointment.StaffId != null)
            {
                context.Notifications.Add(new Notification
                {
                    InviteeId = appointment.StaffId.Value,
                    AppointmentId = appointment.AppointmentId,
                    Type = "NewAppointment",
                    Message = $"A new appointment has been assigned for {appointment.ScheduledFor}"
                });

                context.SaveChanges();
            }

            return Ok(appointment);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateAppointmentRequest request)
        {
            var item = context.Appointments.FirstOrDefault(x => x.AppointmentId == id);
            if (item == null)
                return NotFound();

            item.Notes = request.Notes;
            item.Status = request.Status;

            context.SaveChanges();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = context.Appointments.FirstOrDefault(x => x.AppointmentId == id);
            if (item == null)
                return NotFound();

            var timeSlot = context.TimeSlots.FirstOrDefault(x => x.TimeSlotId == item.TimeSlotId);
            if (timeSlot != null)
                timeSlot.IsAvailable = true;

            context.Appointments.Remove(item);
            context.SaveChanges();

            return Ok();
        }

        [HttpPost("{id}/approve/{staffId}")]
        public IActionResult Approve(int id, int staffId)
        {
            var appointment = context.Appointments.FirstOrDefault(x => x.AppointmentId == id);
            if (appointment == null)
                return NotFound();

            var staff = context.StaffMembers.FirstOrDefault(x => x.StaffId == staffId);
            if (staff == null)
                return BadRequest("Staff member not found");

            appointment.Status = AppointmentStatus.Approved;
            appointment.StaffId = staffId;

            context.Notifications.Add(new Notification
            {
                InviteeId = appointment.InviteeId,
                AppointmentId = appointment.AppointmentId,
                Type = "AppointmentApproved",
                Message = $"Your appointment on {appointment.ScheduledFor} has been approved"
            });

            context.SaveChanges();

            return Ok(appointment);
        }

    }

}
