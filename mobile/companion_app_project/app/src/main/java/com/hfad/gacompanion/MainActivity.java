package com.hfad.gacompanion;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        UserManager userManager = new UserManager("x", "y");
        TextView userManagerTextView = findViewById(R.id.userManagerHelloText);
        userManagerTextView.setText(userManager.hello());

    }

    public void onClickNavigateToLogin(View view){
        Intent intent = new Intent(this, LogInActivity.class);
        startActivity(intent);
    }

    public void onClickNavigateToUserHomepage(View view){
        Intent intent = new Intent(this, UserHomepageActivity.class);
        startActivity(intent);
    }

    public void onClickNavigateToStrengthMinigame(View view){
        Intent intent = new Intent(this, StrengthMinigameActivity.class);
        startActivity(intent);
    }

    public void onClickNavigateToWalkingChallenge(View view){
        Intent intent = new Intent(this, WalkingChallengeActivity.class);
        startActivity(intent);
    }
}