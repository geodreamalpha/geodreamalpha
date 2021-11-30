package com.hfad.gacompanion;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;
import androidx.core.view.GravityCompat;
import androidx.drawerlayout.widget.DrawerLayout;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
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

//Aaron Schwartz
public class WalkingChallengeActivity extends AppCompatActivity implements NavigationView.OnNavigationItemSelectedListener, SensorEventListener {
    private static final String TAG = "WalkChallengeActivity";
    private FirebaseAuth mAuth;
    private ImageView wolfPicture;
    private ProgressBar progressBar;
    private DocumentReference compStats;

    private final FirebaseFirestore mDb = FirebaseFirestore.getInstance();

    private int level;
    private int speed;
    private int current_steps;

    private SensorManager sensorManager;
    private Sensor myStepCounter;
    private boolean isCounterSensorPresent = false;
    private int prevSteps = -1;
    private int stepCount = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_walking_challenge);

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

        this.progressBar = findViewById(R.id.step_progressbar);

        this.compStats = mDb.collection("users")
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

        this.wolfPicture = findViewById(R.id.companion_image);


        sensorManager = (SensorManager) getSystemService(SENSOR_SERVICE);
        if(sensorManager.getDefaultSensor(Sensor.TYPE_STEP_COUNTER)!=null){
            myStepCounter = sensorManager.getDefaultSensor(Sensor.TYPE_STEP_COUNTER);
            isCounterSensorPresent = true;
            //sensorManager.registerListener(this, myStepCounter, SensorManager.SENSOR_DELAY_NORMAL);
        } else {
            TextView step_count = findViewById(R.id.step_count);
            step_count.setText("Counter Sensor is not present");
            isCounterSensorPresent = false;
        }
    }

    private void updateUI(Companion companion) {
        this.level = Integer.parseInt(companion.getLevel());

        this.speed = Integer.parseInt(companion.getSpeed());
        TextView comp_spd = (TextView) findViewById(R.id.speed);
        comp_spd.setText(companion.getSpeed());

        this.current_steps = Integer.parseInt(companion.getCurrentSteps());
        TextView step_count = findViewById(R.id.step_count);
        step_count.setText(companion.getCurrentSteps());
        this.progressBar.setProgress(Integer.parseInt(companion.getCurrentSteps()));

        TextView num_needed = findViewById(R.id.next_level);
        num_needed.setText(companion.getStepsNeeded());
        this.progressBar.setMax(Integer.parseInt(companion.getStepsNeeded()));
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
            case R.id.nav_str_minigame:
                intent = new Intent(this, StrengthMinigameActivity.class);
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
        compStats.update("currentSteps", Integer.toString(current_steps));
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)){
            drawer.closeDrawer(GravityCompat.START);
        } else {
            finish();
        }
    }




    private void nextLevel(){
        compStats.update("level", Integer.toString(level + 1));
        level++;

        compStats.update("speed", Integer.toString(speed + 1));
        speed++;
        TextView comp_str = (TextView) findViewById(R.id.speed);
        comp_str.setText(Integer.toString(speed));

        compStats.update("currentSteps", Integer.toString(current_steps));

        //int required_steps = ((4 * ((int) Math.pow((speed + 1), 3))) / 5);
        int required_steps = ((4 * ((int) Math.pow(((speed + 1) * 4), 3))) / 5);
        Log.d(TAG, Integer.toString(required_steps));
        compStats.update("stepsNeeded", Integer.toString(required_steps));
        progressBar.setMax(required_steps);
        TextView num_needed = findViewById(R.id.next_level);
        num_needed.setText(Integer.toString(required_steps));
    }

    @Override
    protected void onPause() {
        super.onPause();
        Log.d(TAG, "successfully made to onPause");
        compStats.update("currentSteps", Integer.toString(current_steps));
        if(sensorManager.getDefaultSensor(Sensor.TYPE_STEP_COUNTER)!=null){
            sensorManager.unregisterListener(this, myStepCounter);
        }
    }

    @Override
    public void onSensorChanged(SensorEvent sensorEvent) {
        if(sensorEvent.sensor == myStepCounter){
            stepCount = (int)sensorEvent.values[0];
            if(stepCount < current_steps){
                prevSteps = current_steps;
                stepCount += current_steps;
            }
            if(prevSteps == -1){
                prevSteps = stepCount;
            }
            current_steps += (stepCount - prevSteps);
            progressBar.setProgress(current_steps);
            prevSteps = stepCount;
            TextView current_steps_tv = findViewById(R.id.step_count);
            current_steps_tv.setText(Integer.toString(current_steps));
            if(progressBar.getProgress() == progressBar.getMax()){
                nextLevel();
            }
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int i) {

    }

    @Override
    protected void onResume() {
        super.onResume();
        if(sensorManager.getDefaultSensor(Sensor.TYPE_STEP_COUNTER)!=null){
            sensorManager.registerListener(this, myStepCounter, sensorManager.SENSOR_DELAY_NORMAL);
        }
    }

//    public void testShake() throws InterruptedException {
//        onSensorChanged(new SensorEvent(myStepCounter.TYPE_STEP_COUNTER, sensorManager.SENSOR_DELAY_NORMAL, new float[] {0, 0, 0}));
//        Thread.sleep(500);
//        onSensorChanged(SensorManager.SENSOR_ACCELEROMETER, new float[] {300, 300, 300});
//        Assert.assertTrue("Counter: " + mShaker.shakeCounter, mShaker.shakeCounter > 0);
//    }




}