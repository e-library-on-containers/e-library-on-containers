package e.library.on.containers.rentals.repository;

import e.library.on.containers.rentals.utils.events.Event;
import org.jetbrains.annotations.NotNull;
import org.springframework.stereotype.Component;

import java.time.ZonedDateTime;
import java.util.Comparator;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

@Component
class InMemoryRentalsEventRepository implements RentalsEventRepository {
	private final static Map<UUID, Event> inMemoryReadStore = new LinkedHashMap<>();

	@Override
	public void addEvent(@NotNull Event event) {
		inMemoryReadStore.put(event.getId(), event);
	}

	@NotNull
	@Override
	public List<Event> getAllEventsPast(ZonedDateTime data) {
		if (data == null) {
			return inMemoryReadStore.values().stream()
					.sorted(Comparator.comparing(Event::getCreatedAt))
					.toList();
		}
		return inMemoryReadStore.values().stream()
				.filter(event -> event.getCreatedAt().isAfter(data))
				.sorted(Comparator.comparing(Event::getCreatedAt))
				.toList();
	}
}
