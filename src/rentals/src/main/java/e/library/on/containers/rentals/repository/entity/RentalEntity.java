package e.library.on.containers.rentals.repository.entity;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import org.jetbrains.annotations.NotNull;
import org.springframework.data.annotation.LastModifiedDate;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import java.time.ZonedDateTime;

@Getter
@Entity
@Table(name="rental")
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