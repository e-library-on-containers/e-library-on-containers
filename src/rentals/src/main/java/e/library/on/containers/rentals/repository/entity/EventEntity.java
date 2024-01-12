package e.library.on.containers.rentals.repository.entity;

import e.library.on.containers.rentals.repository.dao.EventType;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import org.springframework.data.annotation.CreatedDate;

import javax.persistence.Entity;
import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.Id;
import javax.persistence.Table;
import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
@Entity
@Table(name = "event")
@AllArgsConstructor
@NoArgsConstructor
public class EventEntity {
    @Id
    UUID id;
    @CreatedDate
    ZonedDateTime createdAt;
    UUID rentalId;
    UUID userId;
    int bookInstanceId;
    int forHowManyDays;
    int days;
    @Enumerated(EnumType.ORDINAL)
    EventType eventType;
}
