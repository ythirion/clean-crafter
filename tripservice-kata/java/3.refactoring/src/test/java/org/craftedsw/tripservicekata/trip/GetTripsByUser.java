package org.craftedsw.tripservicekata.trip;

import org.craftedsw.tripservicekata.exception.UserNotLoggedInException;
import org.craftedsw.tripservicekata.user.User;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayNameGeneration;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;

import java.util.List;

import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;
import static org.craftedsw.tripservicekata.user.UserBuilder.aUser;
import static org.junit.jupiter.api.DisplayNameGenerator.ReplaceUnderscores;

@DisplayNameGeneration(ReplaceUnderscores.class)
class GetTripsByUser {
    private final User registeredUser = aUser().build();
    private TripService tripService;
    private User loggedUser;

    @BeforeEach
    void setup() {
        loggedUser = registeredUser;
        tripService = createTripService();
    }

    @Nested
    class return_An_Error {
        private final User guest = null;

        @Test
        void when_user_is_not_loggedIn() {
            notLoggedUser();
            assertThatThrownBy(() -> tripService.getTripsByUser(guest))
                    .isInstanceOf(UserNotLoggedInException.class);
        }

        private void notLoggedUser() {
            loggedUser = guest;
        }
    }

    @Nested
    class return_ {
        private final Trip lisbon = new Trip();
        private final Trip springfield = new Trip();
        private final User anotherUser = aUser().build();

        @Test
        void no_trips_when_logged_user_is_not_a_friend_of_the_target_user() {
            var aUserWithTrips = aUser()
                    .friendsWith(anotherUser)
                    .travelledTo(lisbon)
                    .build();

            assertThat(tripService.getTripsByUser(aUserWithTrips))
                    .isEmpty();
        }

        @Test
        void all_the_target_user_trips_when_logged_user_and_target_user_are_friends() {
            var aUserWithTrips = aUser()
                    .friendsWith(anotherUser, loggedUser)
                    .travelledTo(lisbon, springfield)
                    .build();

            assertThat(tripService.getTripsByUser(aUserWithTrips))
                    .hasSize(2)
                    .contains(lisbon, springfield);
        }
    }

    private TripService createTripService() {
        return new SeamTripService();
    }

    private class SeamTripService extends TripService {
        @Override
        protected User getLoggedUser() {
            return loggedUser;
        }

        @Override
        protected List<Trip> findTripsByUser(User user) {
            return user.trips();
        }
    }
}
