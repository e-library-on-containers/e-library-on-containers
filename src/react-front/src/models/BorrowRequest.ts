export interface BorrowRequest {
    id: string,
    previousOwner: string,
    newOwner: string,
    bookInstanceId: number,
    isAccepted: boolean,
    rentalId: string | null,
    borrowedAt: string | null,
    lastEditDate: Date
}