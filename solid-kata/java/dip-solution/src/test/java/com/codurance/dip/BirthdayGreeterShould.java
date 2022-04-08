package com.codurance.dip;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.ArgumentCaptor;
import org.mockito.Captor;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.jupiter.MockitoExtension;

import java.time.MonthDay;
import java.util.Collections;

import static com.codurance.dip.EmployeeBuilder.anEmployee;
import static org.assertj.core.api.Assertions.assertThat;
import static org.mockito.BDDMockito.given;
import static org.mockito.Mockito.verify;

@ExtendWith(MockitoExtension.class)
class BirthdayGreeterShould {
    private static final int CURRENT_MONTH = 7;
    private static final int CURRENT_DAY_OF_MONTH = 9;
    private static final MonthDay TODAY = MonthDay.of(CURRENT_MONTH, CURRENT_DAY_OF_MONTH);

    @Mock
    private EmployeeRepository employeeRepository;
    @Mock
    private Clock clock;
    @Mock
    private EmailSender emailSender;
    @Captor
    private ArgumentCaptor<Email> emailArgumentCaptor;

    @Test
    void should_send_greeting_email_to_employee() {
        BirthdayGreeter birthdayGreeter = new BirthdayGreeter(employeeRepository, clock, emailSender);

        given(clock.monthDay()).willReturn(TODAY);
        Employee employee = anEmployee().build();
        given(employeeRepository.findEmployeesBornOn(MonthDay.of(CURRENT_MONTH, CURRENT_DAY_OF_MONTH))).willReturn(Collections.singletonList(employee));

        birthdayGreeter.sendGreetings();

        verify(emailSender, Mockito.times(1)).send(emailArgumentCaptor.capture());
        Email sentEmail = emailArgumentCaptor.getValue();

        assertThat(employee.getEmail()).isEqualTo(sentEmail.getTo());
        assertThat(sentEmail.getSubject()).isEqualTo("Happy birthday!");
        assertThat(sentEmail.getMessage()).isEqualTo("Happy birthday, dear John!");
    }
}