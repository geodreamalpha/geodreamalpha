package com.hfad.gacompanion;

import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.view.GravityCompat;
import androidx.drawerlayout.widget.DrawerLayout;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ActionMenuView;
import android.widget.Toast;

import com.google.android.material.navigation.NavigationView;
import com.google.android.material.snackbar.Snackbar;
import com.google.firebase.auth.FirebaseAuth;

//Aaron Schwartz
public class UserHomepageActivity extends AppCompatActivity implements NavigationView.OnNavigationItemSelectedListener {


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

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar,
                R.string.nav_open_drawer, R.string.nav_close_drawer);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);

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

    public boolean onNavigationItemSelected(MenuItem item){
        Intent intent = null;
        switch(item.getItemId()){
            case R.id.logout:
                this.signOut();
                break;
            case R.id.nav_str_minigame:
                intent = new Intent(this, StrengthMinigameActivity.class);
                break;
            case R.id.nav_spd_challenge:
                intent = new Intent(this, WalkingChallengeActivity.class);
                break;
        }

        if(intent != null) startActivity(intent);

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }


    public void signOut() {
        mAuth.signOut();
        Intent intent = new Intent(this, LogInActivity.class);
        startActivity(intent);
    }

    @Override
    public void onBackPressed(){
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)){
            drawer.closeDrawer(GravityCompat.START);
        } else {
            //Do nothing; we do not want to automatically log user out
        }
    }



}