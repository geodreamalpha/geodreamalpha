package com.hfad.gacompanion;

import android.view.View;

//Aaron Schwartz
public class UserManager {

    private String username;
    private String password;
    private Companion userCompanion;

    public UserManager(String username, String password){
        this.username = username;
        this.password = password;
    }

    public String hello(){
        return "Hello from UserManager!";
    }

    public String getUsername() {
        return username;
    }

    public String getPassword() {
        return password;
    }

    public Companion getUserCompanion() {
        return userCompanion;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public void setUserCompanion(Companion userCompanion) {
        this.userCompanion = userCompanion;
    }
}
