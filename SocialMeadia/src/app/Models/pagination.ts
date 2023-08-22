export interface Pagination {
    currentPage:number,
    itemPerPage:number,
    totalItems:number,
    totalPages:number
}
export class paginationResult<T>
{
    result!:T;
    pagination!:Pagination;
}
