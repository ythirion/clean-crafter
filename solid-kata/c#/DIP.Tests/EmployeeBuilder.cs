using System;
using DIP;

public class EmployeeBuilder
{
    private DateOnly? _birthDate;
    private static DateOnly DefaultBirthDate => new(1987, 9, 10);
    
    public static EmployeeBuilder AnEmployee() => new EmployeeBuilder();

    public EmployeeBuilder BornThe(int year, int month, int day)
    {
        _birthDate = new DateOnly(year, month, day);
        return this;
    }

    public Employee Build() => new Employee("John", "Doe", "john.doe@foobar.com", _birthDate ?? DefaultBirthDate);
}