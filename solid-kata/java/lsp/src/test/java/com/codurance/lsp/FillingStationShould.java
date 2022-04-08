package com.codurance.lsp;

import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.catchThrowable;

class FillingStationShould {
    private static final int FULL = 100;
    private final FillingStation fillingStation = new FillingStation();

    @Test
    void refuel_a_petrol_car() {
        PetrolCar car = new PetrolCar();

        fillingStation.refuel(car);

        assertThat(car.fuelTankLevel())
                .isEqualTo(FULL);
    }


    @Test
    void not_fail_refueling_an_electric_car() {
        ElectricCar car = new ElectricCar();

        Throwable throwable = catchThrowable(() -> fillingStation.refuel(car));

        assertThat(throwable)
                .isNull();
    }


    @Test
    void recharge_an_electric_car() {
        ElectricCar car = new ElectricCar();

        fillingStation.charge(car);

        assertThat(car.batteryLevel())
                .isEqualTo(FULL);
    }


    @Test
    void not_fail_recharging_a_petrol_car() {
        PetrolCar car = new PetrolCar();

        Throwable throwable = catchThrowable(() -> fillingStation.charge(car));

        assertThat(throwable)
                .isNull();
    }
}
