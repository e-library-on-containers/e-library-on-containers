export interface Rent {
  id: string
  bookCopyId: number
  userId: string
  rentedAt: Date
  dueDate: Date
  wasExtended: boolean
}
