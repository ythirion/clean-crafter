package com.codurance.lsp;

public class PetrolCar extends Vehicle implements PetrolPowered {
  private static final int FUEL_TANK_FULL = 100;
  private int fuelTankLevel = 0;

  @Override
  public void fillUpWithFuel() {
    this.fuelTankLevel = FUEL_TANK_FULL;
  }

  public int fuelTankLevel() {
    return fuelTankLevel;
  }
}
