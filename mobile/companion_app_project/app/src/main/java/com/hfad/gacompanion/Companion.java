package com.hfad.gacompanion;

public class Companion {

    private int level;
    private int strength;
    private int speed;

    public Companion(int level, int strength, int speed){
        this.level = level;
        this.strength = strength;
        this.speed = speed;
    }

    public int getLevel() {
        return level;
    }

    public int getStrength() {
        return strength;
    }

    public int getSpeed() {
        return speed;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public void setStrength(int strength) {
        this.strength = strength;
    }

    public void setSpeed(int speed) {
        this.speed = speed;
    }
}
