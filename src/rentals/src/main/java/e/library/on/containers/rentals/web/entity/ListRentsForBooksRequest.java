package e.library.on.containers.rentals.web.entity;

import java.util.List;

public record ListRentsForBooksRequest(List<Integer> booksInstanceIds) {
}
