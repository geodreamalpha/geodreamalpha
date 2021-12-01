package com.hfad.gacompanion;

import java.util.Objects;

public class Companion {

    private String level;
    private String strength;
    private String speed;
    private int lvl;
    private int str;
    private int spd;

    private String currentLifts;
    private String liftsNeeded;

    private String currentSteps;
    private String stepsNeeded;

    // No-argument constructor is required to support conversion of Firestore document to POJO
    public Companion() {  }

    public Companion(String level, String strength, String speed, String currentLifts, String liftsNeeded,
                     String currentSteps, String stepsNeeded){
        this.level = level;
        this.strength = strength;
        this.speed = speed;
        this.lvl = Integer.parseInt(level);
        this.str = Integer.parseInt(strength);
        this.spd = Integer.parseInt(speed);
        this.currentLifts = currentLifts;
        this.liftsNeeded = liftsNeeded;
        this.currentSteps = currentSteps;
        this.stepsNeeded = stepsNeeded;
    }

    public void convertStats(){
        this.lvl = Integer.parseInt(this.level);
        this.str = Integer.parseInt(this.strength);
        this.spd = Integer.parseInt(this.speed);
    }

    public String getLevel() {
        return level;
    }

    public String getStrength() {
        return strength;
    }

    public String getSpeed() {
        return speed;
    }

    public void setLevel(String level) {
        this.level = level;
    }

    public void setStrength(String strength) {
        this.strength = strength;
    }

    public void setSpeed(String speed) { this.speed = speed; }

    public int getLvl() {
        return lvl;
    }

    public void setLvl(int lvl) {
        this.lvl = lvl;
    }

    public int getStr() {
        return str;
    }

    public void setStr(int str) {
        this.str = str;
    }

    public int getSpd() {
        return spd;
    }

    public void setSpd(int spd) {
        this.spd = spd;
    }

    public String getCurrentLifts() {
        return currentLifts;
    }

    public void setCurrentLifts(String currentLifts) {
        this.currentLifts = currentLifts;
    }

    public String getLiftsNeeded() {
        return liftsNeeded;
    }

    public void setLiftsNeeded(String liftsNeeded) {
        this.liftsNeeded = liftsNeeded;
    }

    public String getCurrentSteps() {
        return currentSteps;
    }

    public void setCurrentSteps(String currentSteps) {
        this.currentSteps = currentSteps;
    }

    public String getStepsNeeded() {
        return stepsNeeded;
    }

    public void setStepsNeeded(String stepsNeeded) {
        this.stepsNeeded = stepsNeeded;
    }
}
