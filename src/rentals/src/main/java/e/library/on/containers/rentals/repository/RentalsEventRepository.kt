package e.library.on.containers.rentals.repository

import e.library.on.containers.rentals.utils.events.Event

interface RentalsEventRepository {
    fun addEvent(event: Event)
}
