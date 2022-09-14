package org.craftedsw.tripservicekata.user;

import org.craftedsw.tripservicekata.trip.Trip;

import static java.util.Arrays.stream;

public class UserBuilder {
    private User[] friends = new User[0];
    private Trip[] trips = new Trip[0];

    public static UserBuilder aUser() {
        return new UserBuilder();
    }

    public UserBuilder friendsWith(User... friends) {
        this.friends = friends;
        return this;
    }

    public UserBuilder travelledTo(Trip... trips) {
        this.trips = trips;
        return this;
    }

    public User build() {
        var user = new User();
        stream(friends).forEach(user::addFriend);
        stream(trips).forEach(user::addTrip);

        return user;
    }
}
