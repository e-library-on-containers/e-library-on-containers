package e.library.on.containers.rentals.repository.entity;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.EqualsAndHashCode;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import lombok.ToString;
import org.jetbrains.annotations.NotNull;
import org.springframework.data.annotation.LastModifiedDate;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.OneToOne;
import javax.persistence.Table;
import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
@Setter
@Entity
@Table(name="borrow")
@AllArgsConstructor
@NoArgsConstructor(force = true)
@ToString
@Builder
@EqualsAndHashCode
public class BorrowEntity {
    @Id
    UUID id;
    @NotNull
    UUID previousOwner;
    @NotNull
    UUID newOwner;
    int bookInstanceId;
    boolean accepted;
    @OneToOne(optional = true)
    @JoinColumn(name="created_rental")
    RentalEntity createdRental;
    ZonedDateTime borrowedAt;
    @LastModifiedDate
    ZonedDateTime lastEditDate;
}
