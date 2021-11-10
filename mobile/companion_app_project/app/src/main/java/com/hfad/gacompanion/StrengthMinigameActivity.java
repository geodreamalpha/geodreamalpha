//Aaron Schwartz

package com.hfad.gacompanion;

import androidx.annotation.NonNull;
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
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.android.material.navigation.NavigationView;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.firestore.DocumentReference;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.FirebaseFirestore;

public class StrengthMinigameActivity extends AppCompatActivity implements NavigationView.OnNavigationItemSelectedListener {

    private static final String TAG = "StrMinigameActivity";
    private FirebaseAuth mAuth;
    private int currentNumLifts = 0;
    private boolean lifted = false;
    private int numLiftsNeeded = 10;
    private ImageView wolfPicture;
    private ProgressBar progressBar;

    private final FirebaseFirestore mDb = FirebaseFirestore.getInstance();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_strength_minigame);

        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        mAuth = FirebaseAuth.getInstance();

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar,
                R.string.nav_open_drawer, R.string.nav_close_drawer);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);

        Log.d(TAG, mAuth.getCurrentUser().getUid());

        DocumentReference compStats = mDb.collection("users")
                .document(mAuth.getCurrentUser().getUid())
                .collection("compStats")
                .document("0");

        compStats.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
            @Override
            public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                if (task.isSuccessful()) {
                    DocumentSnapshot document = task.getResult();
                    Log.d(TAG, "Successfully got into the onComplete");
                    if (document.exists()){
                        Log.d(TAG, "Successfully got companion document and stats");
                        final Companion companion = document.toObject(Companion.class);
                        updateUI(companion);
                    } else {
                        Log.d(TAG, "No such document");
                    }
                } else {
                    Log.d(TAG, "get failed with ", task.getException());
                }

            }
        });


        this.progressBar = findViewById(R.id.lift_progressbar);
        progressBar.setProgress(this.currentNumLifts);
        progressBar.setMax(this.numLiftsNeeded);

        this.wolfPicture = findViewById(R.id.companion_image);


    }

    private void updateUI(Companion companion) {
        TextView comp_str = (TextView) findViewById(R.id.strength);
        comp_str.setText(companion.getStrength());
    }

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
            case R.id.navigate_to_user_homepage:
                intent = new Intent(this, UserHomepageActivity.class);
                break;
            case R.id.logout:
                this.signOut();
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
            finish();
        }
    }

    public void OnBuffButtonPress(View view){
        if(this.lifted) {
            this.wolfPicture.setImageResource(R.drawable.temp_wolf);
            this.currentNumLifts++;
            this.progressBar.setProgress(currentNumLifts);
            this.lifted = false;
        } else {
            this.wolfPicture.setImageResource(R.drawable.temp_wolf_flipped);
            this.lifted = true;
        }
    }

}

