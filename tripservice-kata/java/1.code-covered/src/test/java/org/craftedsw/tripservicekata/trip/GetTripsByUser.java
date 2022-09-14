package org.craftedsw.tripservicekata.trip;

import org.craftedsw.tripservicekata.exception.UserNotLoggedInException;
import org.junit.jupiter.api.DisplayNameGeneration;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThatThrownBy;
import static org.junit.jupiter.api.DisplayNameGenerator.ReplaceUnderscores;

@DisplayNameGeneration(ReplaceUnderscores.class)
class GetTripsByUser {
    @Nested
    class return_An_Error {
        @Test
        void when_user_is_not_loggedIn() {
            var tripService = new TripService();
            assertThatThrownBy(() -> tripService.getTripsByUser(null))
                    .isInstanceOf(UserNotLoggedInException.class);
        }
    }
}
