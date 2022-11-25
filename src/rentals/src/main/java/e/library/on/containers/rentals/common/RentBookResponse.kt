package e.library.on.containers.rentals.common

import com.fasterxml.jackson.annotation.JsonProperty
import java.util.UUID

data class RentBookResponse(@JsonProperty("rentId") val rentId: UUID)
