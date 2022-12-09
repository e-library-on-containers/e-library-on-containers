package e.library.on.containers.rentals.repository

import e.library.on.containers.rentals.utils.events.Event
import java.time.ZonedDateTime

interface RentalsEventRepository {
    fun addEvent(event: Event)

    fun getAllEventsPast(data: ZonedDateTime?): List<Event>
}
