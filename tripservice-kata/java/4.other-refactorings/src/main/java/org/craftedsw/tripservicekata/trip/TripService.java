package org.craftedsw.tripservicekata.trip;

import io.vavr.collection.Seq;
import org.craftedsw.tripservicekata.exception.UserNotLoggedInException;
import org.craftedsw.tripservicekata.user.User;

import static io.vavr.collection.List.empty;

public class TripService {
    private static final Seq<Trip> NO_TRIPS = empty();
    private final TripRepository repository;

    public TripService(TripRepository repository) {
        this.repository = repository;
    }

    public Seq<Trip> retrieveFriendTrips(User user, User loggedUser) throws UserNotLoggedInException {
        checkUser(loggedUser);

        return user.isFriendWith(loggedUser)
                ? findTripsByUser(user)
                : NO_TRIPS;
    }

    private void checkUser(User loggedUser) {
        if (loggedUser == null) {
            throw new UserNotLoggedInException();
        }
    }

    protected Seq<Trip> findTripsByUser(User user) {
        return repository.findTripsByUser(user);
    }
}
