package e.library.on.containers.rentals.common

import com.fasterxml.jackson.annotation.JsonProperty

data class RentBookRequest(@JsonProperty("isbn") val isbn: String)
