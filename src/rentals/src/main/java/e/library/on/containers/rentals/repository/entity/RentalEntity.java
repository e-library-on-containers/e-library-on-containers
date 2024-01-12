package e.library.on.containers.rentals.repository.entity;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.EqualsAndHashCode;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.ToString;
import org.jetbrains.annotations.NotNull;
import org.springframework.data.annotation.LastModifiedDate;

import javax.persistence.Entity;
import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.Id;
import javax.persistence.Table;
import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
@Entity
@Table(name="rental")
@AllArgsConstructor
@NoArgsConstructor(force = true)
@ToString
@Builder
@EqualsAndHashCode
public class RentalEntity {
    @Id
    UUID id;
    int bookInstanceId;
    @NotNull
    UUID userId;
    @NotNull
    ZonedDateTime rentedAt;
    @NotNull
    ZonedDateTime dueDate;
    @Enumerated(EnumType.ORDINAL)
    RentalState rentalState;
    @LastModifiedDate
    ZonedDateTime lastEditDate;
}
