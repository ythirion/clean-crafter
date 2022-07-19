using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace DIP.Tests
{
    public class BirthdayGreeterShould
    {
        private const int CurrentMonth = 7;
        private const int CurrentDayOfMonth = 9;
        private const int CurrentYear = 2022;

        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IClock> _clockMock;

        private readonly BirthdayGreeter _birthdayGreeter;
        private readonly StringWriter _consoleContent;

        public BirthdayGreeterShould(ITestOutputHelper testOutputHelper)
        {
            _consoleContent = new StringWriter();
            Console.SetOut(_consoleContent);

            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _clockMock = new Mock<IClock>();
            _birthdayGreeter = new BirthdayGreeter(_employeeRepositoryMock.Object, _clockMock.Object);
        }

        [Fact]
        public void Send_Greeting_Email_To_Employees()
        {
            var employee = EmployeeBuilder
                .AnEmployee()
                .BornThe(1980, CurrentMonth, CurrentDayOfMonth)
                .Build();

            _clockMock.Setup(c => c.Today())
                .Returns(new DateOnly(CurrentYear, CurrentMonth, CurrentDayOfMonth));

            _employeeRepositoryMock.Setup(r => r.FindEmployeesBornOn(CurrentMonth, CurrentDayOfMonth))
                .Returns(new List<Employee> {employee});

            _birthdayGreeter.SendGreetings();

            _consoleContent
                .ToString()
                .Should()
                .Be("To:john.doe@foobar.com, Subject: Happy birthday!, Message: Happy birthday, dear John!");
        }
    }
}