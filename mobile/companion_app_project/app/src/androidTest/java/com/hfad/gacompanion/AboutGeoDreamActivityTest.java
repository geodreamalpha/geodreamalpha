package com.hfad.gacompanion;

import android.app.Activity;
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
import androidx.test.rule.ActivityTestRule;

import org.junit.After;
import org.junit.Before;
import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import static androidx.test.espresso.Espresso.onView;
import static androidx.test.espresso.action.ViewActions.click;
import static androidx.test.espresso.contrib.ActivityResultMatchers.hasResultCode;
import static androidx.test.espresso.contrib.DrawerMatchers.isClosed;
import static androidx.test.espresso.intent.Intents.intended;
import static androidx.test.espresso.intent.matcher.IntentMatchers.hasComponent;
import static androidx.test.espresso.matcher.ViewMatchers.isClickable;
import static androidx.test.espresso.matcher.ViewMatchers.isDisplayed;
import static androidx.test.espresso.matcher.ViewMatchers.withId;
import static androidx.test.espresso.matcher.ViewMatchers.withText;
import static org.junit.Assert.*;

@RunWith(AndroidJUnit4.class)
public class AboutGeoDreamActivityTest {
    @Rule
    public ActivityScenarioRule<AboutGeoDreamActivity> activityScenario = new ActivityScenarioRule<>(AboutGeoDreamActivity.class);

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
        onView(withText("Go Back")).check(ViewAssertions.matches(isDisplayed()));
    }

    @Test
    public void test_goBackButton() {
        onView(withId(R.id.goBackButton)).check(ViewAssertions.matches(isClickable()));
        onView(withId(R.id.goBackButton)).perform(click());
        intended(hasComponent(LogInActivity.class.getName()));
    }


}
