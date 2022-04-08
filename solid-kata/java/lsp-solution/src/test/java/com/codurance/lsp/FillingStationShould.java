package com.codurance.lsp;

import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;

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
    void recharge_a_VWDiesel() {
        VWDieselCar car = new VWDieselCar();

        fillingStation.refuel(car);

        assertThat(car.fuelTankLevel())
                .isEqualTo(FULL);
    }

    @Test
    void recharge_an_electric_car() {
        ElectricCar car = new ElectricCar();

        fillingStation.charge(car);

        assertThat(car.batteryLevel())
                .isEqualTo(FULL);
    }

    @Test
    void recharge_a_Tesla() {
        TeslaCar car = new TeslaCar();

        fillingStation.charge(car);

        assertThat(car.batteryLevel())
                .isEqualTo(FULL);
    }
}
