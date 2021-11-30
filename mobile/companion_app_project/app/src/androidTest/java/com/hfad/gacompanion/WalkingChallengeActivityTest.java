package com.hfad.gacompanion;

import android.view.Gravity;
import android.widget.TextView;

import androidx.annotation.ContentView;
import androidx.appcompat.app.AppCompatActivity;
import androidx.test.InstrumentationRegistry;
import androidx.test.core.app.ActivityScenario;
import androidx.test.espresso.assertion.ViewAssertions;
import androidx.test.espresso.contrib.DrawerActions;
import androidx.test.espresso.contrib.DrawerMatchers;
import androidx.test.espresso.contrib.NavigationViewActions;
import androidx.test.espresso.intent.Intents;
import androidx.test.ext.junit.rules.ActivityScenarioRule;
import androidx.test.ext.junit.runners.AndroidJUnit4;

import org.junit.After;
import org.junit.Before;
import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import static androidx.test.espresso.Espresso.onView;
import static androidx.test.espresso.action.ViewActions.click;
import static androidx.test.espresso.contrib.DrawerMatchers.isClosed;
import static androidx.test.espresso.intent.Intents.intended;
import static androidx.test.espresso.intent.matcher.IntentMatchers.hasComponent;
import static androidx.test.espresso.matcher.ViewMatchers.isClickable;
import static androidx.test.espresso.matcher.ViewMatchers.isDisplayed;
import static androidx.test.espresso.matcher.ViewMatchers.withId;
import static androidx.test.espresso.matcher.ViewMatchers.withText;
import static org.junit.Assert.*;

@RunWith(AndroidJUnit4.class)
public class WalkingChallengeActivityTest {


    @Rule
    public ActivityScenarioRule<WalkingChallengeActivity> activityScenario = new ActivityScenarioRule<>(WalkingChallengeActivity.class);

    @Before
    public void setUp() throws Exception{
        Intents.init();
    }

    @After
    public void tearDown() throws Exception{
        Intents.release();
    }

    @Test
    public void test_activityInView() {
        onView(withText("SPEED")).check(ViewAssertions.matches(isDisplayed()));
    }

//    @Test
//    public void test_onSensorChanged() {
//        activityScenario.getScenario().onActivity();
//    }

    @Test
    public void test_navigationDrawerOpenAndClose() {
        onView(withId(R.id.drawer_layout)).perform(DrawerActions.open())
                .check(ViewAssertions.matches(DrawerMatchers.isOpen()));
        onView(withId(R.id.drawer_layout)).perform(DrawerActions.close())
                .check(ViewAssertions.matches(DrawerMatchers.isClosed()));
    }



    @Test
    public void test_navigationDrawerLaunchUserHomepage() {
        onView(withId(R.id.drawer_layout)).perform(DrawerActions.open())
                .check(ViewAssertions.matches(DrawerMatchers.isOpen()));

        onView(withId(R.id.nav_view))
                .perform(NavigationViewActions.navigateTo(R.id.navigate_to_user_homepage));

        intended(hasComponent(UserHomepageActivity.class.getName()));

    }

    @Test
    public void test_navigationDrawerLaunchStrengthMinigame() {
        onView(withId(R.id.drawer_layout)).perform(DrawerActions.open())
                .check(ViewAssertions.matches(DrawerMatchers.isOpen()));

        onView(withId(R.id.nav_view))
                .perform(NavigationViewActions.navigateTo(R.id.nav_str_minigame));

        intended(hasComponent(StrengthMinigameActivity.class.getName()));

    }







}
