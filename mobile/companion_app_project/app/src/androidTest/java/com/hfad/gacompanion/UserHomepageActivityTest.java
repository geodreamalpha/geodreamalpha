package com.hfad.gacompanion;

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
import static androidx.test.espresso.intent.Intents.intended;
import static androidx.test.espresso.intent.matcher.IntentMatchers.hasComponent;
import static androidx.test.espresso.matcher.ViewMatchers.isDisplayed;
import static androidx.test.espresso.matcher.ViewMatchers.withId;
import static androidx.test.espresso.matcher.ViewMatchers.withText;

@RunWith(AndroidJUnit4.class)
public class UserHomepageActivityTest {

    @Rule
    public ActivityScenarioRule<UserHomepageActivity> activityScenario = new ActivityScenarioRule<>(UserHomepageActivity.class);

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
        onView(withText("STRENGTH")).check(ViewAssertions.matches(isDisplayed()));
        onView(withText("SPEED")).check(ViewAssertions.matches(isDisplayed()));
    }


    @Test
    public void test_navigationDrawerOpenAndClose() {
        onView(withId(R.id.drawer_layout)).perform(DrawerActions.open())
                .check(ViewAssertions.matches(DrawerMatchers.isOpen()));
        onView(withId(R.id.drawer_layout)).perform(DrawerActions.close())
                .check(ViewAssertions.matches(DrawerMatchers.isClosed()));
    }

    @Test
    public void test_navigationDrawerLaunchStrengthMinigame() {
        onView(withId(R.id.drawer_layout)).perform(DrawerActions.open())
                .check(ViewAssertions.matches(DrawerMatchers.isOpen()));

        onView(withId(R.id.nav_view))
                .perform(NavigationViewActions.navigateTo(R.id.nav_str_minigame));

        intended(hasComponent(StrengthMinigameActivity.class.getName()));

    }

    @Test
    public void test_navigationDrawerLaunchWalkingChallenge() {
        onView(withId(R.id.drawer_layout)).perform(DrawerActions.open())
                .check(ViewAssertions.matches(DrawerMatchers.isOpen()));

        onView(withId(R.id.nav_view))
                .perform(NavigationViewActions.navigateTo(R.id.nav_spd_challenge));

        intended(hasComponent(WalkingChallengeActivity.class.getName()));

    }




}
