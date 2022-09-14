package org.craftedsw.tripservicekata.trip;

import org.craftedsw.tripservicekata.exception.UserNotLoggedInException;
import org.craftedsw.tripservicekata.user.User;

import java.util.ArrayList;
import java.util.List;

public class TripService {
    private static final List<Trip> NO_TRIPS = new ArrayList<>();
    private final TripRepository repository;

    public TripService(TripRepository repository) {
        this.repository = repository;
    }

    public List<Trip> getTripsByUser(User user, User loggedUser) throws UserNotLoggedInException {
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

    protected List<Trip> findTripsByUser(User user) {
        return repository.findTripsByUser(user);
    }
}
