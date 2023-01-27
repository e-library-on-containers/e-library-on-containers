export interface RentedBook {
  id: number;
  rent_duration: number;
  return_date: Date;
  username: string;
  membership: string;
}
