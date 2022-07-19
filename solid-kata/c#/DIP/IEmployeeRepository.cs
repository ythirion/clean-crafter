namespace DIP
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> FindEmployeesBornOn(int month, int dayOfMonth);
    }
}