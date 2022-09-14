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
import static org.junit.jupiter.api.DisplayNameGenerator.ReplaceUnderscores;

@DisplayNameGeneration(ReplaceUnderscores.class)
class GetTripsByUser {
    private final User loggedInUser = new User();
    private final User targetUser = new User();
    private TripService tripService;
    private User loggedUser;

    @BeforeEach
    void setup() {
        loggedUser = loggedInUser;
        tripService = createTripService();
    }

    @Nested
    class return_An_Error {
        @Test
        void when_user_is_not_loggedIn() {
            notLoggedUser();
            assertThatThrownBy(() -> tripService.getTripsByUser(targetUser))
                    .isInstanceOf(UserNotLoggedInException.class);
        }

        private void notLoggedUser() {
            loggedUser = null;
        }
    }

    @Nested
    class return_ {
        @Test
        void no_trips_when_logged_user_is_not_a_friend_of_the_target_user() {
            assertThat(tripService.getTripsByUser(targetUser))
                    .isEmpty();
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
