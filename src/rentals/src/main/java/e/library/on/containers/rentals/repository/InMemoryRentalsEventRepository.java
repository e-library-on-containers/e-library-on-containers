package e.library.on.containers.rentals.repository;

import e.library.on.containers.rentals.utils.events.Event;
import org.jetbrains.annotations.NotNull;
import org.springframework.stereotype.Component;

import java.util.Map;
import java.util.UUID;
import java.util.concurrent.ConcurrentHashMap;

@Component
class InMemoryRentalsEventRepository implements RentalsEventRepository {
	private final static Map<UUID, Event> inMemoryReadStore = new ConcurrentHashMap<>();

	@Override
	public void addEvent(@NotNull Event event) {
		inMemoryReadStore.put(event.getId(), event);
	}
}
