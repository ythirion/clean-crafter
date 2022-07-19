namespace DIP
{
    public class BirthdayGreeter
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClock _clock;

        public BirthdayGreeter(IEmployeeRepository employeeRepository, IClock clock)
        {
            _employeeRepository = employeeRepository;
            _clock = clock;
        }

        public void SendGreetings()
        {
            var today = _clock.Today();

            _employeeRepository
                .FindEmployeesBornOn(today.Month, today.Day)
                .Select(EmailFor)
                .ToList()
                .ForEach(email => new EmailSender().Send(email));
        }

        private Email EmailFor(Employee employee) =>
            new(employee.Email,
                "Happy birthday!",
                $"Happy birthday, dear {employee.FirstName}!");
    }
}