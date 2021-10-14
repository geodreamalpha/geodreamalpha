package com.hfad.gacompanion;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

import com.google.firebase.auth.FirebaseAuth;

//Aaron Schwartz
public class UserHomepageActivity extends AppCompatActivity {


    public static final String FIRST_TIME = "first time";
    private static final String TAG = "UserHomepageActivity";
    private FirebaseAuth mAuth;
    private boolean first;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_user_homepage);

        Intent createdIntent = getIntent();
        this.first = createdIntent.getBooleanExtra(FIRST_TIME, false);
        Log.d(TAG, "first value" + first);
    }
}