package com.hfad.gacompanion;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Toast;

import com.google.android.material.snackbar.Snackbar;
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

        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        mAuth = FirebaseAuth.getInstance();

        Intent createdIntent = getIntent();
        this.first = createdIntent.getBooleanExtra(FIRST_TIME, false);
        Log.d(TAG, "first value" + first);
    }

//    @Override
//    protected void onStart() {
//        super.onStart();
//        if(this.first){
//            CharSequence addedMsg = "Welcome, " + this.mAuth.getCurrentUser().getEmail() + "!";
//            int duration = Snackbar.LENGTH_SHORT;
//            Snackbar snackbar = Snackbar.make(findViewById(R.id.homepage_temp_text), addedMsg, duration);
//            snackbar.setAction("Log Out", new View.OnClickListener(){
//                @Override
//                public void onClick(View view) {
//                    signOut();
//                    Toast toast = Toast.makeText(UserHomepageActivity.this,
//                            "Logged Out", Toast.LENGTH_SHORT);
//                    toast.show();
//
//                }
//            });
//            snackbar.show();
//            this.first = false;
//        }
//
//    }
//
//
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu_with_logout, menu);
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item){
        switch (item.getItemId()){
            case R.id.action_logout:
                this.signOut();
            default:
                return super.onOptionsItemSelected(item);
        }
    }


    public void signOut() {
        mAuth.signOut();
        Intent intent = new Intent(this, LogInActivity.class);
        startActivity(intent);
    }

}