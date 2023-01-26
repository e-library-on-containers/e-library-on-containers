package e.library.on.containers.rentals.repository.entity;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.jetbrains.annotations.NotNull;
import org.springframework.data.annotation.LastModifiedDate;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import java.time.ZonedDateTime;

@Entity
@Table(name="rentals")
@Data
@AllArgsConstructor
@NoArgsConstructor
public class RentalEntity {
    @Id
    String id;
    int bookInstanceId;
    @NotNull
    String userId;
    @NotNull
    ZonedDateTime rentedAt;
    @NotNull
    ZonedDateTime dueDate;
    boolean isExtended;
    @LastModifiedDate
    ZonedDateTime lastEditDate;
}
