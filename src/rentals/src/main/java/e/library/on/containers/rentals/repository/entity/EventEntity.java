package e.library.on.containers.rentals.repository.entity;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import org.springframework.data.annotation.CreatedDate;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import java.time.ZonedDateTime;

@Getter
@Entity
@Table(name="event")
@AllArgsConstructor
@NoArgsConstructor
public class EventEntity {
    @Id
    String id;
    @CreatedDate
    ZonedDateTime createdAt;
    String rentalId;
    String userId;
    int bookInstanceId;
    int forHowManyDays;
    int days;
    String eventType;
}
