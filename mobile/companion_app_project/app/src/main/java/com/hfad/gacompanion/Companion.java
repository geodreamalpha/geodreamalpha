package com.hfad.gacompanion;

import java.util.Objects;

public class Companion {

    private String level;
    private String strength;
    private String speed;
    private int lvl;
    private int str;
    private int spd;

    // No-argument constructor is required to support conversion of Firestore document to POJO
    public Companion() {  }

    public Companion(String level, String strength, String speed){
        this.level = level;
        this.strength = strength;
        this.speed = speed;
        this.lvl = Integer.parseInt(level);
        this.str = Integer.parseInt(strength);
        this.spd = Integer.parseInt(speed);
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

    @Override
    public String toString() {
        return "Companion{" +
                "level='" + level + '\'' +
                ", strength='" + strength + '\'' +
                ", speed='" + speed + '\'' +
                ", lvl=" + lvl +
                ", str=" + str +
                ", spd=" + spd +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Companion companion = (Companion) o;
        return lvl == companion.lvl &&
                str == companion.str &&
                spd == companion.spd &&
                Objects.equals(level, companion.level) &&
                Objects.equals(strength, companion.strength) &&
                Objects.equals(speed, companion.speed);
    }

    @Override
    public int hashCode() {
        return Objects.hash(level, strength, speed, lvl, str, spd);
    }
}
