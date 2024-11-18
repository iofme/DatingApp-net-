export interface Pagination{
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    TotalPages: number;
}

export class PaginatedResult<T>{
    items?: T;
    pagination?: Pagination;
}