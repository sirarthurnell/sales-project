import { Sale } from '@/models/sale';

export interface PaginatedSalesResponse {
    totalPages: number;
    currentPage: number;
    pageContent: Sale[];
}
